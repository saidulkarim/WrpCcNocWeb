using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Helpers;
using static WrpCcNocWeb.Helpers.CommonHelper;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using WrpCcNocWeb.Models.Utility;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.DatabaseContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WrpCcNocWeb.Controllers
{
    public class homeController : Controller
    {
        #region Initialization
        private readonly HttpContext context;
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private ILogger<homeController> _logger;
        #endregion

        public homeController(ILogger<homeController> logger)
        {
            _logger = logger;
        }

        public IActionResult index()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            List<CcModAppProjectCommonDetail> pcds = new List<CcModAppProjectCommonDetail>();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserMenuPermissionToSession(ui);
            GetUserLevelInfo(ui.UserID);

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");

            if (uli.UserGroupId == 1000000001 && uli.AuthorityLevelId == 0)
            {
                pcds = _db.CcModAppProjectCommonDetail.Where(w => w.UserId == ui.UserID).ToList();
            }
            else if (uli.UserGroupId != 1000000001 && uli.AuthorityLevelId == 0)
            {
                pcds = _db.CcModAppProjectCommonDetail.ToList();
            }
            else
            {
                List<long> ProjectList = new List<long>();

                if (!string.IsNullOrEmpty(uli.UnionGeoCode))
                {
                    ProjectList = new List<long>();
                    pcds = new List<CcModAppProjectCommonDetail>();

                    ProjectList = _db.CcModPrjLocationDetail.Where(w => w.UnionGeoCode == uli.UnionGeoCode).Select(s => s.ProjectId).Distinct().ToList();
                    pcds = _db.CcModAppProjectCommonDetail.Where(w => w.ApplicationStateId == uli.ApplicationStateId && ProjectList.Any(a => w.ProjectId == a.ToString().ToLong())).ToList();
                }

                if (!string.IsNullOrEmpty(uli.UpazilaGeoCode) && string.IsNullOrEmpty(uli.UnionGeoCode))
                {
                    ProjectList = new List<long>();
                    pcds = new List<CcModAppProjectCommonDetail>();

                    ProjectList = _db.CcModPrjLocationDetail.Where(w => w.UpazilaGeoCode == uli.UpazilaGeoCode).Select(s => s.ProjectId).Distinct().ToList();
                    pcds = _db.CcModAppProjectCommonDetail.Where(w => w.ApplicationStateId == uli.ApplicationStateId && ProjectList.Any(a => w.ProjectId == a.ToString().ToLong())).ToList();
                }

                if (!string.IsNullOrEmpty(uli.DistrictGeoCode) && string.IsNullOrEmpty(uli.UpazilaGeoCode) && string.IsNullOrEmpty(uli.UnionGeoCode))
                {
                    ProjectList = new List<long>();
                    pcds = new List<CcModAppProjectCommonDetail>();

                    ProjectList = _db.CcModPrjLocationDetail.Where(w => w.DistrictGeoCode == uli.DistrictGeoCode).Select(s => s.ProjectId).Distinct().ToList();
                    pcds = _db.CcModAppProjectCommonDetail.Where(w => w.ApplicationStateId == uli.ApplicationStateId && ProjectList.Any(a => w.ProjectId == a.ToString().ToLong())).ToList();
                }
            }

            int count = pcds.Count();
            ViewBag.TotalAppliedApplication = count.ToString().PadLeft(3, '0');

            return View();
        }

        /// <summary>
        /// Author: A.T.M. saidul Karim.
        /// Written on: 09 Mar, 2020.
        /// Purpose: Getting user permitted menu and sub-menu
        /// and stored to session. Session name is: UserMenu
        /// </summary>
        /// <param name="ui"></param>
        //Using: UserMenu sf = HttpContext.Session.GetComplexData<UserMenu>("UserMenu");
        private void UserMenuPermissionToSession(UserInfo ui)
        {
            List<UserMenu> uml = new List<UserMenu>();
            long userGroupID = _db.AdminModUserGrpDistDetail.Where(w => w.UserId == ui.UserID).Select(s => s.UserGroupId).FirstOrDefault();
            var HasAuthLevelId = (from ugd in _db.AdminModUserGrpDistDetail
                                  join uGroup in _db.LookUpAdminModUserGroup on ugd.UserGroupId equals uGroup.UserGroupId into userGroup
                                  from ug in userGroup.DefaultIfEmpty()
                                  where ugd.UserId == ui.UserID
                                  select new { ug.AuthorityLevelId }).FirstOrDefault();

            int AuthLevelID = HasAuthLevelId.AuthorityLevelId ?? 0;

            if (AuthLevelID == 0)
            {
                uml = new List<UserMenu>();
                uml = (from u in _db.AdminModUsersDetail
                       join amugdd in _db.AdminModUserGrpDistDetail on u.UserId equals amugdd.UserId into ugd
                       from ug in ugd.DefaultIfEmpty()
                       join amugwmd in _db.AdminModUserGrpWiseMenuDetail on ug.UserGroupId equals amugwmd.UserGroupId into ugmd
                       from ugm in ugmd.DefaultIfEmpty()
                       join luamm in _db.LookUpAdminModMenu on ugm.MenuId equals luamm.MenuId into amm
                       from m in amm.DefaultIfEmpty()
                       join luamsm in _db.LookUpAdminModSubMenu on ugm.SubMenuId equals luamsm.SubMenuId into amsm
                       from sm in amsm.DefaultIfEmpty()

                       where u.UserId == ui.UserID && ugm.MenuId == sm.MenuId

                       select new UserMenu
                       {
                           UserId = u.UserId,
                           UserGroupId = ug.UserGroupId,
                           MenuId = ugm.MenuId,
                           MenuTitle = m.MenuTitle,
                           SubMenuId = ugm.SubMenuId,
                           SubMenuTitle = sm.SubMenuTitle,
                           Controller = sm.Controller,
                           Action = sm.Action
                       }).OrderBy(o => o.MenuId).ThenBy(t => t.SubMenuId).ToList();
            }
            else
            {
                uml = new List<UserMenu>();
                uml = (from amugwmd in _db.AdminModUserGrpWiseMenuDetail.Where(w => w.AuthorityLevelId == AuthLevelID)
                       join luamm in _db.LookUpAdminModMenu on amugwmd.MenuId equals luamm.MenuId into amm
                       from m in amm.DefaultIfEmpty()
                       join luamsm in _db.LookUpAdminModSubMenu on amugwmd.SubMenuId equals luamsm.SubMenuId into amsm
                       from sm in amsm.DefaultIfEmpty()

                           //where amugwmd.AuthorityLevelId == AuthLevelID
                       where sm.MenuId == m.MenuId

                       select new UserMenu
                       {
                           UserId = ui.UserID,
                           UserGroupId = userGroupID,
                           MenuId = amugwmd.MenuId,
                           MenuTitle = m.MenuTitle,
                           SubMenuId = amugwmd.SubMenuId,
                           SubMenuTitle = sm.SubMenuTitle,
                           Controller = sm.Controller,
                           Action = sm.Action
                       }).OrderBy(o => o.MenuId).ThenBy(t => t.SubMenuId).ToList();
            }

            if (uml.Count > 0)
            {
                HttpContext.Session.SetComplexData("UserMenu", uml);
                ViewBag.UserMenu = uml;
                string UserMenuJson = JsonConvert.SerializeObject(uml);

                HttpContext.Response.Cookies.Append("UserMenu", UserMenuJson, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(1)
                });
            }
            else
            {
                uml = new List<UserMenu>();
                UserMenu um = new UserMenu
                {
                    UserId = ui.UserID,
                    UserGroupId = 0,
                    MenuId = 0,
                    MenuTitle = "No menu assigned",
                    SubMenuId = 0,
                    SubMenuTitle = "No sub-menu assigned",
                    Controller = "account",
                    Action = "login"
                };

                uml.Add(um);

                HttpContext.Session.SetComplexData("UserMenu", uml);
                ViewBag.UserMenu = uml;
                string UserMenuJson = JsonConvert.SerializeObject(uml);

                HttpContext.Response.Cookies.Append("UserMenu", UserMenuJson, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(1)
                });
            }
        }

        /// <summary>
        /// Author: A.T.M. saidul Karim.
        /// Written on: 08 Apr, 2020.
        /// Purpose: Getting user level info
        /// </summary>
        /// <param name="userID"></param>
        private void GetUserLevelInfo(long userID)
        {
            UserLevelInfo userLevelInfo = (from ugd in _db.AdminModUserGrpDistDetail.Where(w => w.UserId == userID)
                                           join uGroup in _db.LookUpAdminModUserGroup on ugd.UserGroupId equals uGroup.UserGroupId into userGroup
                                           from ug in userGroup.DefaultIfEmpty()
                                           join aState in _db.LookUpCcModApplicationState on ug.AuthorityLevelId equals aState.AuthorityLevelId into aGroup
                                           from st in aGroup.DefaultIfEmpty()
                                               //where ugd.UserId == userID
                                           select new UserLevelInfo
                                           {
                                               UserID = userID,
                                               UserGroupId = ug.UserGroupId,
                                               UserGroupName = ug.UserGroupName,
                                               AuthorityLevelId = ug.AuthorityLevelId ?? 0,
                                               ApplicationStateId = st.ApplicationStateId,
                                               DistrictGeoCode = ug.DistrictGeoCode,
                                               UpazilaGeoCode = ug.UpazilaGeoCode,
                                               UnionGeoCode = ug.UnionGeoCode
                                           }).FirstOrDefault();

            HttpContext.Session.SetComplexData("UserLevelInfo", userLevelInfo);
            ViewBag.UserLevelInfo = userLevelInfo;
        }

        public IActionResult privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

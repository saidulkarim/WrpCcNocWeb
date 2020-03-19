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

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserMenuPermissionToSession(ui);
            int count = _db.CcModAppProjectCommonDetail.Where(w => w.UserId == ui.UserID).Count();
            ViewBag.TotalAppliedApplication = count.ToString().PadLeft(3, '0');

            //string UserRegistrationID = HttpContext.Session.GetString("UserRegistrationID").ToString();
            //string UserName = HttpContext.Session.GetString("UserName").ToString();
            //string UserEmail = HttpContext.Session.GetString("UserEmail").ToString();
            //string UserMobile = HttpContext.Session.GetString("UserMobile").ToString();

            //if (ui != null)
            //{
            //    TempData["Message"] = ch.ShowMessage(Sign.Info, Sign.Info.ToString(), "Hello, this is " + ui.UserID);
            //}
            //else
            //{
            //    TempData["Message"] = ch.ShowMessage(Sign.Info, Sign.Info.ToString(), "Hello, Session not working.");
            //    return RedirectToActionPermanent("login", "account");
            //}

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

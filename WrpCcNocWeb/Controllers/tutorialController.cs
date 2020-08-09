using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.Utility;

namespace WrpCcNocWeb.Controllers
{
    public class tutorialController : Controller
    {
        #region Initialization       
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        private readonly string rootDirOfDocs = "../docs";
        #endregion

        public IActionResult index()
        {
            return View();
        }

        public IActionResult eng()
        {
            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

            ViewData["Title"] = "User Manual | Clearance Certificate";
            return View();
        }

        public IActionResult ban()
        {
            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

            ViewData["Title"] = "ব্যবহার বিধি | ছাড়পত্র";
            return View();
        }

        public IActionResult video()
        {
            ViewData["Title"] = "Video Tutorial | Clearance Certificate";
            return View();
        }

        public int GetHighestLevelAuthority()
        {
            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            List<LookUpAdminModUserGroup> userGroupList = new List<LookUpAdminModUserGroup>();
            int result = 0;

            if (!string.IsNullOrEmpty(uli.UnionGeoCode))
            {
                userGroupList = _db.LookUpAdminModUserGroup.Where(w => w.UnionGeoCode == uli.UnionGeoCode).ToList();
            }

            if (string.IsNullOrEmpty(uli.UnionGeoCode) && !string.IsNullOrEmpty(uli.UpazilaGeoCode))
            {
                userGroupList = _db.LookUpAdminModUserGroup.Where(w => w.UpazilaGeoCode == uli.UpazilaGeoCode).ToList();
            }

            if (string.IsNullOrEmpty(uli.UnionGeoCode) && string.IsNullOrEmpty(uli.UpazilaGeoCode) && !string.IsNullOrEmpty(uli.DistrictGeoCode))
            {
                userGroupList = _db.LookUpAdminModUserGroup.Where(w => w.DistrictGeoCode == uli.DistrictGeoCode).ToList();
            }

            if (userGroupList.Count > 0)
            {
                int higherAuthLevelId = userGroupList.Min(m => m.AuthorityLevelId).Value;
                result = higherAuthLevelId;
            }
            else
            {
                result = 0;
            }

            return result;
        }
    }
}
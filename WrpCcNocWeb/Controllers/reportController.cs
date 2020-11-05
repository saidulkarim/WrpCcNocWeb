using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.ReportModels;
using WrpCcNocWeb.Models.Utility;
using static WrpCcNocWeb.Helpers.CommonHelper;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace WrpCcNocWeb.Controllers
{
    public class reportController : Controller
    {
        #region Initialization
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private commonController cc = new commonController();
        private EmailService es = new EmailService();
        private SmsService ss = new SmsService();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        private readonly string rootDirOfProjFile = "../images";
        private readonly string rootDirOfDocs = "../docs";
        #endregion

        //report/index
        public IActionResult Index()
        {
            return View();
        }

        //report/registry
        public IActionResult registry()
        {
            ViewData["Title"] = "Registry Book of Clearance Certificate";
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            List<CcRegistryBook> rbList = new List<CcRegistryBook>();

            try
            {
                rbList = (from c in _db.CcModAppProjectCommonDetail

                          join proj in _db.LookUpCcModProjectType on c.ProjectTypeId equals proj.ProjectTypeId into projList
                          from pt in projList.DefaultIfEmpty()

                          join user in _db.AdminModUsersDetail on c.UserId equals user.UserId into userList
                          from u in userList.DefaultIfEmpty()

                          join l in _db.CcModPrjLocationDetail on c.ProjectId equals l.ProjectId into locList
                          from loc in locList.DefaultIfEmpty()

                          join dst in _db.LookUpAdminBndDistrict on loc.DistrictGeoCode equals dst.DistrictGeoCode into dstList
                          from dist in dstList.DefaultIfEmpty()

                          join upz in _db.LookUpAdminBndUpazila on loc.UpazilaGeoCode equals upz.UpazilaGeoCode into upzList
                          from upaz in upzList.DefaultIfEmpty()

                          //where c.ApprovalStatusId == 1

                          select new CcRegistryBook
                          {
                              AppSubmissionId = c.AppSubmissionId,
                              ApplicantNameAddress = u.ApplicantTypeId == 1 ? String.Format("{0}<br />{1}<br />{2}", u.ApplicantName, u.UserProfession, u.PostalAddress) : String.Format("{0}<br />{1}<br />{2}", u.OrganizationName, u.UserDesignation, u.PostalAddress),
                              ProjectType = pt.ProjectType,
                              ProjectObjective = c.ProjectObjective,
                              ProjectDistrict = dist.DistrictName,
                              ProjectUpazila = upaz.UpazilaName,
                              MethodOfUsing = "",
                              IssuingDate = c.AppApprovalDate.Value.ToString("dd MMM, yyyy"),
                              ExpiringDate = c.AppApprovalDate.Value.AddYears(2).ToString("dd MMM, yyyy"),
                              ClearanceTerms = ""
                          }).OrderBy(o => o.AppSubmissionId).ToList();

                if (rbList.Count > 0)
                {
                    ViewData["RegistryBookList"] = rbList;
                }
                else
                {
                    ViewData["RegistryBookList"] = null;
                }
            }
            catch (Exception ex)
            {
                rbList = new List<CcRegistryBook>();
                string message = ch.ExtractInnerException(ex);
                TempData["Message"] = ch.ShowMessage(Sign.Error, "An Error Occured", message);
                ViewData["RegistryBookList"] = null;
            }

            //return new ViewAsPdf("~/Views/report/registry.cshtml", viewData: ViewData)
            //{
            //    PageSize = Rotativa.AspNetCore.Options.Size.A4,
            //    PageOrientation = Orientation.Landscape,
            //    PageMargins = new Margins(10, 10, 10, 10)
            //};

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.TempModels;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.Utility;
using WrpCcNocWeb.ViewModels;
using static WrpCcNocWeb.Helpers.CommonHelper;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace WrpCcNocWeb.Controllers
{
    public class certificateController : Controller
    {
        #region Initialization        
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        private readonly string rootDirOfProjFile = "../images";
        private readonly string rootDirOfDocs = "../docs";
        #endregion        

        public IActionResult index()
        {
            return View();
        }

        //Flood Control Management Project
        //certificate/fcmp/59
        public IActionResult fcmp(long? id)
        {
            if (id == null || id == 0)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Ivalid ID", "Sorry, invalid project ID provided!");
                return RedirectToAction("list", "form");
            }

            CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(id);
            if (pcd == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Project Info Error", "Sorry, project information not found!");
                return RedirectToAction("list", "form");
            }

            LookUpCcModProjectType pt = _db.LookUpCcModProjectType.Find(pcd.ProjectTypeId);
            if (pt == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Project Info Error", "Sorry, project type information not found!");
                return RedirectToAction("list", "form");
            }

            LookUpCcModCertificateFormat cf = _db.LookUpCcModCertificateFormat.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).FirstOrDefault();
            if (cf == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Certificate Format Error", "Sorry, certificate template not found!");
                return RedirectToAction("list", "form");
            }

            ApplicantInfo ai = GetApplicantInfo(pcd.UserId);

            if (ai != null)
            {
                WrpCcNocWebCertificate crt = new WrpCcNocWebCertificate
                {
                    ApplicantName = ai.ApplicantName,
                    ApplicantNameBn = ai.ApplicantNameBn,
                    ApplicantAddress = ai.ApplicantAddress,
                    ApplicantAddressBn = ai.ApplicantAddressBn,
                    ApplicantMobile = ai.ApplicantMobile,
                    ApplicantMobileBn = ai.ApplicantMobileBn.NumberEnglishToBengali(),
                    ApplicantEmail = ai.ApplicantEmail,
                    ApplicantGroupName = ai.ApplicantGroupName,
                    LanguageId = pcd.LanguageId ?? 0,
                    ClearanceNo = pcd.AppSubmissionId,
                    ClearanceNoBn = pcd.AppSubmissionId.ToString().NumberEnglishToBengali(),
                    ClearanceDate = DateTime.Now.ToString("dd MMMM, yyyy"),
                    ClearanceDateBn = DateTime.Now.ToString("dd MMMM, yyyy").NumberEnglishToBengali().MonthEnglishToBengali(),
                    ProjectType = pt,
                    FormNo = pt.ProjectTypeId.ToString().PadLeft(2, '0'),
                    FormNoBn = pt.ProjectTypeId.ToString().PadLeft(2, '0').NumberEnglishToBengali(),
                    CertificateFormat = cf
                };

                ViewData["WrpCcNocWebCertificate"] = crt;

                return new ViewAsPdf("~/Views/certificate/fcmp.cshtml", viewData: ViewData)
                {
                    PageSize = Size.A4,
                    PageOrientation = Orientation.Portrait,
                    PageMargins = new Margins(10, 10, 10, 10)
                };
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Application Info", "Sorry, application information not found!");
                return RedirectToAction("list", "form");
            }
        }

        public ApplicantInfo GetApplicantInfo(long userID)
        {
            var applicant_info = (from u in _db.AdminModUsersDetail
                                  join r in _db.AdminModUserRegistrationDetail on u.UserRegistrationId equals r.UserRegistrationId into rList
                                  from reg in rList.DefaultIfEmpty()
                                  join a in _db.AdminModUserGrpDistDetail on u.UserId equals a.UserId into aList
                                  from ugd in aList.DefaultIfEmpty()
                                  join ug in _db.LookUpAdminModUserGroup on ugd.UserGroupId equals ug.UserGroupId into ugList
                                  from ugl in ugList.DefaultIfEmpty()

                                  where u.UserId == userID

                                  select new ApplicantInfo
                                  {
                                      ApplicantUserId = u.UserId,
                                      ApplicantName = u.UserFullName,
                                      ApplicantNameBn = u.UserFullNameBn,
                                      ApplicantAddress = u.UserAddress,
                                      ApplicantAddressBn = u.UserAddressBn,
                                      ApplicantMobile = reg.UserMobile,
                                      ApplicantMobileBn = reg.UserMobile,
                                      ApplicantEmail = reg.UserEmail,
                                      ApplicantGroupName = ugl.UserGroupName
                                  }).FirstOrDefault();

            return applicant_info;
        }
    }
}
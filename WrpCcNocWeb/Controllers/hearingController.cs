using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.TempModels;
using WrpCcNocWeb.Models.Utility;
using WrpCcNocWeb.Models.UserManagement;

namespace WrpCcNocWeb.Controllers
{
    public class hearingController : Controller
    {
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly EmailService _es = new EmailService();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult send(long? id, int? lang)
        {
            CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(id);

            if (_pcd != null)
            {
                ViewData["ProjectId"] = _pcd.ProjectId;
                GetApplicantInfoViewData(_pcd.UserId);
                ViewData["SuccessEmailSend"] = "not yet";
            }

            return View();
        }

        [HttpPost]
        public IActionResult send()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            string _hearingBody = string.Empty;
            long ProjectApplicantId = 0;

            long _projectId = Request.Form["ProjectId"].ToString().ToLong();
            string _applicantEmail = Request.Form["ApplicantEmail"].ToString().Trim();
            string _hearingSubject = Request.Form["HearingSubject"].ToString().Trim();
            string _hearingReason = Request.Form["HearingReason"].ToString().Trim();

            string _hearingDate = Request.Form["DateOfHearing"].ToString().Trim();
            string _hearingTime = Request.Form["TimeOfHearing"].ToString().Trim();
            string _hearingPlace = Request.Form["HearingPlace"].ToString().Trim();

            _hearingBody += "Date of Hearing: " + _hearingDate + "<br />";
            _hearingBody += "Time of Hearing: " + _hearingTime + "<br />";
            _hearingBody += "Hearing Place: " + _hearingPlace + "<br /><br />";
            _hearingBody += "Reason: " + "<br />" + _hearingReason + "<br /><br />";

            CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(_projectId);

            try
            {
                if (_pcd != null)
                {
                    if (_hearingBody.Length > 500)
                    {
                        ViewData["SuccessEmailSend"] = "Hearing reason text could not be greater than 500 characters.";
                        GetApplicantInfoViewData(_pcd.ProjectId);
                        return View();
                    }

                    ProjectApplicantId = ui.UserID;
                    using var dbContextTransaction = _db.Database.BeginTransaction();

                    CcModAppProjHearing hearing = new CcModAppProjHearing
                    {
                        ProjectId = _projectId,
                        SenderUserId = ProjectApplicantId,
                        HearingSubject = _hearingSubject,
                        HearingReason = _hearingBody,
                        HearingPlace = _hearingPlace,
                        DateOfHearing = _hearingDate,
                        TimeOfHearing = _hearingTime
                    };

                    EmailModel em = new EmailModel
                    {
                        To = _applicantEmail,
                        Subject = _hearingSubject,
                        Body = _hearingBody,
                        Attachment = null
                    };

                    _es.Send(em);
                    _db.CcModAppProjHearing.Add(hearing);
                    int result = _db.SaveChanges();

                    if (result > 0)
                    {
                        dbContextTransaction.Commit();
                        ViewData["SuccessEmailSend"] = "success";
                    }
                    else
                    {
                        dbContextTransaction.Rollback();
                        ViewData["SuccessEmailSend"] = "failed";
                    }

                    ViewData["ProjectId"] = _pcd.ProjectId;
                }
            }
            catch (Exception ex)
            {
                ViewData["SuccessEmailSend"] = ex.Message;
            }

            GetApplicantInfoViewData(_pcd.UserId);
            return View();
        }

        public void GetApplicantInfoViewData(long userID)
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
                                      //ApplicantName = u.UserFullName,
                                      ApplicantName = u.ApplicantTypeId == 1 ? u.ApplicantName : u.OrganizationName,
                                      //ApplicantNameBn = u.UserFullNameBn,
                                      ApplicantNameBn = u.ApplicantTypeId == 1 ? u.ApplicantNameBn : u.OrganizationNameBn,
                                      //ApplicantAddress = u.UserAddress,
                                      ApplicantAddress = u.PostalAddress,
                                      //ApplicantAddressBn = u.UserAddressBn,
                                      ApplicantAddressBn = u.PostalAddressBn,
                                      ApplicantMobile = reg.UserMobile,
                                      ApplicantMobileBn = reg.UserMobile,
                                      ApplicantEmail = reg.UserEmail,
                                      ApplicantGroupName = ugl.UserGroupName
                                  }).FirstOrDefault();

            if (applicant_info != null)
            {
                ViewData["ApplicantName"] = applicant_info.ApplicantName;
                ViewData["ApplicantNameBn"] = applicant_info.ApplicantNameBn;
                ViewData["ApplicantAddress"] = applicant_info.ApplicantAddress;
                ViewData["ApplicantAddressBn"] = applicant_info.ApplicantAddressBn;
                ViewData["ApplicantMobile"] = applicant_info.ApplicantMobile;
                ViewData["ApplicantMobileBn"] = applicant_info.ApplicantMobileBn.NumberEnglishToBengali();
                ViewData["ApplicantEmail"] = applicant_info.ApplicantEmail;
                ViewData["ApplicantGroupName"] = applicant_info.ApplicantGroupName;
            }
        }
    }
}
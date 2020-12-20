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
            int result = 0;
            long _projectId = Request.Form["ProjectId"].ToString().ToLong();
            string _applicantEmail = Request.Form["ApplicantEmail"].ToString().Trim();
            string _hearingSubject = Request.Form["HearingSubject"].ToString().Trim();
            string _hearingMessage = Request.Form["HearingMessage"].ToString().Trim();

            try
            {
                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(_projectId);

                if (_pcd != null)
                {
                    EmailModel em = new EmailModel
                    {
                        To = _applicantEmail,
                        Subject = _hearingSubject,
                        Body = _hearingMessage,
                        Attachment = null
                    };

                    _es.Send(em);

                    ViewData["ProjectId"] = _pcd.ProjectId;
                    GetApplicantInfoViewData(_pcd.UserId);
                    ViewData["SuccessEmailSend"] = "success";
                }
            }
            catch(Exception ex)
            {
                ViewData["SuccessEmailSend"] = ex.Message;
            }

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
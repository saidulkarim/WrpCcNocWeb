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
using WrpCcNocWeb.Models.CcModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Matching;

namespace WrpCcNocWeb.Controllers
{
    public class certificateController : Controller
    {
        #region Initialization        
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private commonController cc = new commonController();
        private EmailService es = new EmailService();
        private Notification noti = new Notification();
        private readonly string rootDirOfProjFile = "../images";
        private readonly string rootDirOfDocs = "../docs";
        #endregion        

        public IActionResult index()
        {
            return View();
        }

        //certificate/view/59
        public IActionResult view(long? id, int? lang)
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
            AdminModUsersDetail ahai = GetAppHigherAuthInfo(id.Value);

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
                    LanguageId = lang ?? 0,// pcd.LanguageId ?? 0,
                    ClearanceNo = pcd.AppSubmissionId,
                    ClearanceNoBn = pcd.AppSubmissionId.ToString().NumberEnglishToBengali(),
                    //ClearanceDate = DateTime.Now.ToString("dd MMMM, yyyy"),
                    //ClearanceDateBn = DateTime.Now.ToString("dd MMMM, yyyy").NumberEnglishToBengali().MonthEnglishToBengali(),
                    ClearanceDate = pcd.AppApprovalDate.Value.ToString("dd MMMM, yyyy"),
                    ClearanceDateBn = pcd.AppApprovalDate.Value.ToString("dd MMMM, yyyy").NumberEnglishToBengali().MonthEnglishToBengali(),
                    ProjectType = pt,
                    FormNo = pt.ProjectTypeId.ToString().PadLeft(2, '0'),
                    FormNoBn = pt.ProjectTypeId.ToString().PadLeft(2, '0').NumberEnglishToBengali(),
                    CertificateFormat = cf,
                    HigherAuthSignature = ahai.HigherAuthSignature,
                    HigherAuthSeal = ahai.HigherAuthSeal
                };

                ViewData["WrpCcNocWebCertificate"] = crt;

                return new ViewAsPdf("~/Views/certificate/view.cshtml", viewData: ViewData)
                {
                    PageSize = Size.A4,
                    PageOrientation = Orientation.Portrait,
                    PageMargins = new Margins(10, 10, 10, 10)
                };
                //return View();
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Application Info", "Sorry, application information not found!");
                return RedirectToAction("list", "form");
            }
        }

        //certificate/undertaking
        public IActionResult undertaking(long? id, int? lang)
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

            LookUpCcModUndertakingFormat uf = _db.LookUpCcModUndertakingFormat.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).FirstOrDefault();
            if (uf == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Undertaking Format Error", "Sorry, undertaking template not found!");
                return RedirectToAction("list", "form");
            }

            ApplicantInfo ai = GetApplicantInfo(pcd.UserId);
            AdminModUsersDetail ahai = GetAppHigherAuthInfo(id.Value);

            if (ai != null)
            {
                var now_date_time = DateTime.Now;
                WrpCcNocUndertaking undertaking_info = new WrpCcNocUndertaking
                {
                    ProjectId = pcd.ProjectId,
                    ProjectTypeId = pcd.ProjectTypeId,
                    LanguageId = lang ?? 0,// pcd.LanguageId ?? 0,
                    ApplicantName = ai.ApplicantName,
                    ApplicantNameBn = ai.ApplicantNameBn,
                    ApplicantAddress = ai.ApplicantAddress,
                    ApplicantAddressBn = ai.ApplicantAddressBn,
                    LocationName = "CEGIS",
                    LocationNameBn = "সি.ই.জি.আই.এস ",

                    UndertakingDate = now_date_time.ToString("dd MMMM, yyyy"),
                    UndertakingDateBn = now_date_time.ToString("dd MMMM, yyyy").NumberEnglishToBengali().MonthEnglishToBengali(),
                    UndertakingTime = now_date_time.ToString("HH:mm:ss"),
                    UndertakingTimeBn = now_date_time.ToString("HH:mm:ss").NumberEnglishToBengali(),
                    UndertakingFormat = uf,
                    ApplicantSignatureFile = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault()
                };

                ViewData["WrpCcNocUndertaking"] = undertaking_info;
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Danger, "Application Info", "Sorry, application information not found!");
                return RedirectToAction("list", "form");
            }

            return View();
        }

        [HttpPost]
        public IActionResult undertaking()
        {
            int result = 0;
            long pid = Request.Form["ProjectId"].ToString().ToLong();
            int lid = Request.Form["LanguageId"].ToString().ToInt();

            if (pid > 0)
            {
                try
                {
                    CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(pid);

                    if (pcd != null)
                    {
                        pcd.IsCompletedId = 7;
                        pcd.UndertakingSubmitYesNoId = 1;
                        _db.Entry(pcd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Success, "Successfully Submitted", "Your undertaking has been successfully submitted!");
                            return RedirectToAction("list", "form");
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, "Not Submitted", "Your undertaking not submitted! Try again...");
                            return RedirectToAction("undertaking", "certificate", new { id = pid, lang = lid });
                        }
                    }
                    else
                    {
                        TempData["Message"] = ch.ShowMessage(Sign.Error, "Invalid Project", "Invalid project selected!");
                        return RedirectToAction("list", "form");
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Error, "Error Occured", ex.Message);
                    return RedirectToAction("list", "form");
                }
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Error, "Invalid Project", "Invalid project selected!");
                return RedirectToAction("list", "form");
            }

            return View();
        }

        #region Certificate Verification
        public IActionResult verification()
        {
            return View();
        }

        // POST: certificate/verification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> verification([Bind("AppSubmissionId,UserMobile,UserEmail")] CertificateVerify verify)
        {
            string result = string.Empty;
            List<string> vars = new List<string>();

            if (ModelState.IsValid)
            {
                CcModAppProjectCommonDetail commonDetail = _db.CcModAppProjectCommonDetail.Where(w => w.AppSubmissionId == verify.AppSubmissionId.ToLong()).FirstOrDefault();
                if (commonDetail == null)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Danger, "Tracking Number", "Sorry, you have entered wrong tracking number!");
                    return View(verify);
                }

                AdminModUserRegistrationDetail registrationDetail = _db.AdminModUserRegistrationDetail.Where(w => w.UserMobile == verify.UserMobile).FirstOrDefault();
                if (registrationDetail == null)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Danger, "Wrong Mobile Number", "Sorry, you have entered wrong mobile number!");
                    return View(verify);
                }

                registrationDetail = _db.AdminModUserRegistrationDetail.Where(w => w.UserEmail == verify.UserEmail).FirstOrDefault();
                if (registrationDetail == null)
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Danger, "Wrong Email Address", "Sorry, you have entered wrong email address!");
                    return View(verify);
                }

                #region Email Sending                
                if (commonDetail != null)
                {
                    var applicationInfoDetails = (from pcd in _db.CcModAppProjectCommonDetail
                                                  join pty in _db.LookUpCcModProjectType on pcd.ProjectTypeId equals pty.ProjectTypeId into type
                                                  from projecttype in type.DefaultIfEmpty()
                                                  join ast in _db.LookUpCcModApplicationState on pcd.ApplicationStateId equals ast.ApplicationStateId into state
                                                  from applicationstate in state.DefaultIfEmpty()
                                                  join aps in _db.LookUpCcModApprovalStatus on pcd.ApprovalStatusId equals aps.ApprovalStatusId into status
                                                  from approvalstatus in status.DefaultIfEmpty()
                                                  join ics in _db.LookUpCcModIsCompletedState on pcd.IsCompletedId equals ics.IsCompletedId into cs
                                                  from completedstate in cs.DefaultIfEmpty()

                                                  where pcd.AppSubmissionId == verify.AppSubmissionId.ToLong()

                                                  select new CertificateVerificationInfo
                                                  {
                                                      UserMobile = verify.UserMobile,
                                                      UserEmail = verify.UserEmail,

                                                      AppliedDate = pcd.AppSubmissionDate.Value.ToString("dd MMM yyyy"),
                                                      ProjectType = projecttype.ProjectType,
                                                      ProjectTitle = pcd.ProjectName,
                                                      TrackingNumber = verify.AppSubmissionId,
                                                      ApplicationState = string.IsNullOrEmpty(applicationstate.ApplicationState) ? "Not Found" : applicationstate.ApplicationState.Substring(applicationstate.ApplicationState.IndexOf("for") + 3, applicationstate.ApplicationState.Length - (applicationstate.ApplicationState.IndexOf("for") + 3)).Trim(),
                                                      ApprovalStatus = approvalstatus.ApprovalStatus,
                                                      ApprovalStage = completedstate.IsCompletedState,
                                                      RejectReason = pcd.ReasonOfRejection
                                                  }).FirstOrDefault();

                    if (applicationInfoDetails != null)
                    {
                        string callAt = cc.GetCallCenterInfo();

                        vars.Add(applicationInfoDetails.AppliedDate);
                        vars.Add(applicationInfoDetails.ProjectType);
                        vars.Add(applicationInfoDetails.ProjectTitle);
                        vars.Add(applicationInfoDetails.TrackingNumber);
                        vars.Add(applicationInfoDetails.ApplicationState);
                        vars.Add(applicationInfoDetails.ApprovalStatus);

                        if (applicationInfoDetails.ApprovalStatus == "Approved")
                        {
                            vars.Add("<span style='color: green; font-weight: bold;'>Certificate is ready to download</span>");
                        }
                        else if (applicationInfoDetails.ApprovalStatus == "Not Approved")
                        {
                            vars.Add("Certificate is not available to download");
                        }
                        else if (applicationInfoDetails.ApprovalStatus == "Rejected")
                        {
                            vars.Add("<span style='color: red; font-weight: bold;'>" + applicationInfoDetails.RejectReason + "</span>");
                        }
                        else
                        {
                            vars.Add("Certificate is not yet ready to download");
                        }

                        //vars.Add(applicationInfoDetails.ApprovalStage);
                        vars.Add(callAt);

                        //2 => Certificate Verification in LookUpAdminModEmailFormat table
                        result = es.SendEmail(applicationInfoDetails.UserEmail, 2, vars);

                        if (result == "success")
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Success, "Success", "An email has been sent to your email address.");
                        }
                        else
                        {
                            TempData["Message"] = ch.ShowMessage(Sign.Error, "An Error Occured", "Email not sent to your email address." + result);
                        }
                    }
                }
                #endregion                
            }

            return View(verify);
        }
        #endregion

        private ApplicantInfo GetApplicantInfo(long userID)
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

        private AdminModUsersDetail GetAppHigherAuthInfo(long projectId)
        {
            AdminModUsersDetail haud = new AdminModUsersDetail();

            try
            {
                CcModAppProjectCommonDetail pCommonDetail = _db.CcModAppProjectCommonDetail.Find(projectId);
                LookUpCcModApplicationState pApplicationState = _db.LookUpCcModApplicationState.Find(pCommonDetail.ApplicationStateId);
                LookUpAdminModUserGroup pUserGroup = _db.LookUpAdminModUserGroup.Where(w => w.AuthorityLevelId == pApplicationState.AuthorityLevelId || w.UserGroupId == pApplicationState.UserGroupId).FirstOrDefault();
                AdminModUserGrpDistDetail pUserGrpDistDetail = _db.AdminModUserGrpDistDetail.Where(w => w.UserGroupId == pUserGroup.UserGroupId).FirstOrDefault();
                haud = _db.AdminModUsersDetail.Find(pUserGrpDistDetail.UserId);
            }
            catch (Exception ex)
            {
                haud = new AdminModUsersDetail();
            }

            return haud;
        }
    }
}
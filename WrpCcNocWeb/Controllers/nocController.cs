using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.TempModels;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.Utility;
using WrpCcNocWeb.ViewModels;
using static WrpCcNocWeb.Helpers.CommonHelper;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using Microsoft.CodeAnalysis;
using System.Drawing;

namespace WrpCcNocWeb.Controllers
{
    public class nocController : Controller
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

        private readonly IWebHostEnvironment hostingEnvironment;

        public nocController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult index()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            ViewData["Title"] = "Apply";
            ViewBag.LookUpAdminModLanguage = _db.LookUpAdminModLanguage.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult index(SelectedForm _selectedForm)
        {
            if (!string.IsNullOrEmpty(_selectedForm.LanguageTypeId))
            {
                HttpContext.Session.SetComplexData("SelectedForm", _selectedForm);
                HttpContext.Response.Cookies.Append("FormLanguage", string.Empty, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(-1)
                });

                HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(1)
                });

                return RedirectToAction("apply", "noc"); //NOC for Installation of tube well (Deep/Shallow)
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Required", "Please select a language.");
            }

            ViewData["Title"] = _selectedForm.LanguageTypeId == "1" ? "নলকূপ স্থাপনের নিমিত্তে অনাপত্তির জন্য আবেদন" : "NOC for Installation of Tube Well (Deep/ Shallow)";
            ViewBag.LookUpAdminModLanguage = _db.LookUpAdminModLanguage.ToList();
            return View();
        }

        #region Form: NOC for Installation of tube well (Deep/Shallow)
        //noc/apply   
        public IActionResult apply()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "noc");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "নলকূপ স্থাপনের নিমিত্তে অনাপত্তির জন্য আবেদন" : "NOC for Installation of Tube Well (Deep/ Shallow)";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            LoadDropdownData();
            /*
            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;

                //LoadDropdownData();
                //FcmpNewEmptyForm();

                //TempData["Message"] = ch.ShowMessage(Sign.Info, "Information", "Please enter new project information.");                
            }
            else
            {                
                LoadDropdownData();

                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(_psi.ProjectId);

                if (_pcd != null)
                    ViewBag.ProjectCommonDetail = _pcd;
                else
                    ViewBag.ProjectCommonDetail = new CcModAppProjectCommonDetail();

                CcModPrjLocationDetail _locationdetail = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();

                if (_locationdetail != null)
                    ViewBag.ProjectLocationDetail = _locationdetail;
                else
                    ViewBag.ProjectLocationDetail = new CcModPrjLocationDetail();

                CcModAppProject_31_IndvDetail _indvdetail = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();

                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail31 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail31 = new CcModAppProject_31_IndvDetail();

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                var _typesofflood = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.FloodTypeDetailId, x.ProjectId, x.FloodTypeId }).ToList();
                ViewBag.TypesOfFloodDetail = _typesofflood;

                var _compatnwpdetail = _db.CcModPrjCompatNWPDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new
                                       {
                                           x.PrjCompatNWPId,
                                           x.ProjectId,
                                           x.NationalWaterPolicyArticleId,
                                           x.LookUpCcModNWPArticle.NationalWtrPolicyShortTitle,
                                           x.LookUpCcModNWPArticle.NationalWtrPolicyArticleTitle
                                       }).ToList();
                ViewBag.CompatNWPDetail = _compatnwpdetail;

                var _compatnwmpdetail = _db.CcModPrjCompatNWMPDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.PrjCompatNWMPId, x.ProjectId, x.NWMPProgrammeId, x.LookUpCcModNWMPProgramme.NWMPProgrammeTitle }).ToList();
                ViewBag.CompatNWMPDetail = _compatnwmpdetail;

                var _compatsdgdetail = _db.CcModPrjCompatSDGDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.SDGCompabilityId, x.ProjectId, x.SDGGoalId }).ToList();
                ViewBag.CompatSDGDetail = _compatsdgdetail;

                var _compatsdgindidetail = _db.CcModPrjCompatSDGIndiDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.SDGIndicatorDetailId, x.ProjectId, x.SDGIndicatorId }).ToList();
                ViewBag.CompatSDGIndiDetail = _compatsdgindidetail;

                var _bdp2100goaldetail = _db.CcModBDP2100GoalDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.DeltaGoalDetailId, x.ProjectId, x.DeltPlan2100GoalId }).ToList();
                ViewBag.BDP2100GoalDetail = _bdp2100goaldetail;

                var _gpwmgrouptype = _db.CcModGPWMGroupTypeDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                       .Select(x => new { x.GPWMGroupTypeDetailId, x.ProjectId, x.GPWMGroupTypeId }).ToList();
                ViewBag.GPWMGroupType = _gpwmgrouptype;                
            }
            */

            GetApplicantInfo(ui.UserID);

            return View();
        }

        //noc/apply
        [HttpPost]
        public IActionResult apply(long id)
        {
            /*
            ProjectStatusInfo _pi = GetProjectInfoById(id);
            int result = 0, appState = 0;

            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            if (_pi.AppSubmissionId == 0)
            {
                appState = _pi.ApplicationStateId;

                if (_pi.ApplicationStateId == 1)
                {
                    //Step 1: Project Estimated Cost Range
                    #region Finding Application State from Cost Range
                    appState = GetProjectCostRangeState(id);
                    #endregion

                    //Step 2: Project Multiple Location
                    #region Multiple Location Checking
                    appState = GetProjectMultipleLocation(id);
                    #endregion

                    //Step 3: Payment Method
                    #region Payment Method Checking
                    //payment method code will be incorporate here
                    #endregion

                    //Step 4: Mandatory File Attachment
                    #region Mandatory File Attachment Checking
                    //mandatory file attachment code will be incorporate here
                    #endregion

                    string projectTrackingCode = id.ToString().GenerateTrackingNumber(6);

                    if (!string.IsNullOrEmpty(projectTrackingCode))
                    {
                        using var dbContextTransaction = _db.Database.BeginTransaction();
                        try
                        {
                            CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(id);

                            if (_pcd != null)
                            {
                                _pcd.AppSubmissionId = projectTrackingCode.ToLong();
                                _pcd.AppSubmissionDate = DateTime.Now;
                                _pcd.ApplicationStateId = appState;
                                _pcd.IsCompletedId = 2;
                                _pcd.ReviewCycleNo = 0;

                                _db.Entry(_pcd).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    try
                                    {
                                        List<string> vars = new List<string>();

                                        #region Email
                                        string callAt = cc.GetCallCenterInfo();
                                        vars.Add(projectTrackingCode);
                                        vars.Add(_pcd.AppSubmissionDate.Value.ToString("dd MMM, yyyy"));
                                        vars.Add(_pcd.AppSubmissionDate.Value.AddDays(90).ToString("dd MMM, yyyy"));
                                        vars.Add(callAt);
                                        es.SendEmail(ui.UserEmail, 4, vars);
                                        #endregion

                                        #region SMS
                                        vars = new List<string>();
                                        vars.Add(projectTrackingCode);
                                        ss.Send(1, ui.UserID, vars);
                                        #endregion
                                    }
                                    catch (Exception ex)
                                    {
                                        noti = new Notification
                                        {
                                            id = id.ToString(),
                                            status = "warning",
                                            title = "Exception",
                                            message = ex.Message
                                        };
                                    }

                                    noti = new Notification
                                    {
                                        id = id.ToString(),
                                        status = "success",
                                        title = "Success",
                                        message = "Your application has been successfully submitted to higher authority. Application tracking code: " + projectTrackingCode
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = id.ToString(),
                                        status = "error",
                                        title = "Application Submission Error",
                                        message = "Your application not submitted."
                                    };
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();

                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = "0",
                                status = "error",
                                title = "An Exception Error Occured",
                                message = message
                            };
                        }
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = id.ToString(),
                        status = "warning",
                        title = "Application Current State",
                        message = "Your application already submitted. " +
                                  "\n<br />Application current state: <b>" + _pi.ApplicationState + ".</b>" +
                                  "\nApplication tracking number:" + _pi.AppSubmissionId
                        //"\n<br />Approval status: " + _pi.ApprovalStatus
                    };
                }
            }
            else
            {
                noti = new Notification
                {
                    id = id.ToString(),
                    status = "warning",
                    title = "Already Submitted",
                    message = "Your application already submitted. Application tracking number: " + _pi.AppSubmissionId
                };
            }
            */

            return Json(noti);
        }

        private void LoadDropdownData()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();
            ViewBag.LookUpAdminBndUpazila = _db.LookUpAdminBndUpazila.ToList();
            ViewBag.LookUpAdminBndUnion = _db.LookUpAdminBndUnion.ToList();
            ViewBag.LookUpAdminBndMouza = _db.LookUpAdminBndMouza.ToList();

            ViewBag.LookUpNocUserType = _db.LookUpNocUserType.ToList();
            ViewBag.LookUpNocAppMode = _db.LookUpNocAppMode.ToList();
            ViewBag.LookUpNocAppObjective = _db.LookUpNocAppObjective.ToList();
            ViewBag.LookUpNocWithdrawalQuantity = _db.LookUpNocWithdrawalQuantity.ToList();
            ViewBag.LookUpNocAuthority = _db.LookUpNocAuthority.ToList();
            ViewBag.LookUpNocMonth = _db.LookUpCcModMonth.ToList();

            ViewBag.LookUpCcModNWPArticle = _db.LookUpCcModNWPArticle.ToList();
            ViewBag.LookUpCcModNWMPProgramme = _db.LookUpCcModNWMPProgramme.ToList();
            ViewBag.LookUpCcModSDGGoal = _db.LookUpCcModSDGGoal.ToList();
            ViewBag.LookUpCcModSDGIndicator = _db.LookUpCcModSDGIndicator.ToList();
            ViewBag.LookUpCcModDeltPlan2100Goal = _db.LookUpCcModDeltPlan2100Goal.ToList();
            ViewBag.LookUpCcModGPWMGroupType = _db.LookUpCcModGPWMGroupType.ToList();
            ViewBag.RecommendationId = _db.LookUpCcModRecommendation.ToList();
            #endregion
        }

        private void FcmpNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail();
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_31_IndvDetail _indvdetail = new CcModAppProject_31_IndvDetail();
            ViewBag.ProjectIndvDetail31 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;
            List<CcModPrjTypesOfFloodDetail> _typesofflood = new List<CcModPrjTypesOfFloodDetail>();
            ViewBag.TypesOfFloodDetail = _typesofflood;
            List<CcModPrjCompatNWPDetail> _compatnwpdetail = new List<CcModPrjCompatNWPDetail>();
            ViewBag.CompatNWPDetail = _compatnwpdetail;
            List<CcModPrjCompatNWMPDetail> _compatnwmpdetail = new List<CcModPrjCompatNWMPDetail>();
            ViewBag.CompatNWMPDetail = _compatnwmpdetail;
            List<CcModPrjCompatSDGDetail> _compatsdgdetail = new List<CcModPrjCompatSDGDetail>();
            ViewBag.CompatSDGDetail = _compatsdgdetail;
            List<CcModPrjCompatSDGIndiDetail> _compatsdgindidetail = new List<CcModPrjCompatSDGIndiDetail>();
            ViewBag.CompatSDGIndiDetail = _compatsdgindidetail;
            List<CcModBDP2100GoalDetail> _bdp2100goaldetail = new List<CcModBDP2100GoalDetail>();
            ViewBag.BDP2100GoalDetail = _bdp2100goaldetail;
            List<CcModGPWMGroupTypeDetail> _gpwmgrouptype = new List<CcModGPWMGroupTypeDetail>();
            ViewBag.GPWMGroupType = _gpwmgrouptype;
        }
        #endregion

        public IActionResult list()
        {
            return View();
        }

        public void GetApplicantInfo(long userID)
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
                ViewBag.ApplicantName = applicant_info.ApplicantName;
                ViewBag.ApplicantNameBn = applicant_info.ApplicantNameBn;
                ViewBag.ApplicantAddress = applicant_info.ApplicantAddress;
                ViewBag.ApplicantAddressBn = applicant_info.ApplicantAddressBn;
                ViewBag.ApplicantMobile = applicant_info.ApplicantMobile;
                ViewBag.ApplicantMobileBn = applicant_info.ApplicantMobileBn.NumberEnglishToBengali();
                ViewBag.ApplicantEmail = applicant_info.ApplicantEmail;
                ViewBag.ApplicantGroupName = applicant_info.ApplicantGroupName;
            }
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
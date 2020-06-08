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
using System.Runtime.InteropServices.WindowsRuntime;

namespace WrpCcNocWeb.Controllers
{
    public class formController : Controller
    {
        #region Initialization
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        private readonly string rootDirOfProjFile = "../images";
        private readonly string rootDirOfDocs = "../docs";
        #endregion

        private readonly IWebHostEnvironment hostingEnvironment;

        public formController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult file()
        {
            return View();
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
            if (!string.IsNullOrEmpty(_selectedForm.ProjectTypeId))
            {
                HttpContext.Session.SetComplexData("SelectedForm", _selectedForm);
                //ViewBag.ProjectTypeId = new SelectList(_db.LookUpCcModProjectType.ToList(), "ProjectTypeId", "ProjectType", _selectedForm.ProjectTypeId);
                //ViewBag.ProjectTypeBnId = new SelectList(_db.LookUpCcModProjectType.ToList(), "ProjectTypeId", "ProjectTypeBn", _selectedForm.ProjectTypeId);

                HttpContext.Response.Cookies.Append("FormLanguage", string.Empty, new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(-1)
                });

                if (_selectedForm.ProjectTypeId.Equals("1"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("fcmp", "form");
                }
                else if (_selectedForm.ProjectTypeId.Equals("2"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("swwdu", "form");
                }
                else
                {
                    TempData["Message"] = ch.ShowMessage(Sign.Warning, "Under Development", "Sorry, select project is under development.");
                }
            }
            else
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Required", "Please select a form to apply.");
            }

            ViewData["Title"] = "Apply";
            ViewBag.LookUpAdminModLanguage = _db.LookUpAdminModLanguage.ToList();
            return View();
        }

        public IActionResult list()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            List<ProjectViewList> pvl = new List<ProjectViewList>();

            if (uli.UserGroupId == 1000000001 && uli.AuthorityLevelId == 0)
            {
                pvl = (from p in _db.CcModAppProjectCommonDetail.Where(w => w.UserId == ui.UserID)
                       join t in _db.LookUpCcModProjectType on p.ProjectTypeId equals t.ProjectTypeId into tGroup
                       from pt in tGroup.DefaultIfEmpty()
                       join s in _db.LookUpCcModApplicationState on p.ApplicationStateId equals s.ApplicationStateId into sGroup
                       from st in sGroup.DefaultIfEmpty()
                       join a in _db.LookUpCcModApprovalStatus on p.ApprovalStatusId equals a.ApprovalStatusId into aGroup
                       from ap in aGroup.DefaultIfEmpty()

                       select new ProjectViewList
                       {
                           ProjectId = p.ProjectId,
                           ProjectTypeId = p.ProjectTypeId,
                           ProjectType = pt.ProjectType,
                           ProjectName = p.ProjectName,
                           ProjectEstimatedCost = p.ProjectEstimatedCost,
                           ApplicationStateId = p.ApplicationStateId,
                           ApplicationState = st.ApplicationState,
                           ApprovalStatusId = p.ApprovalStatusId ?? 0,
                           ApprovalStatus = !string.IsNullOrEmpty(ap.ApprovalStatus) ? ap.ApprovalStatus : "Pending",
                           IsCompleted = p.IsCompletedId
                       }).ToList();
            }
            else if (uli.UserGroupId != 1000000001 && uli.AuthorityLevelId == 0)
            {
                pvl = (from p in _db.CcModAppProjectCommonDetail
                       join t in _db.LookUpCcModProjectType on p.ProjectTypeId equals t.ProjectTypeId into tGroup
                       from pt in tGroup.DefaultIfEmpty()
                       join s in _db.LookUpCcModApplicationState on p.ApplicationStateId equals s.ApplicationStateId into sGroup
                       from st in sGroup.DefaultIfEmpty()
                       join a in _db.LookUpCcModApprovalStatus on p.ApprovalStatusId equals a.ApprovalStatusId into aGroup
                       from ap in aGroup.DefaultIfEmpty()

                       select new ProjectViewList
                       {
                           ProjectId = p.ProjectId,
                           ProjectTypeId = p.ProjectTypeId,
                           ProjectType = pt.ProjectType,
                           ProjectName = p.ProjectName,
                           ProjectEstimatedCost = p.ProjectEstimatedCost,
                           ApplicationStateId = p.ApplicationStateId,
                           ApplicationState = st.ApplicationState,
                           ApprovalStatusId = p.ApprovalStatusId ?? 0,
                           ApprovalStatus = !string.IsNullOrEmpty(ap.ApprovalStatus) ? ap.ApprovalStatus : "Pending",
                           IsCompleted = p.IsCompletedId
                       }).ToList();
            }
            else
            {
                List<long> ProjectList = new List<long>();

                if (!string.IsNullOrEmpty(uli.UnionGeoCode))
                {
                    ProjectList = new List<long>();
                    ProjectList = _db.CcModPrjLocationDetail.Where(w => w.UnionGeoCode == uli.UnionGeoCode).Select(s => s.ProjectId).Distinct().ToList();
                    pvl = (from p in _db.CcModAppProjectCommonDetail.Where(w => w.ApplicationStateId == uli.ApplicationStateId && ProjectList.Any(a => w.ProjectId == a.ToString().ToLong()))
                           join t in _db.LookUpCcModProjectType on p.ProjectTypeId equals t.ProjectTypeId into tGroup
                           from pt in tGroup.DefaultIfEmpty()
                           join s in _db.LookUpCcModApplicationState on p.ApplicationStateId equals s.ApplicationStateId into sGroup
                           from st in sGroup.DefaultIfEmpty()
                           join a in _db.LookUpCcModApprovalStatus on p.ApprovalStatusId equals a.ApprovalStatusId into aGroup
                           from ap in aGroup.DefaultIfEmpty()

                           select new ProjectViewList
                           {
                               ProjectId = p.ProjectId,
                               ProjectTypeId = p.ProjectTypeId,
                               ProjectType = pt.ProjectType,
                               ProjectName = p.ProjectName,
                               ProjectEstimatedCost = p.ProjectEstimatedCost,
                               ApplicationStateId = p.ApplicationStateId,
                               ApplicationState = st.ApplicationState,
                               ApprovalStatusId = p.ApprovalStatusId ?? 0,
                               ApprovalStatus = !string.IsNullOrEmpty(ap.ApprovalStatus) ? ap.ApprovalStatus : "Pending",
                               IsCompleted = p.IsCompletedId
                           }).ToList();
                }

                if (!string.IsNullOrEmpty(uli.UpazilaGeoCode) && string.IsNullOrEmpty(uli.UnionGeoCode))
                {
                    ProjectList = new List<long>();
                    ProjectList = _db.CcModPrjLocationDetail.Where(w => w.UpazilaGeoCode == uli.UpazilaGeoCode).Select(s => s.ProjectId).Distinct().ToList();
                    pvl = (from p in _db.CcModAppProjectCommonDetail.Where(w => w.ApplicationStateId == uli.ApplicationStateId && ProjectList.Any(a => w.ProjectId == a.ToString().ToLong()))
                           join t in _db.LookUpCcModProjectType on p.ProjectTypeId equals t.ProjectTypeId into tGroup
                           from pt in tGroup.DefaultIfEmpty()
                           join s in _db.LookUpCcModApplicationState on p.ApplicationStateId equals s.ApplicationStateId into sGroup
                           from st in sGroup.DefaultIfEmpty()
                           join a in _db.LookUpCcModApprovalStatus on p.ApprovalStatusId equals a.ApprovalStatusId into aGroup
                           from ap in aGroup.DefaultIfEmpty()

                           select new ProjectViewList
                           {
                               ProjectId = p.ProjectId,
                               ProjectTypeId = p.ProjectTypeId,
                               ProjectType = pt.ProjectType,
                               ProjectName = p.ProjectName,
                               ProjectEstimatedCost = p.ProjectEstimatedCost,
                               ApplicationStateId = p.ApplicationStateId,
                               ApplicationState = st.ApplicationState,
                               ApprovalStatusId = p.ApprovalStatusId ?? 0,
                               ApprovalStatus = !string.IsNullOrEmpty(ap.ApprovalStatus) ? ap.ApprovalStatus : "Pending",
                               IsCompleted = p.IsCompletedId
                           }).ToList();
                }

                if (!string.IsNullOrEmpty(uli.DistrictGeoCode) && string.IsNullOrEmpty(uli.UpazilaGeoCode) && string.IsNullOrEmpty(uli.UnionGeoCode))
                {
                    ProjectList = new List<long>();
                    ProjectList = _db.CcModPrjLocationDetail.Where(w => w.DistrictGeoCode == uli.DistrictGeoCode).Select(s => s.ProjectId).Distinct().ToList();
                    pvl = (from p in _db.CcModAppProjectCommonDetail.Where(w => w.ApplicationStateId == uli.ApplicationStateId && ProjectList.Any(a => w.ProjectId == a.ToString().ToLong()))
                           join t in _db.LookUpCcModProjectType on p.ProjectTypeId equals t.ProjectTypeId into tGroup
                           from pt in tGroup.DefaultIfEmpty()
                           join s in _db.LookUpCcModApplicationState on p.ApplicationStateId equals s.ApplicationStateId into sGroup
                           from st in sGroup.DefaultIfEmpty()
                           join a in _db.LookUpCcModApprovalStatus on p.ApprovalStatusId equals a.ApprovalStatusId into aGroup
                           from ap in aGroup.DefaultIfEmpty()

                           select new ProjectViewList
                           {
                               ProjectId = p.ProjectId,
                               ProjectTypeId = p.ProjectTypeId,
                               ProjectType = pt.ProjectType,
                               ProjectName = p.ProjectName,
                               ProjectEstimatedCost = p.ProjectEstimatedCost,
                               ApplicationStateId = p.ApplicationStateId,
                               ApplicationState = st.ApplicationState,
                               ApprovalStatusId = p.ApprovalStatusId ?? 0,
                               ApprovalStatus = !string.IsNullOrEmpty(ap.ApprovalStatus) ? ap.ApprovalStatus : "Pending",
                               IsCompleted = p.IsCompletedId
                           }).ToList();
                }
            }

            ViewBag.PCDS = pvl;
            return View();
        }

        public IActionResult view(long id, int status)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewBag.UserLevel = uli.UserGroupId;
            ViewBag.UserAuthLevelID = uli.AuthorityLevelId;
            ViewBag.HigherAuthLevelID = GetHighestLevelAuthority();
            ChangeStatus(id, status);

            CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(id);

            if (pcd != null)
                ViewBag.ProjectCommonDetail = pcd;
            else
                ViewBag.ProjectCommonDetail = new CcModAppProjectCommonDetail();

            CcModPrjLocationDetail _locationdetail = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();

            if (_locationdetail != null)
                ViewBag.ProjectLocationDetail = _locationdetail;
            else
                ViewBag.ProjectLocationDetail = new CcModPrjLocationDetail();

            CcModAppProject_31_IndvDetail _indvdetail = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();

            if (_indvdetail != null)
                ViewBag.ProjectIndvDetail31 = _indvdetail;
            else
                ViewBag.ProjectIndvDetail31 = new CcModAppProject_31_IndvDetail();

            var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                        .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
            ViewBag.HydroRegionDetail = _hydroregiondetail;

            var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

            var _typesofflood = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                   .Select(x => new { x.FloodTypeDetailId, x.ProjectId, x.FloodTypeId }).ToList();
            ViewBag.TypesOfFloodDetail = _typesofflood;

            var _compatnwpdetail = _db.CcModPrjCompatNWPDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                   .Select(x => new { x.PrjCompatNWPId, x.ProjectId, x.NationalWaterPolicyArticleId }).ToList();
            ViewBag.CompatNWPDetail = _compatnwpdetail;

            var _compatnwmpdetail = _db.CcModPrjCompatNWMPDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                   .Select(x => new { x.PrjCompatNWMPId, x.ProjectId, x.NWMPProgrammeId }).ToList();
            ViewBag.CompatNWMPDetail = _compatnwmpdetail;

            var _compatsdgdetail = _db.CcModPrjCompatSDGDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                   .Select(x => new { x.SDGCompabilityId, x.ProjectId, x.SDGGoalId }).ToList();
            ViewBag.CompatSDGDetail = _compatsdgdetail;

            var _compatsdgindidetail = _db.CcModPrjCompatSDGIndiDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                   .Select(x => new { x.SDGIndicatorDetailId, x.ProjectId, x.SDGIndicatorId }).ToList();
            ViewBag.CompatSDGIndiDetail = _compatsdgindidetail;

            var _bdp2100goaldetail = _db.CcModBDP2100GoalDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                   .Select(x => new { x.DeltaGoalDetailId, x.ProjectId, x.DeltPlan2100GoalId }).ToList();
            ViewBag.BDP2100GoalDetail = _bdp2100goaldetail;

            var _gpwmgrouptype = _db.CcModGPWMGroupTypeDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                   .Select(x => new { x.GPWMGroupTypeDetailId, x.ProjectId, x.GPWMGroupTypeId }).ToList();
            ViewBag.GPWMGroupType = _gpwmgrouptype;

            ViewBag.ProjectId = pcd.ProjectId;
            ViewBag.Project31IndvId = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project31IndvId).FirstOrDefault();
            ViewBag.UserId = ui.UserID;
            ViewBag.ProjectTypeId = pcd.ProjectTypeId;
            ViewBag.ProjectTypeTitle = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewBag.ProjectTypeTitleBn = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewBag.LanguageId = pcd.LanguageId;
            GetApplicantInfo(pcd.UserId);

            return View();
        }

        public IActionResult printfcmp(long id, int status)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();
            ChangeStatus(id, status);

            CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(id);

            if (pcd != null)
                ViewData["ProjectCommonDetail"] = pcd;
            else
                ViewData["ProjectCommonDetail"] = new CcModAppProjectCommonDetail();

            List<ProjectLocationTemp> _locationdetail = GetProjectLocation(pcd.ProjectId);
            if (_locationdetail.Count > 0)
                ViewData["ProjectLocationDetail"] = _locationdetail;
            else
                ViewData["ProjectLocationDetail"] = new List<CcModPrjLocationDetail>();

            CcModAppProject_31_IndvDetail _indvdetail = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail31"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail31"] = new CcModAppProject_31_IndvDetail();

            var _hydroregiondetail = GetHydrologicalRegion(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["HydroRegionDetail"] = _hydroregiondetail;

            var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModDeltPlan2100HotSpot)
                                    .Select(x => new BDP2100HotSpotDetailTemp
                                    {
                                        DeltaPlanHotSpotId = x.DeltaPlanHotSpotId,
                                        PlanName = x.LookUpCcModDeltPlan2100HotSpot.PlanName,
                                        PlanNameBn = x.LookUpCcModDeltPlan2100HotSpot.PlanNameBn
                                    }).ToList();
            ViewData["BDP2100HotSpotDetail"] = _hotspotdetail;

            var _hydrosystemdetail = GetHydroSystemDetail(pcd.ProjectId);
            ViewData["HydroSystemDetail"] = _hydrosystemdetail;

            var _typesofflood = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModTypeOfFlood)
                                   .Select(x => new TypesOfFloodTemp
                                   {
                                       FloodTypeDetailId = x.FloodTypeDetailId,
                                       ProjectId = x.ProjectId,
                                       FloodTypeId = x.FloodTypeId,
                                       FloodTypeName = x.LookUpCcModTypeOfFlood.FloodTypeName,
                                       FloodTypeNameBn = x.LookUpCcModTypeOfFlood.FloodTypeNameBn
                                   }).ToList();
            ViewData["TypesOfFloodDetail"] = _typesofflood;

            var _floodfrequencydetail = GetFloodFrequencyDetail(pcd.ProjectId);
            ViewData["FloodFrequencyDetail"] = _floodfrequencydetail;

            var _irrigcropareadetail = GetIrrigCropAreaDetail(pcd.ProjectId);
            ViewData["IrrigCropAreaDetail"] = _irrigcropareadetail;

            var _analyzeoptionsdetail = GetAnalyzeOptionsDetail(pcd.ProjectId);
            ViewData["AnalyzeOptionsDetail"] = _analyzeoptionsdetail;

            var _designsubmitdetail = GetDesignSubmitDetail(pcd.ProjectId);
            ViewData["DesignSubmitDetail"] = _designsubmitdetail;

            var _ecofinanalysisdetail = GetEcoFinAnalysisDetail(pcd.ProjectId);
            ViewData["EcoFinAnalysisDetail"] = _ecofinanalysisdetail;

            var _eiadetail = GetEiaDetailTemp(pcd.ProjectId);
            ViewData["EiaDetailTemp"] = _eiadetail;

            var _siadetail = GetSiaDetailTemp(pcd.ProjectId);
            ViewData["SiaDetailTemp"] = _siadetail;

            var _compatnwpdetail = GetCompatNWPDetail(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["CompatNWPDetail"] = _compatnwpdetail;

            var _compatnwmpdetail = GetCompatNWMPDetail(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["CompatNWMPDetail"] = _compatnwmpdetail;

            var _compatsdgdetail = GetCompatSDGDetail(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["CompatSDGDetail"] = _compatsdgdetail;

            var _compatsdgindidetail = GetCompatSDGIndicatorDetail(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["CompatSDGIndiDetail"] = _compatsdgindidetail;

            var _bdp2100goaldetail = GetBDP2100GoalDetail(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["BDP2100GoalDetail"] = _bdp2100goaldetail;

            var _gpwmgrouptype = GetGPWMGroupTypeDetail(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["GPWMGroupType"] = _gpwmgrouptype;

            var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project31IndvId"] = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project31IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Flood Control Management Project | Print";
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();

            GetApplicantInfoViewData(pcd.UserId);

            //return View();
            return new ViewAsPdf("~/Views/form/printfcmp.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
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
                                      ApplicantName = u.UserFullName,
                                      ApplicantNameBn = u.UserFullNameBn,
                                      ApplicantAddress = u.UserAddress,
                                      ApplicantAddressBn = u.UserAddressBn,
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
                                      ApplicantName = u.UserFullName,
                                      ApplicantNameBn = u.UserFullNameBn,
                                      ApplicantAddress = u.UserAddress,
                                      ApplicantAddressBn = u.UserAddressBn,
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

        public int ChangeStatus(long id, int status)
        {
            CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(id);
            int result = 0;

            if (pcd != null)
            {
                if (status == 0)
                {
                    pcd.IsCompletedId = 1;
                }
                else if (status == 1)
                {
                    pcd.IsCompletedId = 2;
                }

                _db.Entry(pcd).State = EntityState.Modified;
                result = _db.SaveChanges();
            }

            return result;
        }

        public IActionResult status()
        {
            return View();
        }

        //form/FloodControlManagementProject :: fcmp       
        public IActionResult fcmp()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "বন্যা নিয়ন্ত্রন বা ব্যবস্থাপনা প্রকল্প" : "Flood Control Management Project";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString());

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;

                LoadDropdownData();
                FcmpNewEmptyForm();

                //TempData["Message"] = ch.ShowMessage(Sign.Info, "Information", "Please enter new project information.");                
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
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

            GetApplicantInfo(ui.UserID);
            return View();
        }

        //form/FloodControlManagementProject :: fcmp
        [HttpPost]
        public IActionResult fcmp(long id)
        {
            ProjectStatusInfo _pi = GetProjectInfoById(id);
            int result = 0, appState = 0;

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

                    //Step 3: Mandatory File Attachment
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
                                _pcd.ApplicationStateId = appState;

                                _db.Entry(_pcd).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = id.ToString(),
                                        status = "success",
                                        title = "Success",
                                        message = "Your application has been successfully submitted. Application tracking code: " + projectTrackingCode
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

            return Json(noti);
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

        //form/SurfaceWaterWithdrawalDistributionUse :: swwdu
        public IActionResult sw_temp()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "ভূপরিস্থ পানি সংক্রান্ত প্রকল্প" : "Project for Surface Water";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString());

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;

                LoadDropdownData();
                SwwduNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
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

            GetApplicantInfo(ui.UserID);
            return View();
        }

        //form/SurfaceWaterWithdrawalDistributionUse :: swwdu
        public IActionResult swwdu()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "ভূপরিস্থ পানি সংক্রান্ত প্রকল্প" : "Project for Surface Water";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString());

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;

                LoadDropdownData();
                SwwduNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
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

            GetApplicantInfo(ui.UserID);
            return View();
        }

        //form/SurfaceWaterWithdrawalDistributionUse :: swwdu
        [HttpPost]
        public IActionResult swwdu(long id)
        {
            return View();
        }

        private void SwwduNewEmptyForm()
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

        private int GetProjectCostRangeState(long projectId)
        {
            double ProjectEstimatedCost = _db.CcModAppProjectCommonDetail
                                             .Where(w => w.ProjectId == projectId)
                                             .Select(s => s.ProjectEstimatedCost)
                                             .FirstOrDefault();

            int ApplicationState = _db.LookUpAdminModCostRange
                                      .Where(w => (w.LowerValue < ProjectEstimatedCost.ToString().ToDecimal() || w.LowerValue == ProjectEstimatedCost.ToString().ToDecimal()) &&
                                                  (w.UpperValue > ProjectEstimatedCost.ToString().ToDecimal() || w.UpperValue == ProjectEstimatedCost.ToString().ToDecimal()))
                                      .Select(s => s.StateId)
                                      .FirstOrDefault();

            return ApplicationState;
        }
        
        private int GetProjectMultipleLocation(long projectId)
        {
            int appState = 0;
            int tUnion = 0, tUpazila = 0, tDistrict = 0;
            List<CcModPrjLocationDetail> _projLocation = _db.CcModPrjLocationDetail
                                                            .Where(w => w.ProjectId == projectId)
                                                            .ToList();

            if (_projLocation.Count > 0)
            {
                tUnion = _projLocation.Where(w => w.UnionGeoCode != string.Empty)
                                      .DistinctBy(d => d.UnionGeoCode)
                                      .Select(s => s.UnionGeoCode).Count();

                tUpazila = _projLocation.Where(w => w.UpazilaGeoCode != string.Empty)
                                        .DistinctBy(d => d.UpazilaGeoCode)
                                        .Select(s => s.UpazilaGeoCode).Count();

                tDistrict = _projLocation.Where(w => w.DistrictGeoCode != string.Empty)
                                         .DistinctBy(d => d.DistrictGeoCode)
                                         .Select(s => s.DistrictGeoCode).Count();

                if (tUnion > 1)
                {
                    appState = 21; //Pending for Review of Upazila Water Resources Management Committee. Old => 3; //Pending for Review of Upazila Technical Committee
                }
                else
                {
                    appState = 11; //Pending for Review of Union Water Resources Management Committee. Old => 2; //Pending for Review of Union Technical Committee
                }

                if (tUpazila > 1)
                {
                    appState = 31; //Pending for Review of District Water Resources Management Committee. Old => 4; //Pending for Review of District Technical Committee
                }
                else
                {
                    if (tUnion == 0 && tUpazila == 1)
                        appState = 21;// 3; //Pending for Review of Upazila Technical Committee
                    else if (tUnion == 1 && tUpazila == 1)
                        appState = 11; // 2; //Pending for Review of Union Technical Committee
                                       //else
                                       //    appState = 1; //Not Yet Submitted
                }

                if (tDistrict > 1)
                {
                    appState = 41; //Pending for Review of WARPO Water Resources Management Committee. Old => 5; //Pending for Review of WARPO Technical Committee
                }
                else
                {
                    if (tUnion == 0 && tUpazila == 0 && tDistrict == 1)
                        appState = 31; // 4; //Pending for Review of District Technical Committee
                    else if (tUnion == 1 && tUpazila == 1 && tDistrict == 1)
                        appState = 11; // 2; //Pending for Review of Union Technical Committee
                    else if (tUnion == 0 && tUpazila == 1 && tDistrict == 1)
                        appState = 21; // 3; //Pending for Review of Upazila Technical Committee
                                       //else
                                       //    appState = 1; //Not Yet Submitted
                }
            }

            return appState;
        }

        /* // this method written and blocked on 07 Jun 2020 to change logic
        private int GetProjectMultipleLocation(long projectId)
        {
            int appState = 0;
            int tUnion = 0, tUpazila = 0, tDistrict = 0;
            List<CcModPrjLocationDetail> _projLocation = _db.CcModPrjLocationDetail
                                                            .Where(w => w.ProjectId == projectId)
                                                            .ToList();

            if (_projLocation.Count > 0)
            {
                tUnion = _projLocation.Where(w => w.UnionGeoCode != string.Empty)
                                      .DistinctBy(d => d.UnionGeoCode)
                                      .Select(s => s.UnionGeoCode).Count();

                tUpazila = _projLocation.Where(w => w.UpazilaGeoCode != string.Empty)
                                        .DistinctBy(d => d.UpazilaGeoCode)
                                        .Select(s => s.UpazilaGeoCode).Count();

                tDistrict = _projLocation.Where(w => w.DistrictGeoCode != string.Empty)
                                         .DistinctBy(d => d.DistrictGeoCode)
                                         .Select(s => s.DistrictGeoCode).Count();

                if (tUnion > 1)
                {
                    appState = 23; //Pending for Final Approval of Upazila Higher Authority
                }
                else
                {
                    appState = 13; //Pending for Final Approval of Union Higher Authority
                }

                if (tUpazila > 1)
                {
                    appState = 33; //Pending for Final Approval of District Higher Authority
                }
                else
                {
                    if (tUnion == 0 && tUpazila == 1)
                        appState = 23; //Pending for Final Approval of Upazila Higher Authority
                    else if (tUnion == 1 && tUpazila == 1)
                        appState = 13; //Pending for Final Approval of Union Higher Authority
                }

                if (tDistrict > 1)
                {
                    appState = 43; //Pending for Review of WARPO DG
                }
                else
                {
                    if (tUnion == 0 && tUpazila == 0 && tDistrict == 1)
                        appState = 33; //Pending for Final Approval of District Higher Authority
                    else if (tUnion == 1 && tUpazila == 1 && tDistrict == 1)
                        appState = 13; //Pending for Final Approval of Union Higher Authority
                    else if (tUnion == 0 && tUpazila == 1 && tDistrict == 1)
                        appState = 23; //Pending for Final Approval of Upazila Higher Authority
                }
            }

            return appState;
        }
        */

        private void LoadDropdownData()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();
            //ViewBag.LookUpAdminBndUpazila = _db.LookUpAdminBndUpazila.ToList();
            //ViewBag.LookUpAdminBndUnion = _db.LookUpAdminBndUnion.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();

            ViewBag.LookUpCcModTypeOfFlood = _db.LookUpCcModTypeOfFlood.ToList();
            ViewBag.FloodFrequencyId = _db.LookUpCcModFloodFrequency.ToList();
            ViewBag.DesignSubmittedParameterId = _db.LookUpCcModDesignSubmitParam.ToList();

            ViewBag.LookUpCcModDrainageCondition = _db.LookUpCcModDrainageCondition.ToList();
            ViewBag.LookUpCcModEIAParameter = _db.LookUpCcModEIAParameter.ToList();
            ViewBag.LookUpCcModSIAParameter = _db.LookUpCcModSIAParameter.ToList();
            ViewBag.LookUpCcModEcoAndFinancial = _db.LookUpCcModEcoAndFinancial.ToList();

            ViewBag.LookUpCcModNWPArticle = _db.LookUpCcModNWPArticle.ToList();
            ViewBag.LookUpCcModNWMPProgramme = _db.LookUpCcModNWMPProgramme.ToList();
            ViewBag.LookUpCcModSDGGoal = _db.LookUpCcModSDGGoal.ToList();
            ViewBag.LookUpCcModSDGIndicator = _db.LookUpCcModSDGIndicator.ToList();
            ViewBag.LookUpCcModDeltPlan2100Goal = _db.LookUpCcModDeltPlan2100Goal.ToList();
            ViewBag.LookUpCcModGPWMGroupType = _db.LookUpCcModGPWMGroupType.ToList();
            ViewBag.RecommendationId = _db.LookUpCcModRecommendation.ToList();
            #endregion
        }

        #region Dropdown and General Info Save
        private ProjectStatusInfo GetProjectInfoByType(string projectTypeId)
        {
            var result = _db.CcModAppProjectCommonDetail
                        .Where(w => w.ProjectTypeId == projectTypeId.ToInt() && w.AppSubmissionId == 0)
                        .Select(x => new ProjectStatusInfo
                        {
                            ProjectId = x.ProjectId,
                            AppSubmissionId = x.AppSubmissionId,
                            ApplicationStateId = x.ApplicationStateId,
                            ApprovalStatusId = x.ApprovalStatusId
                        }).FirstOrDefault();

            return result;
        }

        private ProjectStatusInfo GetProjectInfoById(long projectId)
        {
            var result = (from d in _db.CcModAppProjectCommonDetail
                          join app_state in _db.LookUpCcModApplicationState on d.ApplicationStateId equals app_state.ApplicationStateId into appstate
                          from state in appstate.DefaultIfEmpty()
                          join app_approv in _db.LookUpCcModApprovalStatus on d.ApprovalStatusId equals app_approv.ApprovalStatusId into approval
                          from app in approval.DefaultIfEmpty()
                          where d.ProjectId == projectId
                          select new ProjectStatusInfo
                          {
                              ProjectId = d.ProjectId,
                              AppSubmissionId = d.AppSubmissionId,
                              ApplicationStateId = d.ApplicationStateId,
                              ApplicationState = state.ApplicationState,
                              ApprovalStatusId = d.ApprovalStatusId,
                              ApprovalStatus = app.ApprovalStatus
                          }).FirstOrDefault();

            return result;
        }

        //form/GeneralInfoSave :: gis
        [HttpPost]
        public JsonResult gis(CcModAppProjectCommonDetail _pcd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            long ProjectID = 0;
            int result = 0;

            try
            {
                if (_pcd != null && _pcd.ProjectTypeId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_pcd.ProjectId != 0)
                        {
                            CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(_pcd.ProjectId);

                            if (pcd != null)
                            {
                                pcd.ProjectName = _pcd.ProjectName;
                                pcd.BackgroundAndRationale = _pcd.BackgroundAndRationale;
                                pcd.ProjectTarget = _pcd.ProjectTarget;
                                pcd.ProjectObjective = _pcd.ProjectObjective;
                                pcd.ProjectActivity = _pcd.ProjectActivity;
                                pcd.ProjectStartDate = _pcd.ProjectStartDate;
                                pcd.ProjectCompletionDate = _pcd.ProjectCompletionDate;
                                pcd.ProjectEstimatedCost = _pcd.ProjectEstimatedCost;
                                pcd.LanguageId = sf.LanguageTypeId.ToInt();
                                pcd.IsCompletedId = 0;

                                _db.Entry(pcd).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    ProjectID = pcd.ProjectId;
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _pcd.ProjectId.ToString(),
                                        status = "success",
                                        message = "General information has been updated successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _pcd.ProjectId.ToString(),
                                        status = "error",
                                        message = "General information not updated."
                                    };
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                _pcd.UserId = ui.UserID;
                                _pcd.AppSubmissionId = 0;
                                _pcd.IsCompletedId = 0;
                                _pcd.ApplicationStateId = 1; //should come from database
                                _pcd.LanguageId = sf.LanguageTypeId.ToInt();

                                _db.CcModAppProjectCommonDetail.Add(_pcd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    ProjectID = _pcd.ProjectId;
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _pcd.ProjectId.ToString(),
                                        status = "success",
                                        message = "General information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _pcd.ProjectId.ToString(),
                                        status = "error",
                                        message = "General information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _pcd.ProjectId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }
        #endregion

        #region Project Location
        //form/ProjectLocationDataSave :: plds
        [HttpPost]
        public JsonResult plds(CcModPrjLocationDetail _plds)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_plds.ProjectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    if (_plds.LocationId != 0)
                    {
                        _db.Entry(_plds).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = _plds.LocationId.ToString(),
                                status = "success",
                                message = "Project location information has been updated successfully."
                            };
                        }
                        else
                        {
                            dbContextTransaction.Rollback();

                            noti = new Notification
                            {
                                id = _plds.LocationId.ToString(),
                                status = "error",
                                message = "Project location not updated."
                            };
                        }
                    }
                    else
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(_plds.DistrictGeoCode))
                            {
                                _db.CcModPrjLocationDetail.Add(_plds);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {

                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _plds.LocationId.ToString(),
                                        status = "success",
                                        message = "Project location information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _plds.LocationId.ToString(),
                                        status = "error",
                                        message = "Project location not saved."
                                    };
                                }
                            }
                            else
                            {
                                noti = new Notification
                                {
                                    id = _plds.LocationId.ToString(),
                                    status = "error",
                                    message = "Please select project district."
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            var message = ch.ExtractInnerException(ex);

                            noti = new Notification
                            {
                                id = _plds.LocationId.ToString(),
                                status = "error",
                                message = "Transaction has been rollbacked. " + message
                            };
                        }
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "error",
                        message = "Please save general information first."
                    };
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetHydroSystemDetail :: get_pld
        [HttpGet]
        public JsonResult get_pld(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjLocationDetail
                                join district in _db.LookUpAdminBndDistrict on d.DistrictGeoCode equals district.DistrictGeoCode into dist
                                from ds in dist.DefaultIfEmpty()
                                join upazila in _db.LookUpAdminBndUpazila on d.UpazilaGeoCode equals upazila.UpazilaGeoCode into upaz
                                from up in upaz.DefaultIfEmpty()
                                join union in _db.LookUpAdminBndUnion on d.UnionGeoCode equals union.UnionGeoCode into unio
                                from un in unio.DefaultIfEmpty()
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.LocationId,
                                    d.ProjectId,
                                    d.DistrictGeoCode,
                                    ds.DistrictName,
                                    ds.DistrictNameBn,
                                    d.UpazilaGeoCode,
                                    up.UpazilaName,
                                    up.UpazilaNameBn,
                                    d.UnionGeoCode,
                                    un.UnionName,
                                    un.UnionNameBn,
                                    Latitude = string.IsNullOrEmpty(d.Latitude) ? string.Empty : d.Latitude,
                                    Longitude = string.IsNullOrEmpty(d.Longitude) ? string.Empty : d.Longitude,
                                    ImageFileName = (!string.IsNullOrEmpty(d.ImageFileName)) ? String.Format("{0}/{1}/{2}", rootDirOfProjFile, "ProjectLocationPhotos", d.ImageFileName) : "",
                                    OnlyImageFileName = d.ImageFileName
                                }).OrderBy(o => o.LocationId).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }

        private List<ProjectLocationTemp> GetProjectLocation(long project_id)
        {
            List<ProjectLocationTemp> _details = new List<ProjectLocationTemp>();

            try
            {
                _details = (from d in _db.CcModPrjLocationDetail
                            join district in _db.LookUpAdminBndDistrict on d.DistrictGeoCode equals district.DistrictGeoCode into dist
                            from ds in dist.DefaultIfEmpty()
                            join upazila in _db.LookUpAdminBndUpazila on d.UpazilaGeoCode equals upazila.UpazilaGeoCode into upaz
                            from up in upaz.DefaultIfEmpty()
                            join union in _db.LookUpAdminBndUnion on d.UnionGeoCode equals union.UnionGeoCode into unio
                            from un in unio.DefaultIfEmpty()
                            where d.ProjectId == project_id
                            select new ProjectLocationTemp
                            {
                                LocationId = d.LocationId,
                                ProjectId = d.ProjectId,
                                DistrictGeoCode = d.DistrictGeoCode,
                                DistrictName = ds.DistrictName,
                                DistrictNameBn = ds.DistrictNameBn,
                                UpazilaGeoCode = d.UpazilaGeoCode,
                                UpazilaName = up.UpazilaName,
                                UpazilaNameBn = up.UpazilaNameBn,
                                UnionGeoCode = d.UnionGeoCode,
                                UnionName = un.UnionName,
                                UnionNameBn = un.UnionNameBn,
                                Latitude = string.IsNullOrEmpty(d.Latitude) ? string.Empty : d.Latitude,
                                LatitudeBn = string.IsNullOrEmpty(d.Latitude) ? string.Empty : d.Latitude.NumberEnglishToBengali(),
                                Longitude = string.IsNullOrEmpty(d.Longitude) ? string.Empty : d.Longitude,
                                LongitudeBn = string.IsNullOrEmpty(d.Longitude) ? string.Empty : d.Longitude.NumberEnglishToBengali(),
                                ImageFileName = (!string.IsNullOrEmpty(d.ImageFileName)) ? String.Format("{0}/{1}/{2}", rootDirOfProjFile, "ProjectLocationPhotos", d.ImageFileName) : "",
                                OnlyImageFileName = d.ImageFileName
                            }).OrderBy(o => o.LocationId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new ProjectLocationTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new ProjectLocationTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private string GetHydrologicalRegion(long project_id, int language)
        {
            int i = 1;
            string html = string.Empty, className = string.Empty, tickTag = string.Empty;
            List<ProjectHydrologicalRegionTemp> _details = new List<ProjectHydrologicalRegionTemp>();
            List<CcModPrjHydroRegionDetail> hrd = new List<CcModPrjHydroRegionDetail>();

            try
            {
                _details = (from d in _db.LookUpCcModHydroRegion
                            select new ProjectHydrologicalRegionTemp
                            {
                                HydroRegionId = d.HydroRegionId,
                                ProjectId = 0,
                                HydroRegionShortName = d.HydroRegionShortName,
                                HydroRegionShortNameBn = d.HydroRegionShortNameBn,
                                HydroRegionFullName = d.HydroRegionFullName,
                                HydroRegionFullNameBn = d.HydroRegionFullNameBn,
                                IsSelected = false
                            }).OrderBy(o => o.HydroRegionId).ToList();

                if (_details.Count > 0)
                {
                    hrd = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == project_id).ToList();
                    foreach (var item in _details.Where(m => hrd.Any(a => a.HydroRegionId == m.HydroRegionId)))
                    {
                        item.IsSelected = true;
                        item.ProjectId = project_id;
                    }

                    _details.OrderBy(o => o.HydroRegionId).ToList();

                    bool isNewLine = false;
                    foreach (ProjectHydrologicalRegionTemp hrt in _details)
                    {
                        isNewLine = false;
                        className = hrt.IsSelected ? "bg-dark text-white" : "";
                        tickTag = hrt.IsSelected ? "<span class='fe-check'></span>" : "";

                        if (i == 1)
                        {
                            html += "<tr>";
                        }

                        if (language == 1)
                        {
                            html += "<td title='" + hrt.HydroRegionShortNameBn + "' class='" + className + "'>" + tickTag + " " + hrt.HydroRegionFullNameBn + "</td>";
                        }
                        else
                        {
                            html += "<td title='" + hrt.HydroRegionShortName + "' class='" + className + "'>" + tickTag + " " + hrt.HydroRegionFullName + "</td>";
                        }

                        if (i == 4)
                        {
                            html += "</tr>";
                            i = 1;
                            isNewLine = true;
                        }

                        if (!isNewLine)
                            i++;
                    }
                }
                else
                {
                    html = "<tr><td colspan='4'>no data found!</td></tr>";
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                html = "<tr><td colspan='4'>" + message + "</td></tr>";
            }

            return html;
        }

        private List<HydroSystemDetailTemp> GetHydroSystemDetail(long project_id)
        {
            List<HydroSystemDetailTemp> _details = new List<HydroSystemDetailTemp>();

            try
            {
                _details = (from d in _db.CcModHydroSystemDetail
                            join l in _db.LookUpCcModHydroSystem on d.HydroSystemCategoryId equals l.HydroSystemCategoryId
                            where d.ProjectId == project_id
                            select new HydroSystemDetailTemp
                            {
                                HydroSysDetailId = d.HydroSysDetailId,
                                ProjectId = d.ProjectId,
                                HydroSystemCategoryId = d.HydroSystemCategoryId,
                                HydroSystemCategory = l.HydroSystemCategory,
                                HydroSystemCategoryBn = l.HydroSystemCategoryBn,
                                NameOfHydroSystem = d.NameOfHydroSystem,
                                HydroSystemLengthArea = d.HydroSystemLengthArea.ToString(),
                                HydroSystemUnit = l.HydroSysCategoryUnit,
                                HydroSystemUnitBn = l.HydroSysCategoryUnitBn
                            }).OrderBy(o => o.HydroSysDetailId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new HydroSystemDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new HydroSystemDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private List<FloodFrequencyDetailTemp> GetFloodFrequencyDetail(long project_id)
        {
            List<FloodFrequencyDetailTemp> _details = new List<FloodFrequencyDetailTemp>();

            try
            {
                _details = (from d in _db.CcModFloodFrequencyDetail
                            join l in _db.LookUpCcModFloodFrequency on d.FloodFrequencyId equals l.FloodFrequencyId
                            where d.ProjectId == project_id
                            select new FloodFrequencyDetailTemp
                            {
                                FloodFrequencyDetailId = d.FloodFrequencyDetailId,
                                ProjectId = d.ProjectId,
                                FloodFrequencyId = d.FloodFrequencyId,
                                FloodFrequency = l.FloodFrequency,
                                FloodFrequencyBn = l.FloodFrequencyBn,
                                FloodFrequencyLevel = d.FloodFrequencyLevel.ToString()
                            }).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new FloodFrequencyDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new FloodFrequencyDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private List<IrrigCropAreaDetailTemp> GetIrrigCropAreaDetail(long project_id)
        {
            List<IrrigCropAreaDetailTemp> _details = new List<IrrigCropAreaDetailTemp>();

            try
            {
                _details = (from d in _db.CcModPrjIrrigCropAreaDetail
                            where d.ProjectId == project_id
                            select new IrrigCropAreaDetailTemp
                            {
                                IrrigatedCropId = d.IrrigatedCropId,
                                ProjectId = d.ProjectId,
                                CropName = d.CropName,
                                Area = d.Area.ToString()
                            }).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new IrrigCropAreaDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new IrrigCropAreaDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private List<AnalyzeOptionsDetailTemp> GetAnalyzeOptionsDetail(long project_id)
        {
            List<AnalyzeOptionsDetailTemp> _details = new List<AnalyzeOptionsDetailTemp>();

            try
            {
                _details = (from d in _db.CcModAnalyzeOptionsDetail
                            where d.ProjectId == project_id
                            select new AnalyzeOptionsDetailTemp
                            {
                                AnalyzeOptionsId = d.AnalyzeOptionsId,
                                ProjectId = d.ProjectId,
                                OptionNumber = d.OptionNumber,
                                AnalyzeDescription = d.AnalyzeDescription,
                                AnalyzeRemarks = d.AnalyzeRemarks
                            }).OrderBy(o => o.AnalyzeOptionsId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new AnalyzeOptionsDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new AnalyzeOptionsDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private List<DesignSubmitDetailTemp> GetDesignSubmitDetail(long project_id)
        {
            List<DesignSubmitDetailTemp> _details = new List<DesignSubmitDetailTemp>();

            try
            {
                _details = (from d in _db.CcModDesignSubmitDetail
                            join l in _db.LookUpCcModDesignSubmitParam on d.DesignSubmittedParameterId equals l.DesignSubmittedParameterId
                            where d.ProjectId == project_id
                            select new DesignSubmitDetailTemp
                            {
                                DesignSubmittedId = d.DesignSubmittedId,
                                ProjectId = d.ProjectId,
                                DesignSubmittedParameterId = d.DesignSubmittedParameterId,
                                ParameterName = l.ParameterName,
                                ParameterNameBn = l.ParameterNameBn,
                                dswpdYN = d.YesNoId,
                                DesignSubmitApplicantCmt = d.DesignSubmitApplicantCmt,
                                DesignSubmitAuthorityCmt = d.DesignSubmitAuthorityCmt
                            }).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new DesignSubmitDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new DesignSubmitDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private List<EcoFinAnalysisDetailTemp> GetEcoFinAnalysisDetail(long project_id)
        {
            List<EcoFinAnalysisDetailTemp> _details = new List<EcoFinAnalysisDetailTemp>();

            try
            {
                _details = (from d in _db.CcModPrjEcoFinAnalysisDetail
                            join l in _db.LookUpCcModEcoAndFinancial on d.EcoAndFinancialParamId equals l.EcoAndFinancialParamId
                            where d.ProjectId == project_id
                            select new EcoFinAnalysisDetailTemp
                            {
                                EconomicalAndFinancialId = d.EconomicalAndFinancialId,
                                ProjectId = d.ProjectId,
                                EcoAndFinancialParamId = d.EcoAndFinancialParamId,
                                EcoAndFinancialParamName = l.EcoAndFinancialParamName,
                                EcoAndFinancialParamNameBn = l.EcoAndFinancialParamNameBn,
                                EcoAndFinancialParamUnit = l.EcoAndFinancialUnit,
                                EcoAndFinancialApplicantCmt = d.EcoAndFinancialApplicantCmt,
                                EcoAndFinancialAuthorityCmt = d.EcoAndFinancialAuthorityCmt
                            }).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new EcoFinAnalysisDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new EcoFinAnalysisDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private List<EiaDetailTemp> GetEiaDetailTemp(long project_id)
        {
            List<EiaDetailTemp> _details = new List<EiaDetailTemp>();

            try
            {
                _details = (from d in _db.CcModPrjEIADetail
                            join l in _db.LookUpCcModEIAParameter on d.EIAParameterId equals l.EIAParameterId
                            where d.ProjectId == project_id
                            select new EiaDetailTemp
                            {
                                EIAId = d.EIAId,
                                ProjectId = d.ProjectId,
                                EIAParameterId = d.EIAParameterId,
                                ParameterName = l.ParameterName,
                                ParameterNameBn = l.ParameterNameBn,
                                PreProjectSituation = d.PreProjectSituation,
                                PostProjectSituation = d.PostProjectSituation,
                                PositiveNegativeImpact = d.PositiveNegativeImpact,
                                MitigationPlan = d.MitigationPlan
                            }).OrderBy(o => o.EIAId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new EiaDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new EiaDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private List<SiaDetailTemp> GetSiaDetailTemp(long project_id)
        {
            List<SiaDetailTemp> _details = new List<SiaDetailTemp>();

            try
            {
                _details = (from d in _db.CcModPrjSIADetail
                            join l in _db.LookUpCcModSIAParameter on d.SIAParameterId equals l.SIAParameterId
                            where d.ProjectId == project_id
                            select new SiaDetailTemp
                            {
                                SIAId = d.SIAId,
                                ProjectId = d.ProjectId,
                                SIAParameterId = d.SIAParameterId,
                                SIAParameterName = l.SIAParameterName,
                                SIAParameterNameBn = l.SIAParameterNameBn,
                                PreProjectSituation = d.PreProjectSituation,
                                PostProjectSituation = d.PostProjectSituation,
                                PositiveNegativeImpact = d.PositiveNegativeImpact,
                                MitigationPlan = d.MitigationPlan
                            }).OrderBy(o => o.SIAId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new SiaDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new SiaDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        private string GetCompatNWPDetail(long project_id, int language)
        {
            int i = 1;
            string html = string.Empty, className = string.Empty, tickTag = string.Empty;
            List<ProjectNWPArticleTemp> _details = new List<ProjectNWPArticleTemp>();
            List<CcModPrjCompatNWPDetail> cnwpd = new List<CcModPrjCompatNWPDetail>();

            try
            {
                _details = (from d in _db.LookUpCcModNWPArticle
                            select new ProjectNWPArticleTemp
                            {
                                NWPArticleId = d.NationalWaterPolicyArticleId,
                                ProjectId = 0,
                                NWPShortTitle = d.NationalWtrPolicyShortTitle,
                                NWPShortTitleBn = d.NWPArticleShortTitleBn,
                                NWPArticleTitle = d.NationalWtrPolicyArticleTitle,
                                NWPArticleTitleBn = d.NWPArticleTitleBn,
                                IsSelected = false
                            }).OrderBy(o => o.NWPArticleId).ToList();

                if (_details.Count > 0)
                {
                    cnwpd = _db.CcModPrjCompatNWPDetail.Where(w => w.ProjectId == project_id).ToList();
                    foreach (var item in _details.Where(m => cnwpd.Any(a => a.NationalWaterPolicyArticleId == m.NWPArticleId)))
                    {
                        item.IsSelected = true;
                        item.ProjectId = project_id;
                    }

                    _details.OrderBy(o => o.NWPArticleId).ToList();

                    html = "<tr style='background: #9d9b9b; font-weight: bold;'><td colspan='4'>Articles</td></tr>";

                    bool isNewLine = false;
                    foreach (ProjectNWPArticleTemp nwpat in _details)
                    {
                        isNewLine = false;
                        className = nwpat.IsSelected ? "bg-dark text-white" : "";
                        tickTag = nwpat.IsSelected ? "<span class='fe-check'></span>" : "";

                        if (i == 1)
                        {
                            html += "<tr>";
                        }

                        if (language == 1)
                        {
                            html += "<td title='" + nwpat.NWPArticleTitleBn + "' class='" + className + "'>" + tickTag + " " + nwpat.NWPShortTitleBn + "</td>";
                        }
                        else
                        {
                            html += "<td title='" + nwpat.NWPArticleTitle + "' class='" + className + "'>" + tickTag + " " + nwpat.NWPShortTitle + "</td>";
                        }

                        if (i == 4)
                        {
                            html += "</tr>";
                            i = 1;
                            isNewLine = true;
                        }

                        if (!isNewLine)
                            i++;
                    }
                }
                else
                {
                    html = "<tr><td colspan='4'>no data found!</td></tr>";
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                html = "<tr><td colspan='4'>" + message + "</td></tr>";
            }

            return html;
        }

        private string GetCompatNWMPDetail(long project_id, int language)
        {
            int i = 1;
            string html = string.Empty, className = string.Empty, tickTag = string.Empty;
            List<ProjectNWMPProgramTemp> _details = new List<ProjectNWMPProgramTemp>();
            List<CcModPrjCompatNWMPDetail> cnwmpd = new List<CcModPrjCompatNWMPDetail>();

            try
            {
                _details = (from d in _db.LookUpCcModNWMPProgramme
                            select new ProjectNWMPProgramTemp
                            {
                                NWMPProgrammeId = d.NWMPProgrammeId,
                                ProjectId = 0,
                                NWMPProgShortName = d.NWMPProgShortName,
                                NWMPProgrammeTitle = d.NWMPProgrammeTitle,
                                NWMPLink = d.NWMPLink,
                                IsSelected = false
                            }).OrderBy(o => o.NWMPProgrammeId).ToList();

                if (_details.Count > 0)
                {
                    cnwmpd = _db.CcModPrjCompatNWMPDetail.Where(w => w.ProjectId == project_id).ToList();
                    foreach (var item in _details.Where(m => cnwmpd.Any(a => a.NWMPProgrammeId == m.NWMPProgrammeId)))
                    {
                        item.IsSelected = true;
                        item.ProjectId = project_id;
                    }

                    _details.OrderBy(o => o.NWMPProgrammeId).ToList();

                    html = "<tr style='background: #9d9b9b; font-weight: bold;'><td colspan='5'>Involved Programme</td></tr>";

                    bool isNewLine = false;
                    foreach (ProjectNWMPProgramTemp nwmp in _details)
                    {
                        isNewLine = false;
                        className = nwmp.IsSelected ? "bg-dark text-white" : "";
                        tickTag = nwmp.IsSelected ? "<span class='fe-check'></span>" : "";

                        if (i == 1)
                        {
                            html += "<tr>";
                        }

                        html += "<td title='" + nwmp.NWMPProgrammeTitle + "' class='" + className + "'>" + tickTag + " " + nwmp.NWMPProgrammeTitle + "</td>";

                        if (i == 5)
                        {
                            html += "</tr>";
                            i = 1;
                            isNewLine = true;
                        }

                        if (!isNewLine)
                            i++;
                    }
                }
                else
                {
                    html = "<tr><td colspan='5'>no data found!</td></tr>";
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                html = "<tr><td colspan='5'>" + message + "</td></tr>";
            }

            return html;
        }

        private string GetCompatSDGDetail(long project_id, int language)
        {
            int i = 1;
            string html = string.Empty, className = string.Empty, tickTag = string.Empty;
            List<ProjectSDGTemp> _details = new List<ProjectSDGTemp>();
            List<CcModPrjCompatSDGDetail> sdgd = new List<CcModPrjCompatSDGDetail>();

            try
            {
                _details = (from d in _db.LookUpCcModSDGGoal
                            select new ProjectSDGTemp
                            {
                                SDGGoalId = d.SDGGoalId,
                                ProjectId = 0,
                                SDGGoalNumber = d.SDGGoalNumber,
                                SDGGoalNumberBn = d.SDGGoalNumberBn,
                                SDGDocLink = d.SDGDocLink,
                                IsSelected = false
                            }).OrderBy(o => o.SDGGoalId).ToList();

                if (_details.Count > 0)
                {
                    sdgd = _db.CcModPrjCompatSDGDetail.Where(w => w.ProjectId == project_id).ToList();
                    foreach (var item in _details.Where(m => sdgd.Any(a => a.SDGGoalId == m.SDGGoalId)))
                    {
                        item.IsSelected = true;
                        item.ProjectId = project_id;
                    }

                    _details.OrderBy(o => o.SDGGoalId).ToList();

                    string title = language == 1 ? "টেকসই উন্নয়নের লক্ষ্য" : "Sustainable Development Goal";
                    html = "<tr style='background: #9d9b9b; font-weight: bold;'><td colspan='5'>" + title + "</td></tr>";

                    bool isNewLine = false;
                    foreach (ProjectSDGTemp sdg in _details)
                    {
                        isNewLine = false;
                        className = sdg.IsSelected ? "bg-dark text-white" : "";
                        tickTag = sdg.IsSelected ? "<span class='fe-check'></span>" : "";

                        if (i == 1)
                        {
                            html += "<tr>";
                        }

                        if (language == 1)
                        {
                            html += "<td title='" + sdg.SDGGoalNumberBn + "' class='" + className + "'>" + tickTag + " " + sdg.SDGGoalNumberBn + "</td>";
                        }
                        else
                        {
                            html += "<td title='" + sdg.SDGGoalNumber + "' class='" + className + "'>" + tickTag + " " + sdg.SDGGoalNumber + "</td>";
                        }

                        if (i == 5)
                        {
                            html += "</tr>";
                            i = 1;
                            isNewLine = true;
                        }

                        if (!isNewLine)
                            i++;
                    }
                }
                else
                {
                    html = "<tr><td colspan='5'>no data found!</td></tr>";
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                html = "<tr><td colspan='5'>" + message + "</td></tr>";
            }

            return html;
        }

        private string GetCompatSDGIndicatorDetail(long project_id, int language)
        {
            int i = 1;
            string html = string.Empty, className = string.Empty, tickTag = string.Empty;
            List<ProjectSDGITemp> _details = new List<ProjectSDGITemp>();
            List<CcModPrjCompatSDGIndiDetail> sdgd = new List<CcModPrjCompatSDGIndiDetail>();

            try
            {
                _details = (from d in _db.LookUpCcModSDGIndicator
                            select new ProjectSDGITemp
                            {
                                SDGIndicatorId = d.SDGIndicatorId,
                                ProjectId = 0,
                                SDGIndicatorName = d.SDGIndicatorName,
                                SDGIndicatorNameBn = d.SDGIndicatorNameBn,
                                SDGIndicatorDocLink = d.SDGIndicatorDocLink,
                                IsSelected = false
                            }).OrderBy(o => o.SDGIndicatorId).ToList();

                if (_details.Count > 0)
                {
                    sdgd = _db.CcModPrjCompatSDGIndiDetail.Where(w => w.ProjectId == project_id).ToList();
                    foreach (var item in _details.Where(m => sdgd.Any(a => a.SDGIndicatorId == m.SDGIndicatorId)))
                    {
                        item.IsSelected = true;
                        item.ProjectId = project_id;
                    }

                    _details.OrderBy(o => o.SDGIndicatorId).ToList();

                    string title = language == 1 ? "টেকসই উন্নয়নের লক্ষ্য ৬.০ সূচক" : "Sustainable Development Goal 6.0 Indicators";
                    html = "<tr style='background: #9d9b9b; font-weight: bold;'><td colspan='5'>" + title + "</td></tr>";

                    bool isNewLine = false;
                    foreach (ProjectSDGITemp sdg in _details)
                    {
                        isNewLine = false;
                        className = sdg.IsSelected ? "bg-dark text-white" : "";
                        tickTag = sdg.IsSelected ? "<span class='fe-check'></span>" : "";

                        if (i == 1)
                        {
                            html += "<tr>";
                        }

                        if (language == 1)
                        {
                            html += "<td title='" + sdg.SDGIndicatorNameBn + "' class='" + className + "'>" + tickTag + " " + sdg.SDGIndicatorNameBn + "</td>";
                        }
                        else
                        {
                            html += "<td title='" + sdg.SDGIndicatorName + "' class='" + className + "'>" + tickTag + " " + sdg.SDGIndicatorName + "</td>";
                        }

                        if (i == 5)
                        {
                            html += "</tr>";
                            i = 1;
                            isNewLine = true;
                        }

                        if (!isNewLine)
                            i++;
                    }
                }
                else
                {
                    html = "<tr><td colspan='5'>no data found!</td></tr>";
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                html = "<tr><td colspan='5'>" + message + "</td></tr>";
            }

            return html;
        }

        private string GetBDP2100GoalDetail(long project_id, int language)
        {
            int i = 1;
            string html = string.Empty, className = string.Empty, tickTag = string.Empty;
            List<BDP2100GoalDetailTemp> _details = new List<BDP2100GoalDetailTemp>();
            List<CcModBDP2100GoalDetail> sdgd = new List<CcModBDP2100GoalDetail>();

            try
            {
                _details = (from d in _db.LookUpCcModDeltPlan2100Goal
                            select new BDP2100GoalDetailTemp
                            {
                                DeltPlan2100GoalId = d.DeltPlan2100GoalId,
                                ProjectId = 0,
                                DeltPlan2100Goal = d.DeltPlan2100Goal,
                                DeltPlan2100GoalBn = d.DeltPlan2100GoalBn,
                                DeltPlan2100GoaDocLink = d.DeltPlan2100GoaDocLink,
                                IsSelected = false
                            }).OrderBy(o => o.DeltPlan2100GoalId).ToList();

                if (_details.Count > 0)
                {
                    sdgd = _db.CcModBDP2100GoalDetail.Where(w => w.ProjectId == project_id).ToList();
                    foreach (var item in _details.Where(m => sdgd.Any(a => a.DeltPlan2100GoalId == m.DeltPlan2100GoalId)))
                    {
                        item.IsSelected = true;
                        item.ProjectId = project_id;
                    }

                    _details.OrderBy(o => o.DeltPlan2100GoalId).ToList();

                    string title = language == 1 ? "বাংলাদেশ ডেল্টা পরিকল্পনা 2100 লক্ষ্য" : "Bangladesh Delta Plan 2100 Goal";
                    html = "<tr style='background: #9d9b9b; font-weight: bold;'><td colspan='3'>" + title + "</td></tr>";

                    bool isNewLine = false;
                    foreach (BDP2100GoalDetailTemp bdpg in _details)
                    {
                        isNewLine = false;
                        className = bdpg.IsSelected ? "bg-dark text-white" : "";
                        tickTag = bdpg.IsSelected ? "<span class='fe-check'></span>" : "";

                        if (i == 1)
                        {
                            html += "<tr>";
                        }

                        if (language == 1)
                        {
                            html += "<td title='" + bdpg.DeltPlan2100GoalBn + "' class='" + className + "'>" + tickTag + " " + bdpg.DeltPlan2100GoalBn + "</td>";
                        }
                        else
                        {
                            html += "<td title='" + bdpg.DeltPlan2100Goal + "' class='" + className + "'>" + tickTag + " " + bdpg.DeltPlan2100Goal + "</td>";
                        }

                        if (i == 3)
                        {
                            html += "</tr>";
                            i = 1;
                            isNewLine = true;
                        }

                        if (!isNewLine)
                            i++;
                    }
                }
                else
                {
                    html = "<tr><td colspan='3'>no data found!</td></tr>";
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                html = "<tr><td colspan='3'>" + message + "</td></tr>";
            }

            return html;
        }

        private string GetGPWMGroupTypeDetail(long project_id, int language)
        {
            int i = 1;
            string html = string.Empty, className = string.Empty, tickTag = string.Empty;
            List<GPWMGroupTypeTemp> _details = new List<GPWMGroupTypeTemp>();
            List<CcModGPWMGroupTypeDetail> sdgd = new List<CcModGPWMGroupTypeDetail>();

            try
            {
                _details = (from d in _db.LookUpCcModGPWMGroupType
                            select new GPWMGroupTypeTemp
                            {
                                GPWMGroupTypeId = d.GPWMGroupTypeId,
                                ProjectId = 0,
                                GPWMGroupTypeName = d.GPWMGroupTypeName,
                                GPWMGroupTypeNameBn = d.GPWMGroupTypeNameBn,
                                IsSelected = false
                            }).OrderBy(o => o.GPWMGroupTypeId).ToList();

                if (_details.Count > 0)
                {
                    sdgd = _db.CcModGPWMGroupTypeDetail.Where(w => w.ProjectId == project_id).ToList();
                    foreach (var item in _details.Where(m => sdgd.Any(a => a.GPWMGroupTypeId == m.GPWMGroupTypeId)))
                    {
                        item.IsSelected = true;
                        item.ProjectId = project_id;
                    }

                    _details.OrderBy(o => o.GPWMGroupTypeId).ToList();

                    string title = language == 1 ? "জিপিডব্লিউএম গ্রুপ" : "GPWM Group";
                    html = "<tr style='background: #9d9b9b; font-weight: bold;'><td colspan='5'>" + title + "</td></tr>";

                    bool isNewLine = false;
                    foreach (GPWMGroupTypeTemp bdpg in _details)
                    {
                        isNewLine = false;
                        className = bdpg.IsSelected ? "bg-dark text-white" : "";
                        tickTag = bdpg.IsSelected ? "<span class='fe-check'></span>" : "";

                        if (i == 1)
                        {
                            html += "<tr>";
                        }

                        if (language == 1)
                        {
                            html += "<td title='" + bdpg.GPWMGroupTypeNameBn + "' class='" + className + "'>" + tickTag + " " + bdpg.GPWMGroupTypeNameBn + "</td>";
                        }
                        else
                        {
                            html += "<td title='" + bdpg.GPWMGroupTypeName + "' class='" + className + "'>" + tickTag + " " + bdpg.GPWMGroupTypeName + "</td>";
                        }

                        if (i == 5)
                        {
                            html += "</tr>";
                            i = 1;
                            isNewLine = true;
                        }

                        if (!isNewLine)
                            i++;
                    }
                }
                else
                {
                    html = "<tr><td colspan='5'>no data found!</td></tr>";
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);
                html = "<tr><td colspan='5'>" + message + "</td></tr>";
            }

            return html;
        }
        #endregion

        #region Hydrological System
        //form/HydroSystemDetailSave :: hsds
        [HttpPost]
        public JsonResult hsds(CcModHydroSystemDetail _hsd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_hsd != null && _hsd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_hsd.HydroSysDetailId != 0)
                        {
                            _db.Entry(_hsd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _hsd.HydroSysDetailId.ToString(),
                                    status = "success",
                                    message = "Hydrological system information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _hsd.HydroSysDetailId.ToString(),
                                    status = "error",
                                    message = "Hydrological system information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModHydroSystemDetail.Add(_hsd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _hsd.HydroSysDetailId.ToString(),
                                        status = "success",
                                        message = "Hydrological system information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _hsd.HydroSysDetailId.ToString(),
                                        status = "error",
                                        message = "Hydrological system information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _hsd.HydroSysDetailId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetHydroSystemDetail :: get_hsd
        [HttpGet]
        public JsonResult get_hsd(long project_id)
        {
            //List<CcModHydroSystemDetail> _details = new List<CcModHydroSystemDetail>();
            //_details = _db.CcModHydroSystemDetail.Where(w => w.ProjectId != null && w.ProjectId == project_id).ToList();

            try
            {
                var _details = (from d in _db.CcModHydroSystemDetail
                                join l in _db.LookUpCcModHydroSystem on d.HydroSystemCategoryId equals l.HydroSystemCategoryId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.HydroSysDetailId,
                                    d.ProjectId,
                                    d.HydroSystemCategoryId,
                                    l.HydroSystemCategory,
                                    d.NameOfHydroSystem,
                                    d.HydroSystemLengthArea,
                                    d.HydroSystemUnit
                                }).OrderBy(o => o.HydroSysDetailId).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }
        #endregion

        #region Flood Frequency
        //form/FloodFrequencyDetailSave :: ffds
        [HttpPost]
        public JsonResult ffds(CcModFloodFrequencyDetail _ffd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_ffd != null && _ffd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_ffd.FloodFrequencyDetailId != 0)
                        {
                            _db.Entry(_ffd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _ffd.FloodFrequencyId.ToString(),
                                    status = "success",
                                    message = "Flood frequency information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _ffd.FloodFrequencyId.ToString(),
                                    status = "error",
                                    message = "Flood frequency information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModFloodFrequencyDetail.Add(_ffd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _ffd.FloodFrequencyId.ToString(),
                                        status = "success",
                                        message = "Flood frequency information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _ffd.FloodFrequencyId.ToString(),
                                        status = "error",
                                        message = "Flood frequency information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _ffd.FloodFrequencyId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetFloodFrequencyDetail :: get_ffd
        [HttpGet]
        public JsonResult get_ffd(long project_id)
        {
            //List<CcModHydroSystemDetail> _details = new List<CcModHydroSystemDetail>();
            //_details = _db.CcModHydroSystemDetail.Where(w => w.ProjectId != null && w.ProjectId == project_id).ToList();

            try
            {
                var _details = (from d in _db.CcModFloodFrequencyDetail
                                join l in _db.LookUpCcModFloodFrequency on d.FloodFrequencyId equals l.FloodFrequencyId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.FloodFrequencyDetailId,
                                    d.ProjectId,
                                    d.FloodFrequencyId,
                                    l.FloodFrequency,
                                    d.FloodFrequencyLevel
                                }).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }
        #endregion

        #region Irrigated Crop
        //form/IrrigatedCropAreaSave :: icas
        [HttpPost]
        public JsonResult icas(CcModPrjIrrigCropAreaDetail _ica)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_ica != null && _ica.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_ica.IrrigatedCropId != 0)
                        {
                            _db.Entry(_ica).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _ica.IrrigatedCropId.ToString(),
                                    status = "success",
                                    message = "Irrigated crop area information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _ica.IrrigatedCropId.ToString(),
                                    status = "error",
                                    message = "Irrigated crop area information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjIrrigCropAreaDetail.Add(_ica);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _ica.IrrigatedCropId.ToString(),
                                        status = "success",
                                        message = "Irrigated crop area information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _ica.IrrigatedCropId.ToString(),
                                        status = "error",
                                        message = "Irrigated crop area information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _ica.IrrigatedCropId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetIrrigatedCropArea :: get_ica
        [HttpGet]
        public JsonResult get_ica(long project_id)
        {
            //List<CcModHydroSystemDetail> _details = new List<CcModHydroSystemDetail>();
            //_details = _db.CcModHydroSystemDetail.Where(w => w.ProjectId != null && w.ProjectId == project_id).ToList();

            try
            {
                var _details = (from d in _db.CcModPrjIrrigCropAreaDetail
                                    //join l in _db.Irri on d.FloodFrequencyId equals l.FloodFrequencyId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.IrrigatedCropId,
                                    d.ProjectId,
                                    d.CropName,
                                    d.Area
                                }).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }
        #endregion

        #region Analyze Options to fulfill objective
        //form/AnalyzeOptionsFulfillObjectiveSave :: aofos
        [HttpPost]
        public JsonResult aofos(CcModAnalyzeOptionsDetail _aofd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_aofd != null && _aofd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_aofd.AnalyzeOptionsId != 0)
                        {
                            _db.Entry(_aofd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _aofd.AnalyzeOptionsId.ToString(),
                                    status = "success",
                                    message = "Information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _aofd.AnalyzeOptionsId.ToString(),
                                    status = "error",
                                    message = "Information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModAnalyzeOptionsDetail.Add(_aofd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _aofd.AnalyzeOptionsId.ToString(),
                                        status = "success",
                                        message = "Information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _aofd.AnalyzeOptionsId.ToString(),
                                        status = "error",
                                        message = "Information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _aofd.AnalyzeOptionsId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetAnalyzeOptionsFulfillObjective :: get_aofo
        [HttpGet]
        public JsonResult get_aofo(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModAnalyzeOptionsDetail
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.AnalyzeOptionsId,
                                    d.ProjectId,
                                    d.OptionNumber,
                                    d.AnalyzeDescription,
                                    d.AnalyzeRemarks
                                }).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }
        #endregion

        #region Design Submitted with Project Document
        //form/DesignSubmittedWithProjectDocumentSave :: dswpds
        [HttpPost]
        public JsonResult dswpds(CcModDesignSubmitDetail _dspd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_dspd != null && _dspd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_dspd.DesignSubmittedId != 0)
                        {
                            _db.Entry(_dspd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _dspd.DesignSubmittedId.ToString(),
                                    status = "success",
                                    message = "Design submitted with project document information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _dspd.DesignSubmittedId.ToString(),
                                    status = "error",
                                    message = "Design submitted with project document information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModDesignSubmitDetail.Add(_dspd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _dspd.DesignSubmittedId.ToString(),
                                        status = "success",
                                        message = "Design submitted with project document information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _dspd.DesignSubmittedId.ToString(),
                                        status = "error",
                                        message = "Design submitted with project document information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _dspd.DesignSubmittedId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetAnalyzeOptionsFulfillObjective :: get_aofo
        [HttpGet]
        public JsonResult get_dswpd(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModDesignSubmitDetail
                                join l in _db.LookUpCcModDesignSubmitParam on d.DesignSubmittedParameterId equals l.DesignSubmittedParameterId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.DesignSubmittedId,
                                    d.ProjectId,
                                    d.DesignSubmittedParameterId,
                                    l.ParameterName,
                                    dswpdYN = d.YesNoId == 0 ? "No" : "Yes",
                                    d.DesignSubmitApplicantCmt,
                                    d.DesignSubmitAuthorityCmt
                                }).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }
        #endregion

        #region EIA
        //form/EIADetailSave :: eiads
        [HttpPost]
        public JsonResult eiads(CcModPrjEIADetail _eia)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_eia != null && _eia.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_eia.EIAId != 0)
                        {
                            _db.Entry(_eia).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _eia.EIAId.ToString(),
                                    status = "success",
                                    message = "Environmental Impact Assessment (EIA) information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _eia.EIAId.ToString(),
                                    status = "error",
                                    message = "Environmental Impact Assessment (EIA) information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjEIADetail.Add(_eia);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _eia.EIAId.ToString(),
                                        status = "success",
                                        message = "Environmental Impact Assessment (EIA) information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _eia.EIAId.ToString(),
                                        status = "error",
                                        message = "Environmental Impact Assessment (EIA) information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _eia.EIAId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetAnalyzeOptionsFulfillObjective :: get_eiad
        [HttpGet]
        public JsonResult get_eiad(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjEIADetail
                                join l in _db.LookUpCcModEIAParameter on d.EIAParameterId equals l.EIAParameterId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.EIAId,
                                    d.ProjectId,
                                    d.EIAParameterId,
                                    l.ParameterName,
                                    d.PreProjectSituation,
                                    d.PostProjectSituation,
                                    d.PositiveNegativeImpact,
                                    d.MitigationPlan
                                }).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }
        #endregion

        #region SIA
        //form/SIADetailSave :: eiads
        [HttpPost]
        public JsonResult siads(CcModPrjSIADetail _sia)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_sia != null && _sia.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_sia.SIAId != 0)
                        {
                            _db.Entry(_sia).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _sia.SIAId.ToString(),
                                    status = "success",
                                    message = "Social Impact Assessment (EIA) information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _sia.SIAId.ToString(),
                                    status = "error",
                                    message = "Social Impact Assessment (EIA) information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjSIADetail.Add(_sia);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _sia.SIAId.ToString(),
                                        status = "success",
                                        message = "Social Impact Assessment (EIA) information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _sia.SIAId.ToString(),
                                        status = "error",
                                        message = "Social Impact Assessment (EIA) information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _sia.SIAId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GetAnalyzeOptionsFulfillObjective :: get_eiad
        [HttpGet]
        public JsonResult get_siad(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjSIADetail
                                join l in _db.LookUpCcModSIAParameter on d.SIAParameterId equals l.SIAParameterId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.SIAId,
                                    d.ProjectId,
                                    d.SIAParameterId,
                                    l.SIAParameterName,
                                    d.PreProjectSituation,
                                    d.PostProjectSituation,
                                    d.PositiveNegativeImpact,
                                    d.MitigationPlan
                                }).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }
        #endregion

        #region Economical & Financial Analysis
        //form/EconomicalFinancialAnalysisSave :: efas
        [HttpPost]
        public JsonResult efas(CcModPrjEcoFinAnalysisDetail _efas)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_efas != null && _efas.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_efas.EconomicalAndFinancialId != 0)
                        {
                            _db.Entry(_efas).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _efas.EconomicalAndFinancialId.ToString(),
                                    status = "success",
                                    message = "Economical & financial analysis information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _efas.EconomicalAndFinancialId.ToString(),
                                    status = "error",
                                    message = "Economical & financial analysis information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjEcoFinAnalysisDetail.Add(_efas);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _efas.EconomicalAndFinancialId.ToString(),
                                        status = "success",
                                        message = "Economical & financial analysis information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _efas.EconomicalAndFinancialId.ToString(),
                                        status = "error",
                                        message = "Economical & financial analysis information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _efas.EconomicalAndFinancialId.ToString(),
                                    status = "error",
                                    message = "Transaction has been rollbacked. " + message
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/GerEconomicalFinancialAnalysis :: get_efad
        [HttpGet]
        public JsonResult get_efad(long project_id)
        {
            try
            {
                var _details = (from d in _db.CcModPrjEcoFinAnalysisDetail
                                join l in _db.LookUpCcModEcoAndFinancial on d.EcoAndFinancialParamId equals l.EcoAndFinancialParamId
                                where d.ProjectId != null && d.ProjectId == project_id
                                select new
                                {
                                    d.EconomicalAndFinancialId,
                                    d.ProjectId,
                                    d.EcoAndFinancialParamId,
                                    l.EcoAndFinancialParamName,
                                    EcoAndFinancialParamUnit = l.EcoAndFinancialUnit,
                                    d.EcoAndFinancialApplicantCmt,
                                    d.EcoAndFinancialAuthorityCmt
                                }).ToList();

                if (_details.Count > 0)
                {
                    return Json(_details);
                }
                else
                {
                    _details = null;

                    noti = new Notification
                    {
                        id = string.Empty,
                        status = "error",
                        message = "Sorry, no data found."
                    };

                    return Json(noti);
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = string.Empty,
                    status = "error",
                    message = message
                };

                return Json(noti);
            }
        }
        #endregion

        #region Form 3.1 One to One Save > Flood Control Management > Technical Info
        //Tehnical Info
        //form/Form31TechInfoOneToOneSave :: f31otos
        [HttpPost]
        public JsonResult f31tiotos(Form31TechInfo _form31TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form31TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form31TechInfo != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);

                        if (pcd == null)
                        {
                            noti = new Notification
                            {
                                id = "",
                                status = "error",
                                message = "General information is missing. Please enter general information first."
                            };

                            goto Finish;
                        }

                        #region getting client end data
                        CcModAppProjectCommonDetail CommonDetail = _form31TechInfo.CommonDetail;
                        CcModAppProject_31_IndvDetail Project31Indv = _form31TechInfo.Project31Indv;

                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form31TechInfo.BDP2100HotSpot;
                        List<CcModPrjHydroRegionDetail> HydroRegion = _form31TechInfo.HydroRegion;
                        List<CcModPrjTypesOfFloodDetail> TypesOfFlood = _form31TechInfo.TypesOfFlood;
                        #endregion

                        #region Common Detail Data Binding
                        pcd.AnnualRainFallLast1Year = CommonDetail.AnnualRainFallLast1Year;
                        pcd.AnnualRainFallLast2Years = CommonDetail.AnnualRainFallLast2Years;
                        pcd.AnnualRainFallLast3Years = CommonDetail.AnnualRainFallLast3Years;
                        pcd.AnnualRainFallLast4Years = CommonDetail.AnnualRainFallLast4Years;
                        pcd.AnnualRainFallLast5Years = CommonDetail.AnnualRainFallLast5Years;
                        pcd.IssueChallageProblem = CommonDetail.IssueChallageProblem;
                        pcd.YesNoStakeId = CommonDetail.YesNoStakeId;
                        pcd.DiscussWithStakeApplicantCmt = CommonDetail.DiscussWithStakeApplicantCmt;
                        pcd.DiscussWithStakeAuthorityCmt = CommonDetail.DiscussWithStakeAuthorityCmt;
                        pcd.DiscussWithStakePosFeedback = CommonDetail.DiscussWithStakePosFeedback;
                        pcd.DiscussWithStakeNegFeedback = CommonDetail.DiscussWithStakeNegFeedback;
                        //pcd.DiscussWithStakeParticipntLst //file
                        //pcd.DiscussWithStakeMeetingMin //file
                        pcd.AnalyzeOptYesNoId = CommonDetail.AnalyzeOptYesNoId;
                        pcd.AnalyzeOptionsApplicantCmt = CommonDetail.AnalyzeOptionsApplicantCmt;
                        pcd.AnalyzeOptionsAuthorityCmt = CommonDetail.AnalyzeOptionsAuthorityCmt;
                        pcd.EnvAndSocialYesNoId = CommonDetail.EnvAndSocialYesNoId;
                        pcd.EnvAndSocialApplicantCmt = CommonDetail.EnvAndSocialApplicantCmt;
                        pcd.EnvAndSocialAuthorityCmt = CommonDetail.EnvAndSocialAuthorityCmt;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            CcModAppProject_31_IndvDetail p31i = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p31i == null)
                            {
                                _db.CcModAppProject_31_IndvDetail.Add(Project31Indv);

                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                }
                                else
                                {
                                    noti = new Notification
                                    {
                                        id = "",
                                        status = "error",
                                        message = "Please select hydrological region!"
                                    };

                                    goto Finish;
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                }
                                else
                                {
                                    noti = new Notification
                                    {
                                        id = "",
                                        status = "error",
                                        message = "Please select bangladesh delta plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (TypesOfFlood != null && TypesOfFlood.Count > 0)
                                {
                                    _db.CcModPrjTypesOfFloodDetail.AddRange(TypesOfFlood);
                                }
                                else
                                {
                                    noti = new Notification
                                    {
                                        id = "",
                                        status = "error",
                                        message = "Please select types of flood!"
                                    };

                                    goto Finish;
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project31Indv.Project31IndvId.ToString(),
                                        status = "success",
                                        message = "Information has been save successfully."
                                    };

                                    goto Finish;
                                }
                                else
                                {
                                    goto Rollback;
                                }
                            }
                            else
                            {
                                #region Deleting Before Data                                
                                if (HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p31i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p31i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (TypesOfFlood.Count > 0)
                                {
                                    List<CcModPrjTypesOfFloodDetail> tempTfList = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == p31i.ProjectId).ToList();

                                    if (tempTfList.Count > 0)
                                    {
                                        _db.CcModPrjTypesOfFloodDetail.RemoveRange(tempTfList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                _db.CcModPrjTypesOfFloodDetail.AddRange(TypesOfFlood);

                                //result = _db.SaveChanges();
                                //dbNewListTrans.Commit();
                                #endregion

                                #region Project 31 Indvidual Data Binding                                
                                p31i.ConnectivityAmidWaterland = Project31Indv.ConnectivityAmidWaterland;
                                p31i.CatchmentArea = Project31Indv.CatchmentArea;
                                p31i.HighestFloodLevel = Project31Indv.HighestFloodLevel;
                                p31i.MaximumDischarge = Project31Indv.MaximumDischarge;
                                p31i.DrainageConditionId = Project31Indv.DrainageConditionId;
                                p31i.WaterSalinity = Project31Indv.WaterSalinity;
                                p31i.WaterDO = Project31Indv.WaterDO;
                                p31i.WaterTDS = Project31Indv.WaterTDS;
                                p31i.WaterPhLevel = Project31Indv.WaterPhLevel;
                                p31i.HighLandPercent = Project31Indv.HighLandPercent;
                                p31i.MediumHighLandPercent = Project31Indv.MediumHighLandPercent;
                                p31i.MediumLowLandPercent = Project31Indv.MediumLowLandPercent;
                                p31i.LowLandPercent = Project31Indv.LowLandPercent;
                                p31i.VeryLowLandPercent = Project31Indv.VeryLowLandPercent;
                                p31i.CultivableCrops = Project31Indv.CultivableCrops;
                                p31i.CropProduction = Project31Indv.CropProduction;
                                p31i.FishProduction = Project31Indv.FishProduction;
                                p31i.FishDiversity = Project31Indv.FishDiversity;
                                p31i.FishMigration = Project31Indv.FishMigration;
                                p31i.FloraAndFauna = Project31Indv.FloraAndFauna;
                                p31i.LandLessPeoplePercentage = Project31Indv.LandLessPeoplePercentage;
                                p31i.SmallFarmerPercentage = Project31Indv.SmallFarmerPercentage;
                                p31i.AvgMonthlyIncome = Project31Indv.AvgMonthlyIncome;
                                p31i.UseOfToolsYesNoId = Project31Indv.UseOfToolsYesNoId;
                                p31i.ToolsApplicantComments = Project31Indv.ToolsApplicantComments;
                                p31i.ToolsAuthorityComments = Project31Indv.ToolsAuthorityComments;
                                #endregion

                                _db.Entry(p31i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project31Indv.Project31IndvId.ToString(),
                                        status = "success",
                                        message = "Information has been updated successfully."
                                    };

                                    goto Finish;
                                }
                                else
                                {
                                    goto Rollback;
                                }
                            }
                        }
                        else
                        {
                            goto Rollback;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "0", //_form31.HydroSysDetailId.ToString(),
                            status = "error",
                            message = "Saving data transaction has been rollbacked. " + message
                        };

                        goto Finish;
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };

                goto Finish;
            }

        Rollback:
            dbContextTransaction.Rollback();

            noti = new Notification
            {
                id = "",
                status = "error",
                message = "Saving data transaction has been rollbacked."
            };

            goto Finish;

        Finish:
            return Json(noti);
        }

        //Deed Obligatory
        //form/Form31DeedObligatoryOneToOneSave :: f31otos        
        [HttpPost]
        public JsonResult f31dootos(Form31DeedObligatory _form31DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form31DeedObli.CommonDetail.ProjectId;
            Int64 Project31IndvId = _form31DeedObli.Project31Indv.Project31IndvId;

            try
            {
                if (_form31DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_31_IndvDetail p31id = _db.CcModAppProject_31_IndvDetail.Find(Project31IndvId); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                        if (pcd == null)
                        {
                            noti = new Notification
                            {
                                id = "",
                                status = "error",
                                message = "General information is missing. Please enter general information first."
                            };

                            goto Finish;
                        }

                        if (p31id == null)
                        {
                            noti = new Notification
                            {
                                id = "",
                                status = "error",
                                message = "Technical information is missing. Please enter Technical information first."
                            };

                            goto Finish;
                        }

                        #region getting client end data
                        CcModAppProjectCommonDetail CommonDetail = _form31DeedObli.CommonDetail;
                        CcModAppProject_31_IndvDetail Project31Indv = _form31DeedObli.Project31Indv;

                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form31DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form31DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form31DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form31DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form31DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form31DeedObli.GPWMGroupTypeDetail;
                        #endregion

                        #region Common Detail Data Binding
                        pcd.CompatNWPYesNoId = CommonDetail.CompatNWPYesNoId;
                        pcd.CompatibilityNWPApplicantCmt = CommonDetail.CompatibilityNWPApplicantCmt;
                        pcd.CompatibilityNWPAuthorityCmt = CommonDetail.CompatibilityNWPAuthorityCmt;
                        //pcd.CompatibilityNWPDocLink //file
                        pcd.NWMPCompatYesNoId = CommonDetail.NWMPCompatYesNoId;
                        pcd.NWMPApplicantCmt = CommonDetail.NWMPApplicantCmt;
                        pcd.NWMPAuthorityCmt = CommonDetail.NWMPAuthorityCmt;
                        //pcd.NWMPDocLink //file
                        pcd.FYPYesNoId = CommonDetail.FYPYesNoId;
                        pcd.FYPApplicantCmt = CommonDetail.FYPApplicantCmt;
                        pcd.FYPAuthorityCmt = CommonDetail.FYPAuthorityCmt;
                        pcd.SDGYesNoId = CommonDetail.SDGYesNoId;
                        pcd.SDGApplicantCmt = CommonDetail.SDGApplicantCmt;
                        pcd.SDGAuthorityCmt = CommonDetail.SDGAuthorityCmt;
                        //pcd.SDGDocLink //file
                        pcd.DeltaPlanYesNoId = CommonDetail.DeltaPlanYesNoId;
                        pcd.DeltaPlan2100ApplicantCmt = CommonDetail.DeltaPlan2100ApplicantCmt;
                        pcd.DeltaPlan2100AuthorityCmt = CommonDetail.DeltaPlan2100AuthorityCmt;
                        //pcd.DeltaPlan2100DocLink //file
                        pcd.CostalZoneYesNoId = CommonDetail.CostalZoneYesNoId;
                        pcd.CostalZoneApplicantCmt = CommonDetail.CostalZoneApplicantCmt;
                        pcd.CostalZoneAuthorityCmt = CommonDetail.CostalZoneAuthorityCmt;
                        //pcd.CostalZoneDocLink //file.
                        pcd.AgriculturalYesNoId = CommonDetail.AgriculturalYesNoId;
                        pcd.AgriApplicantCmt = CommonDetail.AgriApplicantCmt;
                        pcd.AgriAuthorityCmt = CommonDetail.AgriAuthorityCmt;
                        //pcd.AgriDocLink //file
                        pcd.FisheriesYesNoId = CommonDetail.FisheriesYesNoId;
                        pcd.FisheriesApplicantCmt = CommonDetail.FisheriesApplicantCmt;
                        pcd.FisheriesAuthorityCmt = CommonDetail.FisheriesAuthorityCmt;
                        //pcd.FisheriesDocLink //file
                        pcd.IWRMYesNoId = CommonDetail.IWRMYesNoId;
                        pcd.IWRMApplicantCmt = CommonDetail.IWRMApplicantCmt;
                        pcd.IWRMAuthorityCmt = CommonDetail.IWRMAuthorityCmt;
                        pcd.GPWMYesNoId = CommonDetail.GPWMYesNoId;
                        pcd.GPWMApplicantCmt = CommonDetail.GPWMApplicantCmt;
                        pcd.GPWMAuthorityCmt = CommonDetail.GPWMAuthorityCmt;
                        pcd.FeasibilityYesNoId = CommonDetail.FeasibilityYesNoId;
                        pcd.FeasibilityApplicantCmt = CommonDetail.FeasibilityApplicantCmt;
                        pcd.FeasibilityAuthorityCmt = CommonDetail.FeasibilityAuthorityCmt;
                        pcd.SocialIssuesYesNoId = CommonDetail.SocialIssuesYesNoId;
                        pcd.SocialIssuesApplicantCmt = CommonDetail.SocialIssuesApplicantCmt;
                        pcd.SocialIssuesAuthorityCmt = CommonDetail.SocialIssuesAuthorityCmt;
                        #endregion

                        #region Project 31 Individual Data Binding
                        p31id.DuplicatYesNoId = Project31Indv.DuplicatYesNoId;
                        p31id.DuplicationApplicantComments = Project31Indv.DuplicationApplicantComments;
                        p31id.DuplicationAuthorityComments = Project31Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        _db.Entry(p31id).State = EntityState.Modified;

                        if (CompatNWPDetail != null && CompatNWPDetail.Count > 0)
                        {
                            List<CcModPrjCompatNWPDetail> _cnwp = _db.CcModPrjCompatNWPDetail.Where(w => w.ProjectId == ProjectId).ToList();

                            if (_cnwp.Count > 0)
                            {
                                _db.CcModPrjCompatNWPDetail.RemoveRange(_cnwp);
                            }

                            _db.CcModPrjCompatNWPDetail.AddRange(CompatNWPDetail);
                        }

                        if (CompatNWMPDetail != null && CompatNWMPDetail.Count > 0)
                        {
                            List<CcModPrjCompatNWMPDetail> _cnwmp = _db.CcModPrjCompatNWMPDetail.Where(w => w.ProjectId == ProjectId).ToList();

                            if (_cnwmp.Count > 0)
                            {
                                _db.CcModPrjCompatNWMPDetail.RemoveRange(_cnwmp);
                            }

                            _db.CcModPrjCompatNWMPDetail.AddRange(CompatNWMPDetail);
                        }

                        if (CompatSDGDetail != null && CompatSDGDetail.Count > 0)
                        {
                            List<CcModPrjCompatSDGDetail> _csdg = _db.CcModPrjCompatSDGDetail.Where(w => w.ProjectId == ProjectId).ToList();

                            if (_csdg.Count > 0)
                            {
                                _db.CcModPrjCompatSDGDetail.RemoveRange(_csdg);
                            }

                            _db.CcModPrjCompatSDGDetail.AddRange(CompatSDGDetail);
                        }

                        if (CompatSDGIndiDetail != null && CompatSDGIndiDetail.Count > 0)
                        {
                            List<CcModPrjCompatSDGIndiDetail> _csdgi = _db.CcModPrjCompatSDGIndiDetail.Where(w => w.ProjectId == ProjectId).ToList();

                            if (_csdgi.Count > 0)
                            {
                                _db.CcModPrjCompatSDGIndiDetail.RemoveRange(_csdgi);
                            }

                            _db.CcModPrjCompatSDGIndiDetail.AddRange(CompatSDGIndiDetail);
                        }

                        if (BDP2100GoalDetail != null && BDP2100GoalDetail.Count > 0)
                        {
                            List<CcModBDP2100GoalDetail> _bdp2100goal = _db.CcModBDP2100GoalDetail.Where(w => w.ProjectId == ProjectId).ToList();

                            if (_bdp2100goal.Count > 0)
                            {
                                _db.CcModBDP2100GoalDetail.RemoveRange(_bdp2100goal);
                            }

                            _db.CcModBDP2100GoalDetail.AddRange(BDP2100GoalDetail);
                        }

                        if (GPWMGroupTypeDetail != null && GPWMGroupTypeDetail.Count > 0)
                        {
                            List<CcModGPWMGroupTypeDetail> _gpwm = _db.CcModGPWMGroupTypeDetail.Where(w => w.ProjectId == ProjectId).ToList();

                            if (_gpwm.Count > 0)
                            {
                                _db.CcModGPWMGroupTypeDetail.RemoveRange(_gpwm);
                            }

                            _db.CcModGPWMGroupTypeDetail.AddRange(GPWMGroupTypeDetail);
                        }

                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = pcd.ProjectId.ToString(),
                                status = "success",
                                message = "Information has been saved successfully. "
                            };

                            goto Finish;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "", //_form31.HydroSysDetailId.ToString(),
                            status = "error",
                            message = "Saving data transaction has been rollbacked. " + message
                        };

                        goto Finish;
                    }
                }

            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };

                goto Finish;
            }

        Rollback:
            dbContextTransaction.Rollback();

            noti = new Notification
            {
                id = "",
                status = "error",
                message = "Saving data transaction has been rollbacked."
            };

            goto Finish;

        Finish:
            return Json(noti);
        }

        //05 03 2020
        //Tehnical Info
        //form/Form31RecommandDetailSave :: f31rds > Administrative
        [HttpPost]
        public JsonResult f31rds(CcModAppProjectCommonDetail _recDetail)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _recDetail.ProjectId;

            try
            {
                if (_recDetail != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);

                        if (pcd == null)
                        {
                            noti = new Notification
                            {
                                id = "",
                                status = "error",
                                message = "General information is missing. Please enter general information first."
                            };

                            goto Finish;
                        }

                        #region Common Detail Data Binding for Recommendation
                        pcd.NocTypeId = _recDetail.NocTypeId;
                        pcd.PaymentMethodId = _recDetail.PaymentMethodId;
                        pcd.PaymentDocNumber = _recDetail.PaymentDocNumber;
                        pcd.PaidAmount = _recDetail.PaidAmount;
                        pcd.RecommendationId = _recDetail.RecommendationId;
                        pcd.RecommendationComment = _recDetail.RecommendationComment;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            dbContextTransaction.Commit();

                            noti = new Notification
                            {
                                id = pcd.ProjectId.ToString(),
                                status = "success",
                                message = "Administrative information has been save successfully."
                            };

                            goto Finish;
                        }
                        else
                        {
                            goto Rollback;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = "", //_form31.HydroSysDetailId.ToString(),
                            status = "error",
                            message = "Saving data transaction has been rollbacked. " + message
                        };

                        goto Finish;
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    message = message
                };

                goto Finish;
            }

        Rollback:
            dbContextTransaction.Rollback();

            noti = new Notification
            {
                id = "",
                status = "error",
                message = "Saving data transaction has been rollbacked."
            };

            goto Finish;

        Finish:
            return Json(noti);
        }
        #endregion

        #region Delete
        //form/CommonDeleteMethod :: cdm
        [HttpPost]
        public JsonResult cdm(long id, long projectId, string modelName)
        {
            if (id != 0)
            {
                if (projectId != 0)
                {
                    using var dbContextTransaction = _db.Database.BeginTransaction();
                    try
                    {
                        switch (modelName)
                        {
                            case "CcModPrjLocationDetail":
                                CcModPrjLocationDetail _loc = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == projectId && w.LocationId == id).FirstOrDefault();
                                _db.CcModPrjLocationDetail.Remove(_loc);
                                break;

                            case "CcModHydroSystemDetail":
                                CcModHydroSystemDetail _hsd = _db.CcModHydroSystemDetail.Where(w => w.ProjectId == projectId && w.HydroSysDetailId == id).FirstOrDefault();
                                _db.CcModHydroSystemDetail.Remove(_hsd);
                                break;

                            case "CcModFloodFrequencyDetail":
                                CcModFloodFrequencyDetail _fsd = _db.CcModFloodFrequencyDetail.Where(w => w.ProjectId == projectId && w.FloodFrequencyDetailId == id).FirstOrDefault();
                                _db.CcModFloodFrequencyDetail.Remove(_fsd);
                                break;

                            case "CcModPrjIrrigCropAreaDetail":
                                CcModPrjIrrigCropAreaDetail _ica = _db.CcModPrjIrrigCropAreaDetail.Where(w => w.ProjectId == projectId && w.IrrigatedCropId == id).FirstOrDefault();
                                _db.CcModPrjIrrigCropAreaDetail.Remove(_ica);
                                break;

                            case "CcModAnalyzeOptionsDetail":
                                CcModAnalyzeOptionsDetail _aod = _db.CcModAnalyzeOptionsDetail.Where(w => w.ProjectId == projectId && w.AnalyzeOptionsId == id).FirstOrDefault();
                                _db.CcModAnalyzeOptionsDetail.Remove(_aod);
                                break;

                            case "CcModPrjEIADetail":
                                CcModPrjEIADetail _eia = _db.CcModPrjEIADetail.Where(w => w.ProjectId == projectId && w.EIAId == id).FirstOrDefault();
                                _db.CcModPrjEIADetail.Remove(_eia);
                                break;

                            case "CcModPrjSIADetail":
                                CcModPrjSIADetail _sia = _db.CcModPrjSIADetail.Where(w => w.ProjectId == projectId && w.SIAId == id).FirstOrDefault();
                                _db.CcModPrjSIADetail.Remove(_sia);
                                break;

                            //CcModPrjEcoFinAnalysisDetail
                            case "CcModPrjEcoFinAnalysisDetail":
                                CcModPrjEcoFinAnalysisDetail _efa = _db.CcModPrjEcoFinAnalysisDetail.Where(w => w.ProjectId == projectId && w.EconomicalAndFinancialId == id).FirstOrDefault();
                                _db.CcModPrjEcoFinAnalysisDetail.Remove(_efa);
                                break;
                        }

                        _db.SaveChanges();
                        dbContextTransaction.Commit();

                        noti = new Notification
                        {
                            id = id.ToString(),
                            status = "success",
                            message = "Information has been deleted successfully."
                        };
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        var message = ch.ExtractInnerException(ex);

                        noti = new Notification
                        {
                            id = id.ToString(),
                            status = "error",
                            message = message + "<br /> Model Name: " + modelName
                        };
                    }
                }
            }

            return Json(noti);
        }
        #endregion

        #region Get Single Row Data
        //form/GetSingleLocation :: gsl
        [HttpGet]
        public JsonResult gsl(long id, long projectId)
        {
            CcModPrjLocationDetail _loc = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == projectId && w.LocationId == id).FirstOrDefault();
            return Json(_loc);
        }

        //form/GetSingleHydrologicalSystem :: gshs
        [HttpGet]
        public JsonResult gshs(long id, long projectId)
        {
            CcModHydroSystemDetail _hsd = _db.CcModHydroSystemDetail.Where(w => w.ProjectId == projectId && w.HydroSysDetailId == id).FirstOrDefault();
            return Json(_hsd);
        }

        //form/GetSingleFloodFrequency :: gsff
        [HttpGet]
        public JsonResult gsff(long id, long projectId)
        {
            CcModFloodFrequencyDetail _ffd = _db.CcModFloodFrequencyDetail.Where(w => w.ProjectId == projectId && w.FloodFrequencyDetailId == id).FirstOrDefault();
            return Json(_ffd);
        }

        //form/GetSingleIrrigCropArea :: gsica
        [HttpGet]
        public JsonResult gsica(long id, long projectId)
        {
            CcModPrjIrrigCropAreaDetail _ica = _db.CcModPrjIrrigCropAreaDetail.Where(w => w.ProjectId == projectId && w.IrrigatedCropId == id).FirstOrDefault();
            return Json(_ica);
        }

        //form/GetSingleAnalyzeOptionsDetail :: gsaod
        [HttpGet]
        public JsonResult gsaod(long id, long projectId)
        {
            CcModAnalyzeOptionsDetail _aod = _db.CcModAnalyzeOptionsDetail.Where(w => w.ProjectId == projectId && w.AnalyzeOptionsId == id).FirstOrDefault();
            return Json(_aod);
        }

        //form/GetSingleDesignSubmittedWithProjectDocument :: gsdswpd
        [HttpGet]
        public JsonResult gsdswpd(long id, long projectId)
        {
            CcModDesignSubmitDetail _dsd = _db.CcModDesignSubmitDetail.Where(w => w.ProjectId == projectId && w.DesignSubmittedId == id).FirstOrDefault();
            return Json(_dsd);
        }

        //form/GetSingleDesignSubmittedWithProjectDocument :: gseia
        [HttpGet]
        public JsonResult gseia(long id, long projectId)
        {
            CcModPrjEIADetail _eia = _db.CcModPrjEIADetail.Where(w => w.ProjectId == projectId && w.EIAId == id).FirstOrDefault();
            return Json(_eia);
        }

        //form/GetSingleDesignSubmittedWithProjectDocument :: gssia
        [HttpGet]
        public JsonResult gssia(long id, long projectId)
        {
            CcModPrjSIADetail _sia = _db.CcModPrjSIADetail.Where(w => w.ProjectId == projectId && w.SIAId == id).FirstOrDefault();
            return Json(_sia);
        }

        //form/GetSingleEcoFinAnalysisDetail :: gsefa
        [HttpGet]
        public JsonResult gsefa(long id, long projectId)
        {
            CcModPrjEcoFinAnalysisDetail _efa = _db.CcModPrjEcoFinAnalysisDetail.Where(w => w.ProjectId == projectId && w.EconomicalAndFinancialId == id).FirstOrDefault();
            return Json(_efa);
        }

        //form/GetFormComments :: sfc
        [HttpPost]
        public JsonResult sfc(CcModAppProjDataAnalysis pda)
        {
            int result = 0;
            if (pda.ProjectTypeId != 0)
            {
                if (pda.ProjectId != 0)
                {
                    if (!string.IsNullOrEmpty(pda.LabelNameOfControl))
                    {
                        if (!string.IsNullOrEmpty(pda.Comments))
                        {
                            using var dbContextTransaction = _db.Database.BeginTransaction();
                            try
                            {
                                CcModAppProjDataAnalysis exists = _db.CcModAppProjDataAnalysis
                                                                     .Where(w =>
                                                                                w.UserId == pda.UserId &&
                                                                                w.ProjectTypeId == pda.ProjectTypeId &&
                                                                                w.ProjectId == pda.ProjectId &&
                                                                                w.LabelNameOfControl == pda.LabelNameOfControl)
                                                                     .FirstOrDefault();

                                if (exists != null)
                                {
                                    exists.Comments = pda.Comments;
                                    _db.Entry(exists).State = EntityState.Modified;
                                }
                                else
                                {
                                    pda.DateOfAnalysis = DateTime.Now;
                                    _db.CcModAppProjDataAnalysis.Add(pda);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = pda.AppProjDataAnalysisId.ToString(),
                                        status = "success",
                                        title = "Success",
                                        message = "Your comments has been submitted."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = pda.AppProjDataAnalysisId.ToString(),
                                        status = "error",
                                        title = "Error",
                                        message = "Your comments not submitted."
                                    };
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
                        else
                        {
                            noti = new Notification
                            {
                                id = "0",
                                status = "error",
                                title = "Empty Comments",
                                message = "Comments should not be left empty."
                            };
                        }
                    }
                    else
                    {
                        noti = new Notification
                        {
                            id = "0",
                            status = "error",
                            title = "Empty Control Label",
                            message = "Control label should not be left empty."
                        };
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "error",
                        title = "Project Id Required",
                        message = "Project Id should not be left empty."
                    };
                }
            }
            else
            {
                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    title = "Project Type Required",
                    message = "Project type should not be left empty."
                };
            }

            return Json(noti);
        }

        //form/GetFormComments :: gfc
        [HttpGet]
        public JsonResult gfc(long userid, int projtypeid, long projid, string controlname)
        {
            var comments = (from d in _db.CcModAppProjDataAnalysis
                            join u in _db.AdminModUsersDetail on d.UserId equals u.UserId into uList
                            from user in uList.DefaultIfEmpty()
                            join a in _db.AdminModUserGrpDistDetail on user.UserId equals a.UserId into aList
                            from ugd in aList.DefaultIfEmpty()
                            join ug in _db.LookUpAdminModUserGroup on ugd.UserGroupId equals ug.UserGroupId into ugList
                            from ugl in ugList.DefaultIfEmpty()

                            where d.ProjectTypeId == projtypeid &&
                                  d.ProjectId == projid &&
                                  d.LabelNameOfControl == controlname

                            select new FormCommentsListTemp
                            {
                                AppProjDataAnalysisId = d.AppProjDataAnalysisId,
                                UserId = d.UserId,
                                UserComments = d.Comments,
                                UserName = String.Format("{0}, {1}", user.UserFullName, ugl.UserGroupName),
                                DateOfAnalysis = (d.DateOfAnalysis.HasValue) ? d.DateOfAnalysis.Value.ToString("dd MMM, yyyy hh:mm:ss tt") : ""
                            }).ToList();

            return Json(comments);
        }

        public List<DataAnalysisControlComments> GetFormDataAnalysisComments(int project_type_id, long project_id)
        {
            List<DataAnalysisControlComments> result = new List<DataAnalysisControlComments>();

            var comments = (from d in _db.CcModAppProjDataAnalysis
                            join u in _db.AdminModUsersDetail on d.UserId equals u.UserId into uList
                            from user in uList.DefaultIfEmpty()
                            join a in _db.AdminModUserGrpDistDetail on user.UserId equals a.UserId into aList
                            from ugd in aList.DefaultIfEmpty()
                            join ug in _db.LookUpAdminModUserGroup on ugd.UserGroupId equals ug.UserGroupId into ugList
                            from ugl in ugList.DefaultIfEmpty()

                            where d.ProjectTypeId == project_type_id &&
                                  d.ProjectId == project_id

                            select new FormCommentsListTemp
                            {
                                AppProjDataAnalysisId = d.AppProjDataAnalysisId,
                                UserId = d.UserId,
                                LabelNameOfControl = d.LabelNameOfControl,
                                UserComments = d.Comments,
                                UserName = String.Format("{0}, {1}", user.UserFullName, ugl.UserGroupName),
                                DateOfAnalysis = (d.DateOfAnalysis.HasValue) ? d.DateOfAnalysis.Value.ToString("dd MMM, yyyy hh:mm:ss tt") : ""
                            }).ToList();

            if (comments.Count > 0)
            {
                result = comments.GroupBy(g => g.LabelNameOfControl)
                        .Select(s => new DataAnalysisControlComments
                        {
                            ControlTitle = s.Key,
                            Comments = string.Join("<br /><hr />", s.Select(ss => ss.UserComments + "<br />--" + ss.UserName + "<br />" + ss.DateOfAnalysis))
                        }).ToList();
            }

            return result;
        }
        #endregion

        #region File Uploading
        //form/UploadProjectLocationFile :: uplf
        [HttpPost]
        [Obsolete]
        public JsonResult uplf(long locationid, long projectid, IFormFile file)
        {
            int result = 0;
            string filename = "", extension = "", foldername = "images/ProjectLocationPhotos";
            CcModPrjLocationDetail location = new CcModPrjLocationDetail();

            if (locationid != 0)
            {
                location = _db.CcModPrjLocationDetail.Find(locationid);
            }

            try
            {
                if (location != null && file != null)
                {
                    filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    extension = filename.Substring(filename.IndexOf('.'));
                    filename = EnsureCorrectFilename(filename);
                    filename = GetGenProjLocFilename(location) + extension;

                    location.ImageFileName = filename;

                    _db.Entry(location).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    if (result > 0)
                    {
                        using FileStream output = System.IO.File.Create(GetPathAndFilename(filename, foldername));
                        file.CopyTo(output);

                        noti = new Notification
                        {
                            id = location.LocationId.ToString(),
                            status = "success",
                            title = "Success",
                            message = "File has been successfully uploaded."
                        };
                    }
                    else
                    {
                        noti = new Notification
                        {
                            id = location.LocationId.ToString(),
                            status = "error",
                            title = "File Submission Error",
                            message = "Your selected file has not submitted."
                        };
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "warning",
                        title = "Select File",
                        message = "No file(s) selected!"
                    };
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    title = "An Exception Error Occured",
                    message = message
                };
            }

            return Json(noti);
        }

        //form/UploadCommonDetailFile :: ucdf
        [HttpPost]
        [Obsolete]
        public JsonResult ucdf(long projectid, string controltitle, IFormFile file)
        {
            int result = 0;
            string filename = "", extension = "", foldername = "images/CommonDetails";
            CcModAppProjectCommonDetail pcd = new CcModAppProjectCommonDetail();

            if (projectid != 0)
            {
                pcd = _db.CcModAppProjectCommonDetail.Find(projectid);
            }

            try
            {
                if (pcd != null && file != null)
                {
                    filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    extension = filename.Substring(filename.IndexOf('.'));
                    filename = EnsureCorrectFilename(filename);
                    filename = GetCommonDetailFileName(projectid.ToString(), controltitle) + extension;

                    switch (controltitle)
                    {
                        case "DiscussWithStakeParticipntLst":
                            pcd.DiscussWithStakeParticipntLst = filename;
                            break;

                        case "DiscussWithStakeMeetingMin":
                            pcd.DiscussWithStakeMeetingMin = filename;
                            break;

                        case "CompatibilityNWPDocLink":
                            pcd.CompatibilityNWPDocLink = filename;
                            break;

                        case "NWMPDocLink":
                            pcd.NWMPDocLink = filename;
                            break;

                        case "SDGDocLink":
                            pcd.SDGDocLink = filename;
                            break;

                        case "DeltaPlan2100DocLink":
                            pcd.DeltaPlan2100DocLink = filename;
                            break;

                        case "CostalZoneDocLink":
                            pcd.CostalZoneDocLink = filename;
                            break;

                        case "AgriDocLink":
                            pcd.AgriDocLink = filename;
                            break;

                        case "FisheriesDocLink":
                            pcd.FisheriesDocLink = filename;
                            break;

                        case "NocFileName":
                            pcd.NocFileName = filename;
                            break;

                        case "PaymentDocFileName":
                            pcd.PaymentDocFileName = filename;
                            break;
                    }

                    _db.Entry(pcd).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    if (result > 0)
                    {
                        using FileStream output = System.IO.File.Create(GetPathAndFilename(filename, foldername));
                        file.CopyTo(output);

                        noti = new Notification
                        {
                            id = pcd.ProjectId.ToString(),
                            status = "success",
                            title = "Success",
                            message = "File has been successfully uploaded."
                        };
                    }
                    else
                    {
                        noti = new Notification
                        {
                            id = pcd.ProjectId.ToString(),
                            status = "error",
                            title = "File Submission Error",
                            message = "Your selected file has not submitted."
                        };
                    }
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "warning",
                        title = "Select File",
                        message = "No file(s) selected!"
                    };
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    title = "An Exception Error Occured",
                    message = message
                };
            }

            return Json(noti);
        }

        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> UploadFileAsync(IList<IFormFile> files, long projectid, string foldername, string controltitle)
        {
            try
            {
                if (files.Count > 0)
                {
                    foreach (IFormFile source in files)
                    {
                        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
                        filename = EnsureCorrectFilename(filename);

                        using FileStream output = System.IO.File.Create(GetPathAndFilename(filename, foldername));
                        await source.CopyToAsync(output);
                    }

                    noti = new Notification
                    {
                        id = "0",
                        status = "success",
                        title = "Success",
                        message = "File has been successfully uploaded."
                    };
                }
                else
                {
                    noti = new Notification
                    {
                        id = "0",
                        status = "warning",
                        title = "Select File",
                        message = "No file(s) selected!"
                    };
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    title = "An Exception Error Occured",
                    message = message
                };
            }

            return Json(noti);
        }

        private string GetGenProjLocFilename(CcModPrjLocationDetail location)
        {
            string result = string.Empty;
            string adminboundary = !string.IsNullOrEmpty(location.UnionGeoCode) ? location.UnionGeoCode.PadLeft(8, '0') :
                                   !string.IsNullOrEmpty(location.UpazilaGeoCode) ? location.UpazilaGeoCode.PadLeft(8, '0') :
                                   !string.IsNullOrEmpty(location.DistrictGeoCode) ? location.DistrictGeoCode.PadLeft(8, '0') : "";

            if (location.LocationId != 0)
            {
                int fileCount = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == location.ProjectId).Count();
                result = location.ProjectId.ToString() + "_" + fileCount.ToString() + "_" + adminboundary;
            }

            return result;
        }

        private string GetCommonDetailFileName(string projectId, string control_title)
        {
            string result = string.Empty;

            switch (control_title)
            {
                case "DiscussWithStakeParticipntLst":
                    result = projectId + "_DWSPL_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "DiscussWithStakeMeetingMin":
                    result = projectId + "_DWSMM_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "CompatibilityNWPDocLink":
                    result = projectId + "_CNWPD_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "NWMPDocLink":
                    result = projectId + "_NWMPD_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "SDGDocLink":
                    result = projectId + "_SDGDL_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "DeltaPlan2100DocLink":
                    result = projectId + "_DP2100DL_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "CostalZoneDocLink":
                    result = projectId + "_CZDL_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "AgriDocLink":
                    result = projectId + "_AGRDL_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "FisheriesDocLink":
                    result = projectId + "_FISDL_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "NocFileName":
                    result = projectId + "_NOC_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;

                case "PaymentDocFileName":
                    result = projectId + "_PAYDF_" + DateTime.Now.ToString("yyMMddHHmmssfff");
                    break;
            }

            return result;
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string fileName, string folderName)
        {
            var path = Path.Combine(hostingEnvironment.WebRootPath, folderName, fileName);
            return path;
        }
        #endregion

        #region Application Approving and Forwarding
        //form/ForwardApplication :: fwapp
        [HttpPost]
        public JsonResult fwapp(long pid)
        {
            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            List<LookUpAdminModUserGroup> userGroupList = new List<LookUpAdminModUserGroup>();
            int appState = 0, result = 0;

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
                userGroupList = userGroupList.Where(w => w.AuthorityLevelId < uli.AuthorityLevelId).ToList();
                int maxAuthLevelId = userGroupList.Max(m => m.AuthorityLevelId).Value;

                LookUpAdminModUserGroup nextLevelInfo = userGroupList.Where(w => w.AuthorityLevelId == maxAuthLevelId).FirstOrDefault();
                appState = _db.LookUpCcModApplicationState.Where(w => w.AuthorityLevelId == nextLevelInfo.AuthorityLevelId).Select(s => s.ApplicationStateId).FirstOrDefault();
            }

            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(pid);

                if (_pcd != null)
                {
                    _pcd.ApplicationStateId = appState;
                    _pcd.IsCompletedId = 0;

                    _db.Entry(_pcd).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    if (result > 0)
                    {
                        dbContextTransaction.Commit();

                        noti = new Notification
                        {
                            id = pid.ToString(),
                            status = "success",
                            title = "Success",
                            message = "Application has been successfully forwarded to next level authority. Application tracking code is: " + _pcd.AppSubmissionId
                        };
                    };
                }
                else
                {
                    dbContextTransaction.Rollback();

                    noti = new Notification
                    {
                        id = pid.ToString(),
                        status = "error",
                        title = "Forwarding Error",
                        message = "Application not forwarded. Application tracking code is: " + _pcd.AppSubmissionId
                    };
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

            return Json(noti);
        }

        //form/ApplicationApprove :: appaprv
        [HttpPost]
        public JsonResult appaprv(long pid)
        {
            //UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            //List<LookUpAdminModUserGroup> userGroupList = new List<LookUpAdminModUserGroup>();
            int result = 0;

            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(pid);

                if (_pcd != null)
                {
                    _pcd.ApprovalStatusId = 1;
                    _pcd.IsCompletedId = 3;

                    _db.Entry(_pcd).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    if (result > 0)
                    {
                        dbContextTransaction.Commit();

                        noti = new Notification
                        {
                            id = pid.ToString(),
                            status = "success",
                            title = "Success",
                            message = "Application has been successfully approved. Application tracking code is: " + _pcd.AppSubmissionId
                        };
                    };
                }
                else
                {
                    dbContextTransaction.Rollback();

                    noti = new Notification
                    {
                        id = pid.ToString(),
                        status = "error",
                        title = "Forwarding Error",
                        message = "Application not approved. Application tracking code is: " + _pcd.AppSubmissionId
                    };
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

            return Json(noti);
        }

        //form/ApplicationReject :: apprejt
        [HttpPost]
        public JsonResult apprejt(long pid, string reason)
        {
            //UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            //List<LookUpAdminModUserGroup> userGroupList = new List<LookUpAdminModUserGroup>();
            int result = 0;

            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(pid);

                if (_pcd != null)
                {
                    _pcd.ApprovalStatusId = 3;
                    _pcd.ReasonOfRejection = reason.Trim();
                    _pcd.IsCompletedId = 3;

                    _db.Entry(_pcd).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    if (result > 0)
                    {
                        dbContextTransaction.Commit();

                        noti = new Notification
                        {
                            id = pid.ToString(),
                            status = "success",
                            title = "Success",
                            message = "Application has been successfully rejected. Application tracking code is: " + _pcd.AppSubmissionId
                        };
                    };
                }
                else
                {
                    dbContextTransaction.Rollback();

                    noti = new Notification
                    {
                        id = pid.ToString(),
                        status = "error",
                        title = "Forwarding Error",
                        message = "Application not rejected. Application tracking code is: " + _pcd.AppSubmissionId
                    };
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

            return Json(noti);
        }

        //form/ApplicationRe-evaluate :: appreeva
        [HttpPost]
        public JsonResult appreeva(long pid, string level, string reason)
        {
            //UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            //List<LookUpAdminModUserGroup> userGroupList = new List<LookUpAdminModUserGroup>();
            int result = 0;
            string message = string.Empty;

            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(pid);

                if (_pcd != null)
                {
                    _pcd.ApprovalStatusId = 2;
                    _pcd.ReasonOfRejection = reason.Trim();
                    _pcd.IsCompletedId = 0;

                    _db.Entry(_pcd).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    if (result > 0)
                    {
                        dbContextTransaction.Commit();

                        if (level == "applicant")
                        {
                            message = "Application has been sent to applicant for correction purpose.";
                        }
                        else if (level == "wrmc")
                        {
                            message = "Application will be sent to Water Resource Management Committee for re-checking purpose.";
                        }
                        else
                        {
                            message = "Application will be sent to Technical Committee for re-evaluation purpose.";
                        }


                        noti = new Notification
                        {
                            id = pid.ToString(),
                            status = "success",
                            title = "Success",
                            message = "Application has been successfully rejected. Application tracking code is: " + _pcd.AppSubmissionId
                        };
                    };
                }
                else
                {
                    dbContextTransaction.Rollback();

                    noti = new Notification
                    {
                        id = pid.ToString(),
                        status = "error",
                        title = "Forwarding Error",
                        message = "Application not sent to re-evaluate. Application tracking code is: " + _pcd.AppSubmissionId
                    };
                }
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                message = ch.ExtractInnerException(ex);

                noti = new Notification
                {
                    id = "0",
                    status = "error",
                    title = "An Exception Error Occured",
                    message = message
                };
            }

            return Json(noti);
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
        #endregion
    }
}
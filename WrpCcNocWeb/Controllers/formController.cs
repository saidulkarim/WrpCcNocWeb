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

                    return RedirectToAction("fcmp", "form"); //Flood Cotrol Management
                }
                else if (_selectedForm.ProjectTypeId.Equals("2"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("swwdu", "form"); //Surface Water Withdrawal, Distribution or Use
                }
                else if (_selectedForm.ProjectTypeId.Equals("3"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("ipsw", "form"); //Irrigation Project by Surface Water
                }
                else if (_selectedForm.ProjectTypeId.Equals("4"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("pchi", "form"); //Project for Construction of Hydraulic Infrastructure
                }
                else if (_selectedForm.ProjectTypeId.Equals("5"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("swrp", "form"); //Surface Water Reservation Project
                }
                else if (_selectedForm.ProjectTypeId.Equals("6"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("wldp", "form"); //Wetland Development Project
                }
                else if (_selectedForm.ProjectTypeId.Equals("7"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("swis", "form"); //Project for Surface Water Use in Industrial Sector
                }
                else if (_selectedForm.ProjectTypeId.Equals("8"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("rbpp", "form"); //River Bank Protection Project
                }
                else if (_selectedForm.ProjectTypeId.Equals("9"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("redp", "form"); //River Excavation or Dredging Project
                }
                else if (_selectedForm.ProjectTypeId.Equals("10"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("eekp", "form"); //Excavation-Excavation of Khal Project
                }
                else if (_selectedForm.ProjectTypeId.Equals("11"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("fpsw", "form"); //Excavation-Excavation of Khal Project
                }
                else if (_selectedForm.ProjectTypeId.Equals("12"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("gwwu", "form"); //Project for Ground Water Withdrawal, Distribution or Use
                }
                else if (_selectedForm.ProjectTypeId.Equals("13"))
                {
                    HttpContext.Response.Cookies.Append("FormLanguage", _selectedForm.LanguageTypeId, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });

                    return RedirectToAction("fcdi", "form"); //Special Project by DG
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
                           IsCompletedId = p.IsCompletedId
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
                           IsCompletedId = p.IsCompletedId
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
                               IsCompletedId = p.IsCompletedId
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
                               IsCompletedId = p.IsCompletedId
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
                               IsCompletedId = p.IsCompletedId
                           }).ToList();
                }
            }

            ViewBag.PCDS = pvl;
            return View();
        }

        public IActionResult status()
        {
            return View();
        }

        public IActionResult file()
        {
            return View();
        }

        #region  Print View
        //Form 3.1: Flood Control Management Project Print View
        public IActionResult view31(long id)
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
            //ChangeStatus(id, status);

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

            ViewBag.ApplicationState = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfo(pcd.UserId);

            return View();
        }

        //Form 3.2: Surface Water Withdrawal, Distribution or Use Print View
        public IActionResult view32(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_32_IndvDetail _indvdetail = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail32"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail32"] = new CcModAppProject_32_IndvDetail();

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

            var _pwudetail = _db.CcModProposedWaterUseDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModProposedWaterUse)
                                    .Select(x => new ProposedWaterUseDetailTemp
                                    {
                                        ProposedWaterUseId = x.ProposedWaterUseId,
                                        WaterUseTypeName = x.LookUpCcModProposedWaterUse.WaterUseTypeName,
                                        WaterUseTypeNameBn = x.LookUpCcModProposedWaterUse.WaterUseTypeNameBn
                                    }).ToList();
            ViewData["ProposedWaterUseDetail"] = _pwudetail;

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverType)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _towbdetail = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModTypeOfWaterBody)
                                    .Select(x => new TypeOfWaterBodyDetailTemp
                                    {
                                        WaterBodyTypeId = x.WaterBodyTypeId.Value,
                                        WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
                                        WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
                                    }).ToList();
            ViewData["TypeOfWaterBodyDetail"] = _towbdetail;

            var _hydrosystemdetail = GetHydroSystemDetail(pcd.ProjectId);
            ViewData["HydroSystemDetail"] = _hydrosystemdetail;

            var _sdidetail = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _swadetail = (from d in _db.CcModPrjSWADetail
                              where d.ProjectId == pcd.ProjectId
                              select new SWATemp
                              {
                                  SWAId = d.SWAId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  MinWaterFlow = d.MinWaterFlow,
                                  WaterDemandMonth = d.WaterDemandMonth
                              }).OrderBy(o => o.SWAId).ToList();
            ViewData["SurfaceWaterAvailDetail"] = _swadetail;

            var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModWtrDiversionSource)
                                    .Select(x => new WtrDiversionSourceDetailTemp
                                    {
                                        WaterDiversionSourceId = x.WaterDiversionSourceId,
                                        SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                        SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                    }).ToList();
            ViewData["WtrDiversionSourceDetail"] = _wdsdetail;

            var _gwddetail = (from d in _db.CcModPrjGrndWtrDepthDetail
                              where d.ProjectId == pcd.ProjectId
                              select new GrndWtrDepthDetailTemp
                              {
                                  GrndWtrDepthDetailId = d.GrndWtrDepthDetailId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  WaterDepth = d.WaterDepth
                              }).OrderBy(o => o.GrndWtrDepthDetailId).ToList(); ;
            ViewData["GrndWtrDepthDetail"] = _gwddetail;

            var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModGroundWaterQuality)
                                    .Select(x => new LookUpCcModGroundWaterQuality
                                    {
                                        GroundWaterQualityId = x.GroundWaterQualityId,
                                        ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                        ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                                    }).ToList();
            ViewData["GroundWaterQltDetail"] = _gwqdetail;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project32IndvId"] = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project32IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Flood Control Management Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.3: Irrigation Project by Surface Water Print View
        public IActionResult view33(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_33_IndvDetail _indvdetail = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail33"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail33"] = new CcModAppProject_33_IndvDetail { Project33IndvId = 0, ProjectId = 0 };

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

            var _towbdetail = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModTypeOfWaterBody)
                                    .Select(x => new TypeOfWaterBodyDetailTemp
                                    {
                                        WaterBodyTypeId = x.WaterBodyTypeId.Value,
                                        WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
                                        WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
                                    }).ToList();
            ViewData["TypeOfWaterBodyDetail"] = _towbdetail;

            var _hydrosystemdetail = GetHydroSystemDetail(pcd.ProjectId);
            ViewData["HydroSystemDetail"] = _hydrosystemdetail;

            var _sdidetail = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _icadetails = (from d in _db.CcModPrjIrrigCropAreaDetail
                               where d.ProjectId == pcd.ProjectId
                               select new IrrigCropAreaDetailTemp
                               {
                                   IrrigatedCropId = d.IrrigatedCropId,
                                   ProjectId = d.ProjectId,
                                   CropName = d.CropName,
                                   Area = d.Area.ToString()
                               }).ToList();
            ViewData["IrrigCropAreaDetailTemp"] = _icadetails;

            var _swadetail = (from d in _db.CcModPrjSWADetail
                              where d.ProjectId == pcd.ProjectId
                              select new SWATemp
                              {
                                  SWAId = d.SWAId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  MinWaterFlow = d.MinWaterFlow,
                                  WaterDemandMonth = d.WaterDemandMonth
                              }).OrderBy(o => o.SWAId).ToList(); ;
            ViewData["SurfaceWaterAvailDetail"] = _swadetail;

            var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModWtrDiversionSource)
                                    .Select(x => new WtrDiversionSourceDetailTemp
                                    {
                                        WaterDiversionSourceId = x.WaterDiversionSourceId,
                                        SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                        SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                    }).ToList();
            ViewData["WtrDiversionSourceDetail"] = _wdsdetail;

            var _gwddetail = (from d in _db.CcModPrjGrndWtrDepthDetail
                              where d.ProjectId == pcd.ProjectId
                              select new GrndWtrDepthDetailTemp
                              {
                                  GrndWtrDepthDetailId = d.GrndWtrDepthDetailId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  WaterDepth = d.WaterDepth
                              }).OrderBy(o => o.GrndWtrDepthDetailId).ToList(); ;
            ViewData["GrndWtrDepthDetail"] = _gwddetail;

            var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModGroundWaterQuality)
                                    .Select(x => new LookUpCcModGroundWaterQuality
                                    {
                                        GroundWaterQualityId = x.GroundWaterQualityId,
                                        ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                        ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                                    }).ToList();
            ViewData["GroundWaterQltDetail"] = _gwqdetail;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project33IndvId"] = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project33IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Irrigation Project by Surface Water | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.4: Project for Construction of Hydraulic Infrastructure
        public IActionResult view34(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_34_IndvDetail _indvdetail = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail34"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail34"] = new CcModAppProject_34_IndvDetail { Project34IndvId = 0, ProjectId = 0 };

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

            var _ncdetail = _db.CcModNavigationClassDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModNavigationClass)
                                    .Select(x => new NavigationClassDetailTemp
                                    {
                                        NavigationClassId = x.NavigationClassId,
                                        NavigationClassName = x.LookUpCcModNavigationClass.NavigationClassName,
                                        NavigationClassNameBn = x.LookUpCcModNavigationClass.NavigationClassNameBn
                                    }).ToList();
            ViewData["NavigationClassDetail"] = _ncdetail;

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverType)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _rivNature = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverNature)
                                    .Select(x => new RiverNatureDetailTemp
                                    {
                                        RiverNatureId = x.RiverNatureId.Value,
                                        RiverNatureTitle = x.LookUpCcModRiverNature.RiverNatureTitle,
                                        RiverNatureTitleBn = x.LookUpCcModRiverNature.RiverNatureTitleBn
                                    }).ToList();
            ViewData["RiverNatureID"] = _rivNature;

            var _sdidetail = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project34IndvId"] = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project34IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Project for Construction of Hydraulic Infrastructure | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.5: Surface Water Reservation Project
        public IActionResult view35(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_35_IndvDetail _indvdetail = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail35"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail35"] = new CcModAppProject_35_IndvDetail { Project35IndvId = 0, ProjectId = 0 };

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

            var _stcdetail = _db.CcModStructTypeConservDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModStructTypeConserv)
                                    .Select(x => new StructTypeConDetailTemp
                                    {
                                        TypeOfStructureId = x.TypeOfStructureId,
                                        StructureName = x.LookUpCcModStructTypeConserv.StructureName,
                                        StructureNameBn = x.LookUpCcModStructTypeConserv.StructureNameBn
                                    }).ToList();
            ViewData["StructTypeConservDetail"] = _stcdetail;

            var _cldetail = _db.CcModConservLocationDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModConservLocation)
                                    .Select(x => new ConservLocationDetailTemp
                                    {
                                        ConservLocationId = x.ConservLocationId,
                                        ConservLocation = x.LookUpCcModConservLocation.ConservLocation,
                                        ConservLocationBn = x.LookUpCcModConservLocation.ConservLocationBn
                                    }).ToList();
            ViewData["ConservLocationDetail"] = _cldetail;

            var _towbdetail = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModTypeOfWaterBody)
                                    .Select(x => new TypeOfWaterBodyDetailTemp
                                    {
                                        WaterBodyTypeId = x.WaterBodyTypeId.Value,
                                        WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
                                        WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
                                    }).ToList();
            ViewData["TypeOfWaterBodyDetail"] = _towbdetail;

            var _sdidetail = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _icadetails = (from d in _db.CcModPrjIrrigCropAreaDetail
                               where d.ProjectId == pcd.ProjectId
                               select new IrrigCropAreaDetailTemp
                               {
                                   IrrigatedCropId = d.IrrigatedCropId,
                                   ProjectId = d.ProjectId,
                                   CropName = d.CropName,
                                   Area = d.Area.ToString()
                               }).ToList();
            ViewData["IrrigCropAreaDetailTemp"] = _icadetails;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project35IndvId"] = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project35IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Project for Construction of Hydraulic Infrastructure | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.6: Wetland Development Project
        public IActionResult view36(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_36_IndvDetail _indvdetail = _db.CcModAppProject_36_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail36"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail36"] = new CcModAppProject_36_IndvDetail { Project36IndvId = 0, ProjectId = 0 };

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

            var _drainagecondition = _db.CcModAppProject_36_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModDrainageCondition)
                                    .Select(x => new DrainageConditionlTemp
                                    {
                                        DrainageConditionId = x.DrainageConditionId.Value,
                                        DrainageCondition = x.LookUpCcModDrainageCondition.DrainageCondition,
                                        DrainageConditionBn = x.LookUpCcModDrainageCondition.DrainageConditionBn
                                    }).ToList();
            ViewData["DrainageConditionDetail"] = _drainagecondition;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project36IndvId"] = _db.CcModAppProject_36_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project36IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Wetland Development Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.7: Project for Surface Water Use in Industrial Sector
        public IActionResult view37(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_37_IndvDetail _indvdetail = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail37"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail37"] = new CcModAppProject_37_IndvDetail { Project37IndvId = 0, ProjectId = 0 };

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

            //rony
            //Name of the Water Bodies to be Used
            var _water_bodies_to_be_used = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                            .Select(x => new WaterBodyDetailTemp
                                            {
                                                WaterBodyId = x.WaterBodyId.Value,
                                                NameOfWaterBody = x.LookUpCcModWaterBody.NameOfWaterBody,
                                                NameOfWaterBodyBn = x.LookUpCcModWaterBody.NameOfWaterBodyBn
                                            }).ToList();
            ViewData["WaterBodyToBeUsed"] = _water_bodies_to_be_used;

            var river_type_detail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = river_type_detail;

            var _type_of_water_body = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                        .Select(x => new TypeOfWaterBodyDetailTemp
                                        {
                                            WaterBodyTypeId = x.WaterBodyTypeId.Value,
                                            WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
                                            WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
                                        }).ToList();
            ViewData["TypeOfWaterBody"] = _type_of_water_body;

            var gwa_detail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new GroundWaterQualityDetailTemp
                            {
                                GroundWaterQualityId = x.GroundWaterQualityId,
                                ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                            }).ToList();
            ViewData["GroundWaterQuality"] = gwa_detail;

            var towu_detail = _db.CcModTypeOfWaterUseDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                .Select(x => new TypeOfWaterUseDetailTemp
                                {
                                    TypeOfWaterUseId = x.TypeOfWaterUseId,
                                    WaterUseName = x.LookUpCcModTypeOfWaterUse.WaterUseName,
                                    WaterUseNameBn = x.LookUpCcModTypeOfWaterUse.WaterUseNameBn
                                }).ToList();
            ViewData["TypeOfWaterUse"] = towu_detail;

            var _water_use_sector = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new WaterUseSectorDetailTemp
                                    {
                                        WaterUseSectorId = x.WaterUseSectorId.Value,
                                        WaterUseSectorName = x.LookUpCcModWaterUseSector.WaterUseSectorName,
                                        WaterUseSectorNameBn = x.LookUpCcModWaterUseSector.WaterUseSectorNameBn
                                    }).ToList();
            ViewData["WaterUseSector"] = _water_use_sector;

            var wds_detail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                .Select(x => new WtrDiversionSourceDetailTemp
                                {
                                    WaterDiversionSourceId = x.WaterDiversionSourceId,
                                    SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                    SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                }).ToList();
            ViewData["WaterDiversSource"] = wds_detail;

            var _swadetail = GetSurfaceWaterAvailabilityDetail(pcd.ProjectId);
            ViewData["SurfaceWaterAvailDetail"] = _swadetail;

            var _groundwaterdepthdetail = GetGroundWaterDepthDetail(pcd.ProjectId);
            ViewData["GroundWaterDepthDetail"] = _groundwaterdepthdetail;

            var _waterusedetail = GetWaterUseDetail(pcd.ProjectId);
            ViewData["WaterUseDetail"] = _waterusedetail;

            var _gwwdetail = GetGroundWaterWithdrawalDetail(pcd.ProjectId);
            ViewData["GroundWaterWithdrawalDetail"] = _gwwdetail;
            //rony

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project37IndvId"] = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project37IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Surface Water Use in Industrial Sector | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.8: River Bank Protection Project
        public IActionResult view38(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_38_IndvDetail _indvdetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail38"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail38"] = new CcModAppProject_38_IndvDetail { Project38IndvId = 0, ProjectId = 0 };

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

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverType)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _rivNature = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverNature)
                                    .Select(x => new RiverNatureDetailTemp
                                    {
                                        RiverNatureId = x.RiverNatureId.Value,
                                        RiverNatureTitle = x.LookUpCcModRiverNature.RiverNatureTitle,
                                        RiverNatureTitleBn = x.LookUpCcModRiverNature.RiverNatureTitleBn
                                    }).ToList();
            ViewData["RiverNatureID"] = _rivNature;

            var _sdidetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _blsdetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModBankLineShifting)
                                    .Select(x => new BankLineShiftingDetailTemp
                                    {
                                        BankLineShiftingId = x.BankLineShiftingId.Value,
                                        BankLineTitle = x.LookUpCcModBankLineShifting.BankLineTitle,
                                        BankLineTitleBn = x.LookUpCcModBankLineShifting.BankLineTitleBn
                                    }).ToList();
            ViewData["BankLineShiftingDetail"] = _blsdetail;

            var _bsdetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModBankStability)
                                    .Select(x => new BankStabilityDetailTemp
                                    {
                                        BankStabilityTypeId = x.BankStabilityTypeId.Value,
                                        BankStabilityType = x.LookUpCcModBankStability.BankStabilityType,
                                        BankStabilityTypeBn = x.LookUpCcModBankStability.BankStabilityTypeBn
                                    }).ToList();
            ViewData["BankStabilityDetail"] = _bsdetail;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project38IndvId"] = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project38IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "River Bank Protection Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.9: River Excavation or Dredging Project
        public IActionResult view39(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_39_IndvDetail _indvdetail = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail39"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail39"] = new CcModAppProject_39_IndvDetail { Project39IndvId = 0, ProjectId = 0 };

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

            var _ncdetail = _db.CcModNavigationClassDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModNavigationClass)
                                    .Select(x => new NavigationClassDetailTemp
                                    {
                                        NavigationClassId = x.NavigationClassId,
                                        NavigationClassName = x.LookUpCcModNavigationClass.NavigationClassName,
                                        NavigationClassNameBn = x.LookUpCcModNavigationClass.NavigationClassNameBn
                                    }).ToList();
            ViewData["NavigationClassDetail"] = _ncdetail;

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverType)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _rivNature = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverNature)
                                    .Select(x => new RiverNatureDetailTemp
                                    {
                                        RiverNatureId = x.RiverNatureId.Value,
                                        RiverNatureTitle = x.LookUpCcModRiverNature.RiverNatureTitle,
                                        RiverNatureTitleBn = x.LookUpCcModRiverNature.RiverNatureTitleBn
                                    }).ToList();
            ViewData["RiverNatureID"] = _rivNature;

            var _drainagecondition = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModDrainageCondition)
                                        .Select(x => new DrainageConditionlTemp
                                        {
                                            DrainageConditionId = x.DrainageConditionId.Value,
                                            DrainageCondition = x.LookUpCcModDrainageCondition.DrainageCondition,
                                            DrainageConditionBn = x.LookUpCcModDrainageCondition.DrainageConditionBn
                                        }).ToList();
            ViewData["DrainageConditionDetail"] = _drainagecondition;

            var _bsdetail = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModBankStability)
                                .Select(x => new BankStabilityDetailTemp
                                {
                                    BankStabilityTypeId = x.BankStabilityTypeId.Value,
                                    BankStabilityType = x.LookUpCcModBankStability.BankStabilityType,
                                    BankStabilityTypeBn = x.LookUpCcModBankStability.BankStabilityTypeBn
                                }).ToList();
            ViewData["BankStabilityDetail"] = _bsdetail;

            var _sdidetail = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            //var _analyzeoptionsdetail = GetAnalyzeOptionsDetail(pcd.ProjectId);
            //ViewData["AnalyzeOptionsDetail"] = _analyzeoptionsdetail;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project39IndvId"] = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project39IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "River Excavation or Dredging Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.10 Excavation - Excavation of Khal Project
        public IActionResult view310(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_310_IndvDetail _indvdetail = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail310"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail310"] = new CcModAppProject_310_IndvDetail { Project310IndvId = 0, ProjectId = 0 };

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


            var _rivNature = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverNature)
                                    .Select(x => new RiverNatureDetailTemp
                                    {
                                        RiverNatureId = x.RiverNatureId.Value,
                                        RiverNatureTitle = x.LookUpCcModRiverNature.RiverNatureTitle,
                                        RiverNatureTitleBn = x.LookUpCcModRiverNature.RiverNatureTitleBn
                                    }).ToList();
            ViewData["RiverNatureID"] = _rivNature;

            var _drainagecondition = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModDrainageCondition)
                                        .Select(x => new DrainageConditionlTemp
                                        {
                                            DrainageConditionId = x.DrainageConditionId.Value,
                                            DrainageCondition = x.LookUpCcModDrainageCondition.DrainageCondition,
                                            DrainageConditionBn = x.LookUpCcModDrainageCondition.DrainageConditionBn
                                        }).ToList();
            ViewData["DrainageConditionDetail"] = _drainagecondition;

            var _sdidetail = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _swadetail = (from d in _db.CcModPrjSWADetail
                              where d.ProjectId == pcd.ProjectId
                              select new SWATemp
                              {
                                  SWAId = d.SWAId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  MinWaterFlow = d.MinWaterFlow,
                                  WaterDemandMonth = d.WaterDemandMonth
                              }).OrderBy(o => o.SWAId).ToList(); ;
            ViewData["SurfaceWaterAvailDetail"] = _swadetail;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project310IndvId"] = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project310IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Excavation - Excavation of Khal Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.11 Fisheries Project in Surface Water
        public IActionResult view311(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_311_IndvDetail _indvdetail = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail311"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail311"] = new CcModAppProject_311_IndvDetail { Project311IndvId = 0, ProjectId = 0 };

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


            var _fishwaterarea = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModFishWaterArea)
                                        .Select(x => new FishWaterAreaTemp
                                        {
                                            FishWaterAreaId = x.FishWaterAreaId.Value,
                                            AreaName = x.LookUpCcModFishWaterArea.AreaName,
                                            AreaNameBn = x.LookUpCcModFishWaterArea.AreaNameBn
                                        }).ToList();
            ViewData["FishWaterAreaDetail"] = _fishwaterarea;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project311IndvId"] = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project311IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Fisheries Project in Surface Water | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.12 Project for Ground Water Withdrawal, Distribution or Use
        public IActionResult view312(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_312_IndvDetail _indvdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail312"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail312"] = new CcModAppProject_312_IndvDetail { Project312IndvId = 0, ProjectId = 0 };

            var _hydroregiondetail = GetHydrologicalRegion(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["HydroRegionDetail"] = _hydroregiondetail;

            var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new BDP2100HotSpotDetailTemp
                                    {
                                        DeltaPlanHotSpotId = x.DeltaPlanHotSpotId,
                                        PlanName = x.LookUpCcModDeltPlan2100HotSpot.PlanName,
                                        PlanNameBn = x.LookUpCcModDeltPlan2100HotSpot.PlanNameBn
                                    }).ToList();
            ViewData["BDP2100HotSpotDetail"] = _hotspotdetail;

            var _powudetail = _db.CcModPurposeOfWaterUseDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new PurposeOfWaterUseDetailTemp
                                    {
                                        PurposeOfWaterUseId = x.PurposeOfWaterUseId,
                                        WaterUsePurpose = x.LookUpCcModPurposeOfWaterUse.WaterUsePurpose,
                                        WaterUsePurposeBn = x.LookUpCcModPurposeOfWaterUse.WaterUsePurposeBn
                                    }).ToList();
            ViewData["PurposeOfWaterUseDetail"] = _powudetail;

            var _topwdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new TypeProposedWellDetailTemp
                                    {
                                        TypeProposedWellId = x.TypeProposedWellId.Value,
                                        ProposedWellName = x.LookUpCcModTypeProposedWell.ProposedWellName,
                                        ProposedWellNameBn = x.LookUpCcModTypeProposedWell.ProposedWellNameBn
                                    }).ToList();
            ViewData["TypeProposedWellDetail"] = _topwdetail;

            var _gwddetail = (from d in _db.CcModPrjGrndWtrDepthDetail
                              where d.ProjectId == pcd.ProjectId
                              select new GrndWtrDepthDetailTemp
                              {
                                  GrndWtrDepthDetailId = d.GrndWtrDepthDetailId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  WaterDepth = d.WaterDepth
                              }).OrderBy(o => o.GrndWtrDepthDetailId).ToList(); ;
            ViewData["GrndWtrDepthDetail"] = _gwddetail;

            var _coadetail = _db.CcModCategoryOfAquiferDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new CategoryOfAquiferDetailTemp
                                    {
                                        CategoryOfAquiferId = x.CategoryOfAquiferId,
                                        AquiferName = x.LookUpCcModCategoryOfAquifer.AquiferName,
                                        AquiferNameBn = x.LookUpCcModCategoryOfAquifer.AquiferNameBn
                                    }).ToList();
            ViewData["CategoryOfAquiferDetail"] = _coadetail;

            var gwa_detail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new GroundWaterQualityDetailTemp
                            {
                                GroundWaterQualityId = x.GroundWaterQualityId,
                                ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                            }).ToList();
            ViewData["GroundWaterQuality"] = gwa_detail;

            var _pgwrdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new PotentialGroundWaterRechargeDetailTemp
                                    {
                                        PotGrndWtrRechargeId = x.PotGrndWtrRechargeId.Value,
                                        IndicatorName = x.LookUpCcModPotGrndWtrRecharge.IndicatorName,
                                        IndicatorNameBn = x.LookUpCcModPotGrndWtrRecharge.IndicatorNameBn
                                    }).ToList();
            ViewData["PotentialGroundWaterRechargeDetail"] = _pgwrdetail;

            var _soiltypedetail = _db.CcModSoilTypeDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new SoilTypeDetailTemp
                                    {
                                        SoilTypeId = x.SoilTypeId,
                                        SoilTypeName = x.LookUpCcModSoilType.SoilTypeName,
                                        SoilTypeNameBn = x.LookUpCcModSoilType.SoilTypeNameBn
                                    }).ToList();
            ViewData["SoilTypeDetail"] = _soiltypedetail;

            var _wucdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new WaterUseConsumptionDetailTemp
                                    {
                                        WaterUseConsumptionId = x.WaterUseConsumptionId.Value,
                                        ConsumptionName = x.LookUpCcModWaterUseConsumption.ConsumptionName,
                                        ConsumptionNameBn = x.LookUpCcModWaterUseConsumption.ConsumptionNameBn
                                    }).ToList();
            ViewData["WaterUseConsumptionDetail"] = _wucdetail;

            var _icadetails = (from d in _db.CcModPrjIrrigCropAreaDetail
                               where d.ProjectId == pcd.ProjectId
                               select new IrrigCropAreaDetailTemp
                               {
                                   IrrigatedCropId = d.IrrigatedCropId,
                                   ProjectId = d.ProjectId,
                                   CropName = d.CropName,
                                   Area = d.Area.ToString()
                               }).ToList();
            ViewData["IrrigCropAreaDetailTemp"] = _icadetails;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project312IndvId"] = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project312IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Project for Ground Water Withdrawal, Distribution or Use | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return View();
        }

        //Form 3.13 Special Project by DG
        public IActionResult view313(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_313_IndvDetail _indvdetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail313"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail313"] = new CcModAppProject_313_IndvDetail { Project313IndvId = 0, ProjectId = 0 };

            var _hydroregiondetail = GetHydrologicalRegion(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["HydroRegionDetail"] = _hydroregiondetail;

            var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new BDP2100HotSpotDetailTemp
                                    {
                                        DeltaPlanHotSpotId = x.DeltaPlanHotSpotId,
                                        PlanName = x.LookUpCcModDeltPlan2100HotSpot.PlanName,
                                        PlanNameBn = x.LookUpCcModDeltPlan2100HotSpot.PlanNameBn
                                    }).ToList();
            ViewData["BDP2100HotSpotDetail"] = _hotspotdetail;

            var _hydrosystemdetail = GetHydroSystemDetail(pcd.ProjectId);
            ViewData["HydroSystemDetail"] = _hydrosystemdetail;

            var _bsdetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new BankStabilityDetailTemp
                            {
                                BankStabilityTypeId = x.BankStabilityTypeId.Value,
                                BankStabilityType = x.LookUpCcModBankStability.BankStabilityType,
                                BankStabilityTypeBn = x.LookUpCcModBankStability.BankStabilityTypeBn
                            }).ToList();
            ViewData["BankStabilityDetail"] = _bsdetail;

            var _sdidetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new SediOfRiverOrKhalDetailTemp
                            {
                                SedimentationId = x.SedimentationId.Value,
                                SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                            }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _khaltypedetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new KhalTypeDetailTemp
                            {
                                KhalTypeId = x.KhalTypeId.Value,
                                KhalTypeName = x.LookUpCcModKhalType.KhalTypeName,
                                KhalTypeNameBn = x.LookUpCcModKhalType.KhalTypeNameBn
                            }).ToList();
            ViewData["KhalTypeDetail"] = _khaltypedetail;

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _typesofflood = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == pcd.ProjectId)
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

            var _drainagecondition = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new DrainageConditionlTemp
                                    {
                                        DrainageConditionId = x.DrainageConditionId.Value,
                                        DrainageCondition = x.LookUpCcModDrainageCondition.DrainageCondition,
                                        DrainageConditionBn = x.LookUpCcModDrainageCondition.DrainageConditionBn
                                    }).ToList();
            ViewData["DrainageConditionDetail"] = _drainagecondition;

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

            //var _formcommentslisttemp = GetFormDataAnalysisComments(pcd.ProjectTypeId, pcd.ProjectId);
            //ViewData["FormCommentsListTemp"] = _formcommentslisttemp;

            ViewData["ProjectId"] = pcd.ProjectId;
            ViewData["Project313IndvId"] = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project313IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Special Project by DG | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

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
            //ChangeStatus(id, status);

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

        public IActionResult printform32(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();
            //ChangeStatus(id);

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

            CcModAppProject_32_IndvDetail _indvdetail = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail32"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail32"] = new CcModAppProject_32_IndvDetail();

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

            var _pwudetail = _db.CcModProposedWaterUseDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModProposedWaterUse)
                                    .Select(x => new ProposedWaterUseDetailTemp
                                    {
                                        ProposedWaterUseId = x.ProposedWaterUseId,
                                        WaterUseTypeName = x.LookUpCcModProposedWaterUse.WaterUseTypeName,
                                        WaterUseTypeNameBn = x.LookUpCcModProposedWaterUse.WaterUseTypeNameBn
                                    }).ToList();
            ViewData["ProposedWaterUseDetail"] = _pwudetail;

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverType)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _towbdetail = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModTypeOfWaterBody)
                                    .Select(x => new TypeOfWaterBodyDetailTemp
                                    {
                                        WaterBodyTypeId = x.WaterBodyTypeId.Value,
                                        WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
                                        WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
                                    }).ToList();
            ViewData["TypeOfWaterBodyDetail"] = _towbdetail;

            var _hydrosystemdetail = GetHydroSystemDetail(pcd.ProjectId);
            ViewData["HydroSystemDetail"] = _hydrosystemdetail;

            var _sdidetail = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _swadetail = (from d in _db.CcModPrjSWADetail
                              where d.ProjectId == pcd.ProjectId
                              select new SWATemp
                              {
                                  SWAId = d.SWAId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  MinWaterFlow = d.MinWaterFlow,
                                  WaterDemandMonth = d.WaterDemandMonth
                              }).OrderBy(o => o.SWAId).ToList(); ;
            ViewData["SurfaceWaterAvailDetail"] = _swadetail;

            var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModWtrDiversionSource)
                                    .Select(x => new WtrDiversionSourceDetailTemp
                                    {
                                        WaterDiversionSourceId = x.WaterDiversionSourceId,
                                        SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                        SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                    }).ToList();
            ViewData["WtrDiversionSourceDetail"] = _wdsdetail;

            var _gwddetail = (from d in _db.CcModPrjGrndWtrDepthDetail
                              where d.ProjectId == pcd.ProjectId
                              select new GrndWtrDepthDetailTemp
                              {
                                  GrndWtrDepthDetailId = d.GrndWtrDepthDetailId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  WaterDepth = d.WaterDepth
                              }).OrderBy(o => o.GrndWtrDepthDetailId).ToList(); ;
            ViewData["GrndWtrDepthDetail"] = _gwddetail;

            var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModGroundWaterQuality)
                                    .Select(x => new LookUpCcModGroundWaterQuality
                                    {
                                        GroundWaterQualityId = x.GroundWaterQualityId,
                                        ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                        ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                                    }).ToList();
            ViewData["GroundWaterQltDetail"] = _gwqdetail;

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
            ViewData["Project32IndvId"] = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project32IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Flood Control Management Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            //return View();
            return new ViewAsPdf("~/Views/form/printform32.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform33(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_33_IndvDetail _indvdetail = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail33"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail33"] = new CcModAppProject_33_IndvDetail { Project33IndvId = 0, ProjectId = 0 };

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

            var _towbdetail = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModTypeOfWaterBody)
                                    .Select(x => new TypeOfWaterBodyDetailTemp
                                    {
                                        WaterBodyTypeId = x.WaterBodyTypeId.Value,
                                        WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
                                        WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
                                    }).ToList();
            ViewData["TypeOfWaterBodyDetail"] = _towbdetail;

            var _hydrosystemdetail = GetHydroSystemDetail(pcd.ProjectId);
            ViewData["HydroSystemDetail"] = _hydrosystemdetail;

            var _sdidetail = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _icadetails = (from d in _db.CcModPrjIrrigCropAreaDetail
                               where d.ProjectId == pcd.ProjectId
                               select new IrrigCropAreaDetailTemp
                               {
                                   IrrigatedCropId = d.IrrigatedCropId,
                                   ProjectId = d.ProjectId,
                                   CropName = d.CropName,
                                   Area = d.Area.ToString()
                               }).ToList();
            ViewData["IrrigCropAreaDetailTemp"] = _icadetails;

            var _swadetail = (from d in _db.CcModPrjSWADetail
                              where d.ProjectId == pcd.ProjectId
                              select new SWATemp
                              {
                                  SWAId = d.SWAId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  MinWaterFlow = d.MinWaterFlow,
                                  WaterDemandMonth = d.WaterDemandMonth
                              }).OrderBy(o => o.SWAId).ToList(); ;
            ViewData["SurfaceWaterAvailDetail"] = _swadetail;

            var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModWtrDiversionSource)
                                    .Select(x => new WtrDiversionSourceDetailTemp
                                    {
                                        WaterDiversionSourceId = x.WaterDiversionSourceId,
                                        SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                        SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                    }).ToList();
            ViewData["WtrDiversionSourceDetail"] = _wdsdetail;

            var _gwddetail = (from d in _db.CcModPrjGrndWtrDepthDetail
                              where d.ProjectId == pcd.ProjectId
                              select new GrndWtrDepthDetailTemp
                              {
                                  GrndWtrDepthDetailId = d.GrndWtrDepthDetailId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  WaterDepth = d.WaterDepth
                              }).OrderBy(o => o.GrndWtrDepthDetailId).ToList(); ;
            ViewData["GrndWtrDepthDetail"] = _gwddetail;

            var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModGroundWaterQuality)
                                    .Select(x => new LookUpCcModGroundWaterQuality
                                    {
                                        GroundWaterQualityId = x.GroundWaterQualityId,
                                        ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                        ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                                    }).ToList();
            ViewData["GroundWaterQltDetail"] = _gwqdetail;

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
            ViewData["Project33IndvId"] = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project33IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Irrigation Project by Surface Water | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform33.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform34(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_34_IndvDetail _indvdetail = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail34"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail34"] = new CcModAppProject_34_IndvDetail { Project34IndvId = 0, ProjectId = 0 };

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

            var _ncdetail = _db.CcModNavigationClassDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModNavigationClass)
                                    .Select(x => new NavigationClassDetailTemp
                                    {
                                        NavigationClassId = x.NavigationClassId,
                                        NavigationClassName = x.LookUpCcModNavigationClass.NavigationClassName,
                                        NavigationClassNameBn = x.LookUpCcModNavigationClass.NavigationClassNameBn
                                    }).ToList();
            ViewData["NavigationClassDetail"] = _ncdetail;

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverType)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _rivNature = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverNature)
                                    .Select(x => new RiverNatureDetailTemp
                                    {
                                        RiverNatureId = x.RiverNatureId.Value,
                                        RiverNatureTitle = x.LookUpCcModRiverNature.RiverNatureTitle,
                                        RiverNatureTitleBn = x.LookUpCcModRiverNature.RiverNatureTitleBn
                                    }).ToList();
            ViewData["RiverNatureID"] = _rivNature;

            //var _towbdetail = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModTypeOfWaterBody)
            //                        .Select(x => new TypeOfWaterBodyDetailTemp
            //                        {
            //                            WaterBodyTypeId = x.WaterBodyTypeId.Value,
            //                            WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
            //                            WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
            //                        }).ToList();
            //ViewData["TypeOfWaterBodyDetail"] = _towbdetail;

            //var _hydrosystemdetail = GetHydroSystemDetail(pcd.ProjectId);
            //ViewData["HydroSystemDetail"] = _hydrosystemdetail;

            var _sdidetail = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            //var _icadetails = (from d in _db.CcModPrjIrrigCropAreaDetail
            //                   where d.ProjectId == pcd.ProjectId
            //                   select new IrrigCropAreaDetailTemp
            //                   {
            //                       IrrigatedCropId = d.IrrigatedCropId,
            //                       ProjectId = d.ProjectId,
            //                       CropName = d.CropName,
            //                       Area = d.Area.ToString()
            //                   }).ToList();
            //ViewData["IrrigCropAreaDetailTemp"] = _icadetails;

            //var _swadetail = (from d in _db.CcModPrjSWADetail
            //                  where d.ProjectId == pcd.ProjectId
            //                  select new SWATemp
            //                  {
            //                      SWAId = d.SWAId,
            //                      ProjectId = d.ProjectId,
            //                      MonthId = d.MonthId,
            //                      Month = mfi.GetMonthName(d.MonthId).ToString(),
            //                      MinWaterFlow = d.MinWaterFlow,
            //                      WaterDemandMonth = d.WaterDemandMonth
            //                  }).OrderBy(o => o.SWAId).ToList(); ;
            //ViewData["SurfaceWaterAvailDetail"] = _swadetail;

            //var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModWtrDiversionSource)
            //                        .Select(x => new WtrDiversionSourceDetailTemp
            //                        {
            //                            WaterDiversionSourceId = x.WaterDiversionSourceId,
            //                            SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
            //                            SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
            //                        }).ToList();
            //ViewData["WtrDiversionSourceDetail"] = _wdsdetail;

            //var _gwddetail = (from d in _db.CcModPrjGrndWtrDepthDetail
            //                  where d.ProjectId == pcd.ProjectId
            //                  select new GrndWtrDepthDetailTemp
            //                  {
            //                      GrndWtrDepthDetailId = d.GrndWtrDepthDetailId,
            //                      ProjectId = d.ProjectId,
            //                      MonthId = d.MonthId,
            //                      Month = mfi.GetMonthName(d.MonthId).ToString(),
            //                      WaterDepth = d.WaterDepth
            //                  }).OrderBy(o => o.GrndWtrDepthDetailId).ToList(); ;
            //ViewData["GrndWtrDepthDetail"] = _gwddetail;

            //var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModGroundWaterQuality)
            //                        .Select(x => new LookUpCcModGroundWaterQuality
            //                        {
            //                            GroundWaterQualityId = x.GroundWaterQualityId,
            //                            ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
            //                            ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
            //                        }).ToList();
            //ViewData["GroundWaterQltDetail"] = _gwqdetail;

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
            ViewData["Project34IndvId"] = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project34IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Project for Construction of Hydraulic Infrastructure | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform34.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform35(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_35_IndvDetail _indvdetail = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail35"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail35"] = new CcModAppProject_35_IndvDetail { Project35IndvId = 0, ProjectId = 0 };

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

            var _stcdetail = _db.CcModStructTypeConservDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModStructTypeConserv)
                                    .Select(x => new StructTypeConDetailTemp
                                    {
                                        TypeOfStructureId = x.TypeOfStructureId,
                                        StructureName = x.LookUpCcModStructTypeConserv.StructureName,
                                        StructureNameBn = x.LookUpCcModStructTypeConserv.StructureNameBn
                                    }).ToList();
            ViewData["StructTypeConservDetail"] = _stcdetail;

            var _cldetail = _db.CcModConservLocationDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModConservLocation)
                                    .Select(x => new ConservLocationDetailTemp
                                    {
                                        ConservLocationId = x.ConservLocationId,
                                        ConservLocation = x.LookUpCcModConservLocation.ConservLocation,
                                        ConservLocationBn = x.LookUpCcModConservLocation.ConservLocationBn
                                    }).ToList();
            ViewData["ConservLocationDetail"] = _cldetail;

            var _towbdetail = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModTypeOfWaterBody)
                                    .Select(x => new TypeOfWaterBodyDetailTemp
                                    {
                                        WaterBodyTypeId = x.WaterBodyTypeId.Value,
                                        WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
                                        WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
                                    }).ToList();
            ViewData["TypeOfWaterBodyDetail"] = _towbdetail;

            var _sdidetail = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _icadetails = (from d in _db.CcModPrjIrrigCropAreaDetail
                               where d.ProjectId == pcd.ProjectId
                               select new IrrigCropAreaDetailTemp
                               {
                                   IrrigatedCropId = d.IrrigatedCropId,
                                   ProjectId = d.ProjectId,
                                   CropName = d.CropName,
                                   Area = d.Area.ToString()
                               }).ToList();
            ViewData["IrrigCropAreaDetailTemp"] = _icadetails;

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
            ViewData["Project35IndvId"] = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project35IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Surface Water Reservation Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform35.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform36(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_36_IndvDetail _indvdetail = _db.CcModAppProject_36_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail36"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail36"] = new CcModAppProject_36_IndvDetail { Project36IndvId = 0, ProjectId = 0 };

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

            var _drainagecondition = _db.CcModAppProject_36_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModDrainageCondition)
                                    .Select(x => new DrainageConditionlTemp
                                    {
                                        DrainageConditionId = x.DrainageConditionId.Value,
                                        DrainageCondition = x.LookUpCcModDrainageCondition.DrainageCondition,
                                        DrainageConditionBn = x.LookUpCcModDrainageCondition.DrainageConditionBn
                                    }).ToList();
            ViewData["DrainageConditionDetail"] = _drainagecondition;

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
            ViewData["Project36IndvId"] = _db.CcModAppProject_36_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project36IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Wetland Development Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform36.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform37(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_37_IndvDetail _indvdetail = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail37"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail37"] = new CcModAppProject_37_IndvDetail { Project37IndvId = 0, ProjectId = 0 };

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

            //rony
            var _water_bodies_to_be_used = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                            .Select(x => new WaterBodyDetailTemp
                                            {
                                                WaterBodyId = x.WaterBodyId.Value,
                                                NameOfWaterBody = x.LookUpCcModWaterBody.NameOfWaterBody,
                                                NameOfWaterBodyBn = x.LookUpCcModWaterBody.NameOfWaterBodyBn
                                            }).ToList();
            ViewData["WaterBodyToBeUsed"] = _water_bodies_to_be_used;

            var river_type_detail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = river_type_detail;

            var _type_of_water_body = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                        .Select(x => new TypeOfWaterBodyDetailTemp
                                        {
                                            WaterBodyTypeId = x.WaterBodyTypeId.Value,
                                            WaterBodyType = x.LookUpCcModTypeOfWaterBody.WaterBodyType,
                                            WaterBodyTypeBn = x.LookUpCcModTypeOfWaterBody.WaterBodyTypeBn
                                        }).ToList();
            ViewData["TypeOfWaterBody"] = _type_of_water_body;

            var gwa_detail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new GroundWaterQualityDetailTemp
                            {
                                GroundWaterQualityId = x.GroundWaterQualityId,
                                ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                            }).ToList();
            ViewData["GroundWaterQuality"] = gwa_detail;

            var towu_detail = _db.CcModTypeOfWaterUseDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                .Select(x => new TypeOfWaterUseDetailTemp
                                {
                                    TypeOfWaterUseId = x.TypeOfWaterUseId,
                                    WaterUseName = x.LookUpCcModTypeOfWaterUse.WaterUseName,
                                    WaterUseNameBn = x.LookUpCcModTypeOfWaterUse.WaterUseNameBn
                                }).ToList();
            ViewData["TypeOfWaterUse"] = towu_detail;

            var _water_use_sector = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new WaterUseSectorDetailTemp
                                    {
                                        WaterUseSectorId = x.WaterUseSectorId.Value,
                                        WaterUseSectorName = x.LookUpCcModWaterUseSector.WaterUseSectorName,
                                        WaterUseSectorNameBn = x.LookUpCcModWaterUseSector.WaterUseSectorNameBn
                                    }).ToList();
            ViewData["WaterUseSector"] = _water_use_sector;

            var wds_detail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                .Select(x => new WtrDiversionSourceDetailTemp
                                {
                                    WaterDiversionSourceId = x.WaterDiversionSourceId,
                                    SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                    SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                }).ToList();
            ViewData["WaterDiversSource"] = wds_detail;

            var _swadetail = GetSurfaceWaterAvailabilityDetail(pcd.ProjectId);
            ViewData["SurfaceWaterAvailDetail"] = _swadetail;

            var _groundwaterdepthdetail = GetGroundWaterDepthDetail(pcd.ProjectId);
            ViewData["GroundWaterDepthDetail"] = _groundwaterdepthdetail;

            var _waterusedetail = GetWaterUseDetail(pcd.ProjectId);
            ViewData["WaterUseDetail"] = _waterusedetail;

            var _gwwdetail = GetGroundWaterWithdrawalDetail(pcd.ProjectId);
            ViewData["GroundWaterWithdrawalDetail"] = _gwwdetail;
            //rony

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
            ViewData["Project37IndvId"] = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project37IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Project for Surface Water Use in Industrial Sector | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform37.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform38(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_38_IndvDetail _indvdetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail38"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail38"] = new CcModAppProject_38_IndvDetail { Project38IndvId = 0, ProjectId = 0 };

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

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverType)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _rivNature = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverNature)
                                    .Select(x => new RiverNatureDetailTemp
                                    {
                                        RiverNatureId = x.RiverNatureId.Value,
                                        RiverNatureTitle = x.LookUpCcModRiverNature.RiverNatureTitle,
                                        RiverNatureTitleBn = x.LookUpCcModRiverNature.RiverNatureTitleBn
                                    }).ToList();
            ViewData["RiverNatureID"] = _rivNature;

            var _sdidetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _blsdetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModBankLineShifting)
                                    .Select(x => new BankLineShiftingDetailTemp
                                    {
                                        BankLineShiftingId = x.BankLineShiftingId.Value,
                                        BankLineTitle = x.LookUpCcModBankLineShifting.BankLineTitle,
                                        BankLineTitleBn = x.LookUpCcModBankLineShifting.BankLineTitleBn
                                    }).ToList();
            ViewData["BankLineShiftingDetail"] = _blsdetail;

            var _bsdetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModBankStability)
                                    .Select(x => new BankStabilityDetailTemp
                                    {
                                        BankStabilityTypeId = x.BankStabilityTypeId.Value,
                                        BankStabilityType = x.LookUpCcModBankStability.BankStabilityType,
                                        BankStabilityTypeBn = x.LookUpCcModBankStability.BankStabilityTypeBn
                                    }).ToList();
            ViewData["BankStabilityDetail"] = _bsdetail;

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
            ViewData["Project38IndvId"] = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project38IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "River Bank Protection Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform38.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform39(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_39_IndvDetail _indvdetail = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail39"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail39"] = new CcModAppProject_39_IndvDetail { Project39IndvId = 0, ProjectId = 0 };

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

            var _ncdetail = _db.CcModNavigationClassDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModNavigationClass)
                                    .Select(x => new NavigationClassDetailTemp
                                    {
                                        NavigationClassId = x.NavigationClassId,
                                        NavigationClassName = x.LookUpCcModNavigationClass.NavigationClassName,
                                        NavigationClassNameBn = x.LookUpCcModNavigationClass.NavigationClassNameBn
                                    }).ToList();
            ViewData["NavigationClassDetail"] = _ncdetail;

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverType)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _rivNature = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverNature)
                                    .Select(x => new RiverNatureDetailTemp
                                    {
                                        RiverNatureId = x.RiverNatureId.Value,
                                        RiverNatureTitle = x.LookUpCcModRiverNature.RiverNatureTitle,
                                        RiverNatureTitleBn = x.LookUpCcModRiverNature.RiverNatureTitleBn
                                    }).ToList();
            ViewData["RiverNatureID"] = _rivNature;

            var _drainagecondition = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModDrainageCondition)
                                        .Select(x => new DrainageConditionlTemp
                                        {
                                            DrainageConditionId = x.DrainageConditionId.Value,
                                            DrainageCondition = x.LookUpCcModDrainageCondition.DrainageCondition,
                                            DrainageConditionBn = x.LookUpCcModDrainageCondition.DrainageConditionBn
                                        }).ToList();
            ViewData["DrainageConditionDetail"] = _drainagecondition;

            var _bsdetail = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModBankStability)
                                .Select(x => new BankStabilityDetailTemp
                                {
                                    BankStabilityTypeId = x.BankStabilityTypeId.Value,
                                    BankStabilityType = x.LookUpCcModBankStability.BankStabilityType,
                                    BankStabilityTypeBn = x.LookUpCcModBankStability.BankStabilityTypeBn
                                }).ToList();
            ViewData["BankStabilityDetail"] = _bsdetail;

            var _sdidetail = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            //var _analyzeoptionsdetail = GetAnalyzeOptionsDetail(pcd.ProjectId);
            //ViewData["AnalyzeOptionsDetail"] = _analyzeoptionsdetail;

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
            ViewData["Project39IndvId"] = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project39IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "River Excavation or Dredging Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform39.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform310(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_310_IndvDetail _indvdetail = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail310"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail310"] = new CcModAppProject_310_IndvDetail { Project310IndvId = 0, ProjectId = 0 };

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


            var _rivNature = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModRiverNature)
                                    .Select(x => new RiverNatureDetailTemp
                                    {
                                        RiverNatureId = x.RiverNatureId.Value,
                                        RiverNatureTitle = x.LookUpCcModRiverNature.RiverNatureTitle,
                                        RiverNatureTitleBn = x.LookUpCcModRiverNature.RiverNatureTitleBn
                                    }).ToList();
            ViewData["RiverNatureID"] = _rivNature;

            var _drainagecondition = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModDrainageCondition)
                                        .Select(x => new DrainageConditionlTemp
                                        {
                                            DrainageConditionId = x.DrainageConditionId.Value,
                                            DrainageCondition = x.LookUpCcModDrainageCondition.DrainageCondition,
                                            DrainageConditionBn = x.LookUpCcModDrainageCondition.DrainageConditionBn
                                        }).ToList();
            ViewData["DrainageConditionDetail"] = _drainagecondition;

            var _sdidetail = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModSediOfRiverOrKhal)
                                    .Select(x => new SediOfRiverOrKhalDetailTemp
                                    {
                                        SedimentationId = x.SedimentationId.Value,
                                        SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                        SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                                    }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _swadetail = (from d in _db.CcModPrjSWADetail
                              where d.ProjectId == pcd.ProjectId
                              select new SWATemp
                              {
                                  SWAId = d.SWAId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  MinWaterFlow = d.MinWaterFlow,
                                  WaterDemandMonth = d.WaterDemandMonth
                              }).OrderBy(o => o.SWAId).ToList(); ;
            ViewData["SurfaceWaterAvailDetail"] = _swadetail;

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
            ViewData["Project310IndvId"] = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project310IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Excavation - Excavation of Khal Project | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform310.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform311(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_311_IndvDetail _indvdetail = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail311"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail311"] = new CcModAppProject_311_IndvDetail { Project311IndvId = 0, ProjectId = 0 };

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


            var _fishwaterarea = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Include(i => i.LookUpCcModFishWaterArea)
                                        .Select(x => new FishWaterAreaTemp
                                        {
                                            FishWaterAreaId = x.FishWaterAreaId.Value,
                                            AreaName = x.LookUpCcModFishWaterArea.AreaName,
                                            AreaNameBn = x.LookUpCcModFishWaterArea.AreaNameBn
                                        }).ToList();
            ViewData["FishWaterAreaDetail"] = _fishwaterarea;

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
            ViewData["Project311IndvId"] = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project311IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Fisheries Project in Surface Water | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform311.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform312(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_312_IndvDetail _indvdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail312"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail312"] = new CcModAppProject_312_IndvDetail { Project312IndvId = 0, ProjectId = 0 };

            var _hydroregiondetail = GetHydrologicalRegion(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["HydroRegionDetail"] = _hydroregiondetail;

            var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new BDP2100HotSpotDetailTemp
                                    {
                                        DeltaPlanHotSpotId = x.DeltaPlanHotSpotId,
                                        PlanName = x.LookUpCcModDeltPlan2100HotSpot.PlanName,
                                        PlanNameBn = x.LookUpCcModDeltPlan2100HotSpot.PlanNameBn
                                    }).ToList();
            ViewData["BDP2100HotSpotDetail"] = _hotspotdetail;

            var _powudetail = _db.CcModPurposeOfWaterUseDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new PurposeOfWaterUseDetailTemp
                                    {
                                        PurposeOfWaterUseId = x.PurposeOfWaterUseId,
                                        WaterUsePurpose = x.LookUpCcModPurposeOfWaterUse.WaterUsePurpose,
                                        WaterUsePurposeBn = x.LookUpCcModPurposeOfWaterUse.WaterUsePurposeBn
                                    }).ToList();
            ViewData["PurposeOfWaterUseDetail"] = _powudetail;

            var _topwdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new TypeProposedWellDetailTemp
                                    {
                                        TypeProposedWellId = x.TypeProposedWellId.Value,
                                        ProposedWellName = x.LookUpCcModTypeProposedWell.ProposedWellName,
                                        ProposedWellNameBn = x.LookUpCcModTypeProposedWell.ProposedWellNameBn
                                    }).ToList();
            ViewData["TypeProposedWellDetail"] = _topwdetail;

            var _gwddetail = (from d in _db.CcModPrjGrndWtrDepthDetail
                              where d.ProjectId == pcd.ProjectId
                              select new GrndWtrDepthDetailTemp
                              {
                                  GrndWtrDepthDetailId = d.GrndWtrDepthDetailId,
                                  ProjectId = d.ProjectId,
                                  MonthId = d.MonthId,
                                  Month = mfi.GetMonthName(d.MonthId).ToString(),
                                  WaterDepth = d.WaterDepth
                              }).OrderBy(o => o.GrndWtrDepthDetailId).ToList(); ;
            ViewData["GrndWtrDepthDetail"] = _gwddetail;

            var _coadetail = _db.CcModCategoryOfAquiferDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new CategoryOfAquiferDetailTemp
                                    {
                                        CategoryOfAquiferId = x.CategoryOfAquiferId,
                                        AquiferName = x.LookUpCcModCategoryOfAquifer.AquiferName,
                                        AquiferNameBn = x.LookUpCcModCategoryOfAquifer.AquiferNameBn
                                    }).ToList();
            ViewData["CategoryOfAquiferDetail"] = _coadetail;

            var gwa_detail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new GroundWaterQualityDetailTemp
                            {
                                GroundWaterQualityId = x.GroundWaterQualityId,
                                ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                            }).ToList();
            ViewData["GroundWaterQuality"] = gwa_detail;

            var _pgwrdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new PotentialGroundWaterRechargeDetailTemp
                                    {
                                        PotGrndWtrRechargeId = x.PotGrndWtrRechargeId.Value,
                                        IndicatorName = x.LookUpCcModPotGrndWtrRecharge.IndicatorName,
                                        IndicatorNameBn = x.LookUpCcModPotGrndWtrRecharge.IndicatorNameBn
                                    }).ToList();
            ViewData["PotentialGroundWaterRechargeDetail"] = _pgwrdetail;

            var _soiltypedetail = _db.CcModSoilTypeDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new SoilTypeDetailTemp
                                    {
                                        SoilTypeId = x.SoilTypeId,
                                        SoilTypeName = x.LookUpCcModSoilType.SoilTypeName,
                                        SoilTypeNameBn = x.LookUpCcModSoilType.SoilTypeNameBn
                                    }).ToList();
            ViewData["SoilTypeDetail"] = _soiltypedetail;

            var _wucdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new WaterUseConsumptionDetailTemp
                                    {
                                        WaterUseConsumptionId = x.WaterUseConsumptionId.Value,
                                        ConsumptionName = x.LookUpCcModWaterUseConsumption.ConsumptionName,
                                        ConsumptionNameBn = x.LookUpCcModWaterUseConsumption.ConsumptionNameBn
                                    }).ToList();
            ViewData["WaterUseConsumptionDetail"] = _wucdetail;

            var _icadetails = (from d in _db.CcModPrjIrrigCropAreaDetail
                               where d.ProjectId == pcd.ProjectId
                               select new IrrigCropAreaDetailTemp
                               {
                                   IrrigatedCropId = d.IrrigatedCropId,
                                   ProjectId = d.ProjectId,
                                   CropName = d.CropName,
                                   Area = d.Area.ToString()
                               }).ToList();
            ViewData["IrrigCropAreaDetailTemp"] = _icadetails;

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
            ViewData["Project312IndvId"] = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project312IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Project for Ground Water Withdrawal, Distribution or Use | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform312.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }

        public IActionResult printform313(long id)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            UserLevelInfo uli = HttpContext.Session.GetComplexData<UserLevelInfo>("UserLevelInfo");
            ViewData["UserLevel"] = uli.UserGroupId;
            ViewData["UserAuthLevelID"] = uli.AuthorityLevelId;
            ViewData["HigherAuthLevelID"] = GetHighestLevelAuthority();

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

            CcModAppProject_313_IndvDetail _indvdetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).FirstOrDefault();
            if (_indvdetail != null)
                ViewData["ProjectIndvDetail313"] = _indvdetail;
            else
                ViewData["ProjectIndvDetail313"] = new CcModAppProject_313_IndvDetail { Project313IndvId = 0, ProjectId = 0 };

            var _hydroregiondetail = GetHydrologicalRegion(pcd.ProjectId, pcd.LanguageId ?? 0);
            ViewData["HydroRegionDetail"] = _hydroregiondetail;

            var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new BDP2100HotSpotDetailTemp
                                    {
                                        DeltaPlanHotSpotId = x.DeltaPlanHotSpotId,
                                        PlanName = x.LookUpCcModDeltPlan2100HotSpot.PlanName,
                                        PlanNameBn = x.LookUpCcModDeltPlan2100HotSpot.PlanNameBn
                                    }).ToList();
            ViewData["BDP2100HotSpotDetail"] = _hotspotdetail;

            var _hydrosystemdetail = GetHydroSystemDetail(pcd.ProjectId);
            ViewData["HydroSystemDetail"] = _hydrosystemdetail;

            var _bsdetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new BankStabilityDetailTemp
                            {
                                BankStabilityTypeId = x.BankStabilityTypeId.Value,
                                BankStabilityType = x.LookUpCcModBankStability.BankStabilityType,
                                BankStabilityTypeBn = x.LookUpCcModBankStability.BankStabilityTypeBn
                            }).ToList();
            ViewData["BankStabilityDetail"] = _bsdetail;

            var _sdidetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new SediOfRiverOrKhalDetailTemp
                            {
                                SedimentationId = x.SedimentationId.Value,
                                SedimentationName = x.LookUpCcModSediOfRiverOrKhal.SedimentationName,
                                SedimentationNameBn = x.LookUpCcModSediOfRiverOrKhal.SedimentationNameBn
                            }).ToList();
            ViewData["SediOfRiverOrKhalDetail"] = _sdidetail;

            var _khaltypedetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                            .Select(x => new KhalTypeDetailTemp
                            {
                                KhalTypeId = x.KhalTypeId.Value,
                                KhalTypeName = x.LookUpCcModKhalType.KhalTypeName,
                                KhalTypeNameBn = x.LookUpCcModKhalType.KhalTypeNameBn
                            }).ToList();
            ViewData["KhalTypeDetail"] = _khaltypedetail;

            var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new RiverTypeDetailTemp
                                    {
                                        RiverTypeId = x.RiverTypeId,
                                        RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                        RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                    }).ToList();
            ViewData["RiverTypeDetail"] = _rtdetail;

            var _typesofflood = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == pcd.ProjectId)
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

            var _drainagecondition = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId)
                                    .Select(x => new DrainageConditionlTemp
                                    {
                                        DrainageConditionId = x.DrainageConditionId.Value,
                                        DrainageCondition = x.LookUpCcModDrainageCondition.DrainageCondition,
                                        DrainageConditionBn = x.LookUpCcModDrainageCondition.DrainageConditionBn
                                    }).ToList();
            ViewData["DrainageConditionDetail"] = _drainagecondition;

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
            ViewData["Project313IndvId"] = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == pcd.ProjectId).Select(s => s.Project313IndvId).FirstOrDefault();
            ViewData["UserId"] = ui.UserID;
            ViewData["ProjectTypeId"] = pcd.ProjectTypeId;
            ViewData["Title"] = "Special Project by DG | Print";
            ViewData["ProjectTypeTitle"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectType).FirstOrDefault();
            ViewData["ProjectTypeTitleBn"] = _db.LookUpCcModProjectType.Where(w => w.ProjectTypeId == pcd.ProjectTypeId).Select(s => s.ProjectTypeBn).FirstOrDefault();
            ViewData["LanguageId"] = pcd.LanguageId;
            ViewData["SignatureFileName"] = _db.AdminModUsersDetail.Where(w => w.UserId == pcd.UserId).Select(s => s.SignatureFileName).FirstOrDefault();
            ViewData["ApplicationState"] = GetAppState(pcd.ApplicationStateId, pcd.IsCompletedId.Value);
            GetApplicantInfoViewData(pcd.UserId);

            return new ViewAsPdf("~/Views/form/printform313.cshtml", viewData: ViewData)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Landscape,
                PageMargins = new Margins(10, 10, 10, 10)
            };
        }
        #endregion

        #region Form 3.1: Flood Contorl Management
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

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

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
                                _pcd.IsCompletedId = 1;
                                _pcd.ReviewCycleNo = 0;

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
        #endregion

        #region Form 3.2: Surface Water Withdrawal, Distribution or Use
        //form/SurfaceWaterWithdrawalDistributionUse :: swwdu
        public IActionResult swwdu()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

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

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;

                LoadDropdownDataForForm32();
                SwwduNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm32();

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

                CcModAppProject_32_IndvDetail _indvdetail = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail32 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail32 = new CcModAppProject_32_IndvDetail { Project32IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;


                var _pwudetail = _db.CcModProposedWaterUseDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModProposedWaterUse)
                                    .Select(x => new ProposedWaterUseDetailTemp
                                    {
                                        ProposedWaterUseId = x.ProposedWaterUseId,
                                        WaterUseTypeName = x.LookUpCcModProposedWaterUse.WaterUseTypeName,
                                        WaterUseTypeNameBn = x.LookUpCcModProposedWaterUse.WaterUseTypeNameBn
                                    }).ToList();
                ViewBag.ProposedWaterUseDetail = _pwudetail;

                var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModRiverType)
                                        .Select(x => new RiverTypeDetailTemp
                                        {
                                            RiverTypeId = x.RiverTypeId,
                                            RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                            RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                        }).ToList();
                ViewBag.RiverTypeDetail = _rtdetail;

                var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModWtrDiversionSource)
                                    .Select(x => new WtrDiversionSourceDetailTemp
                                    {
                                        WaterDiversionSourceId = x.WaterDiversionSourceId,
                                        SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                        SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                    }).ToList();
                ViewBag.WtrDiversionSourceDetail = _wdsdetail;

                var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModGroundWaterQuality)
                                    .Select(x => new GroundWaterQualityDetailTemp
                                    {
                                        GroundWaterQualityId = x.GroundWaterQualityId,
                                        ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                        ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                                    }).ToList();
                ViewBag.GroundWaterQuality = _gwqdetail;

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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void SwwduNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_32_IndvDetail _indvdetail = new CcModAppProject_32_IndvDetail { Project32IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail32 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;
            //List<CcModPrjTypesOfFloodDetail> _typesofflood = new List<CcModPrjTypesOfFloodDetail>();
            //ViewBag.TypesOfFloodDetail = _typesofflood;
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

        #region Form 3.3 Irrigation Project by Surface Water
        //form/IrrigationProjectSurfaceWater :: ipsw
        public IActionResult ipsw()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "ভূপরিস্থ পানি দ্বারা সেচ প্রকল্প" : "Irrigation Project by Surface Water ";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm33();
                IpswNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm33();

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

                CcModAppProject_33_IndvDetail _indvdetail = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail33 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail33 = new CcModAppProject_33_IndvDetail { Project33IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;


                var _pwudetail = _db.CcModProposedWaterUseDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModProposedWaterUse)
                                    .Select(x => new ProposedWaterUseDetailTemp
                                    {
                                        ProposedWaterUseId = x.ProposedWaterUseId,
                                        WaterUseTypeName = x.LookUpCcModProposedWaterUse.WaterUseTypeName,
                                        WaterUseTypeNameBn = x.LookUpCcModProposedWaterUse.WaterUseTypeNameBn
                                    }).ToList();
                ViewBag.ProposedWaterUseDetail = _pwudetail;

                var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModRiverType)
                                        .Select(x => new RiverTypeDetailTemp
                                        {
                                            RiverTypeId = x.RiverTypeId,
                                            RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                            RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                        }).ToList();
                ViewBag.RiverTypeDetail = _rtdetail;

                var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModWtrDiversionSource)
                                    .Select(x => new WtrDiversionSourceDetailTemp
                                    {
                                        WaterDiversionSourceId = x.WaterDiversionSourceId,
                                        SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                        SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                    }).ToList();
                ViewBag.WtrDiversionSourceDetail = _wdsdetail;

                var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModGroundWaterQuality)
                                    .Select(x => new GroundWaterQualityDetailTemp
                                    {
                                        GroundWaterQualityId = x.GroundWaterQualityId,
                                        ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                        ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                                    }).ToList();
                ViewBag.GroundWaterQuality = _gwqdetail;

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

        //form/IrrigationProjectSurfaceWater :: ipsw
        [HttpPost]
        public IActionResult ipsw(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void IpswNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_33_IndvDetail _indvdetail = new CcModAppProject_33_IndvDetail { Project33IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail33 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        #region Form 3.4 Project for Construction of Hydraulic Infrastructure
        //form/ProjectConstructionHydraulicInfrastructure :: pchi
        public IActionResult pchi()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "হাইড্রোলিক অবকাঠামো নির্মাণ প্রকল্প" : "Project for Construction of Hydraulic Infrastructure";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm34();
                PchiNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm34();

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

                CcModAppProject_34_IndvDetail _indvdetail = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail34 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail34 = new CcModAppProject_34_IndvDetail { Project34IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                var _ncdetail = _db.CcModNavigationClassDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModNavigationClass)
                                    .Select(x => new NavigationClassDetailTemp
                                    {
                                        NavigationClassId = x.NavigationClassId,
                                        NavigationClassName = x.LookUpCcModNavigationClass.NavigationClassName,
                                        NavigationClassNameBn = x.LookUpCcModNavigationClass.NavigationClassNameBn
                                    }).ToList();
                ViewBag.NavigationClassDetail = _ncdetail;

                var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModRiverType)
                                        .Select(x => new RiverTypeDetailTemp
                                        {
                                            RiverTypeId = x.RiverTypeId,
                                            RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                            RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                        }).ToList();
                ViewBag.RiverTypeDetail = _rtdetail;

                var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModWtrDiversionSource)
                                    .Select(x => new WtrDiversionSourceDetailTemp
                                    {
                                        WaterDiversionSourceId = x.WaterDiversionSourceId,
                                        SourceName = x.LookUpCcModWtrDiversionSource.SourceName,
                                        SourceNameBn = x.LookUpCcModWtrDiversionSource.SourceNameBn
                                    }).ToList();
                ViewBag.WtrDiversionSourceDetail = _wdsdetail;

                var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModGroundWaterQuality)
                                    .Select(x => new GroundWaterQualityDetailTemp
                                    {
                                        GroundWaterQualityId = x.GroundWaterQualityId,
                                        ParameterName = x.LookUpCcModGroundWaterQuality.ParameterName,
                                        ParameterNameBn = x.LookUpCcModGroundWaterQuality.ParameterNameBn
                                    }).ToList();
                ViewBag.GroundWaterQuality = _gwqdetail;

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

        //form/ProjectConstructionHydraulicInfrastructure :: pchi
        [HttpPost]
        public IActionResult pchi(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void PchiNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_34_IndvDetail _indvdetail = new CcModAppProject_34_IndvDetail { Project34IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail34 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        #region Form 3.5 Surface Water Reservation Project
        //form/SurfaceWaterReservationProject :: swrp
        public IActionResult swrp()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "ভূপরিস্থ পানি সংরক্ষণ প্রকল্পের ছাড়পত্রের জন্য আবেদন" : "Surface Water Reservation Project";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm35();
                SwrpNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm35();

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

                CcModAppProject_35_IndvDetail _indvdetail = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail35 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail35 = new CcModAppProject_35_IndvDetail { Project35IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                var _stcdetail = _db.CcModStructTypeConservDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModStructTypeConserv)
                                    .Select(x => new StructTypeConDetailTemp
                                    {
                                        TypeOfStructureId = x.TypeOfStructureId,
                                        StructureName = x.LookUpCcModStructTypeConserv.StructureName,
                                        StructureNameBn = x.LookUpCcModStructTypeConserv.StructureNameBn
                                    }).ToList();
                ViewBag.StructTypeConservDetail = _stcdetail;

                var _cldetail = _db.CcModConservLocationDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModConservLocation)
                                        .Select(x => new ConservLocationDetailTemp
                                        {
                                            ConservLocationId = x.ConservLocationId,
                                            ConservLocation = x.LookUpCcModConservLocation.ConservLocation,
                                            ConservLocationBn = x.LookUpCcModConservLocation.ConservLocationBn
                                        }).ToList();
                ViewBag.ConservLocationDetail = _cldetail;

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

        //form/SurfaceWaterReservationProject :: swrp
        [HttpPost]
        public IActionResult swrp(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void SwrpNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_35_IndvDetail _indvdetail = new CcModAppProject_35_IndvDetail { Project35IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail35 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        #region Form 3.6 Wetland Development Project
        //form/WetlandDevelopmentProject :: wldp
        public IActionResult wldp()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "বন্যা প্লাবিত সমতল ভূমি বা জলাভূমি উন্নয়ন প্রকল্পের ছাড়পত্রের জন্য আবেদন" : "Wetland Development Project";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm36();
                WldpNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm36();

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

                CcModAppProject_36_IndvDetail _indvdetail = _db.CcModAppProject_36_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail36 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail36 = new CcModAppProject_36_IndvDetail { Project36IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                var _hydrosystemdetail = GetHydroSystemDetail(_psi.ProjectId);
                ViewBag.HydroSystemDetail = _hydrosystemdetail;

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

        //form/WetlandDevelopmentProject :: swrp
        [HttpPost]
        public IActionResult wldp(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void WldpNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;

            CcModAppProject_36_IndvDetail _indvdetail = new CcModAppProject_36_IndvDetail { Project36IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail36 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        #region Form 3.7 Project for Surface Water Use in Industrial Sector
        //form/SurfaceWaterIndustrialSector :: swis
        public IActionResult swis()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "শিল্পের জন্য ভূপরিস্থ পানি ব্যবহার প্রকল্পের ছাড়পত্রের জন্য আবেদন" : "Project for Surface Water Use in Industrial Sector";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm37();
                SwisNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm37();

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

                CcModAppProject_37_IndvDetail _indvdetail = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail37 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail37 = new CcModAppProject_37_IndvDetail { Project37IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;


                var _rivertypedetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.RiverTypeDetailId, x.ProjectId, x.RiverTypeId }).ToList();
                ViewBag.RiverTypeDetail = _rivertypedetail;

                var _gwqdetail = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.GroundWaterQualityDetailId, x.ProjectId, x.GroundWaterQualityId }).ToList();
                ViewBag.GroundWaterQualityDetail = _gwqdetail;

                var _towudetail = _db.CcModTypeOfWaterUseDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.TypeOfWaterUseDetailId, x.ProjectId, x.TypeOfWaterUseId }).ToList();
                ViewBag.TypeOfWaterUseDetail = _towudetail;

                var _wdsdetail = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.WaterDiversSourceDetailId, x.ProjectId, x.WaterDiversionSourceId }).ToList();
                ViewBag.WaterDiversSourceDetail = _wdsdetail;

                var _hydrosystemdetail = GetHydroSystemDetail(_psi.ProjectId);
                ViewBag.HydroSystemDetail = _hydrosystemdetail;

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

        //form/SurfaceWaterIndustrialSector :: swis
        [HttpPost]
        public IActionResult swis(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void SwisNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;

            CcModAppProject_37_IndvDetail _indvdetail = new CcModAppProject_37_IndvDetail { Project37IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail37 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        #region Form 3.8 River Bank Protection Project
        //form/RiverBankProtectionProject :: rbpp
        public IActionResult rbpp()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "নদী তীর সংরক্ষণ বা নদী শাসন প্রকল্পের ছাড়পত্রের জন্য আবেদন" : "River Bank Protection Project";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm38();
                RbppNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm38();

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

                CcModAppProject_38_IndvDetail _indvdetail = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail38 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail38 = new CcModAppProject_38_IndvDetail { Project38IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                //tech
                var _rivertypedetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.RiverTypeDetailId, x.ProjectId, x.RiverTypeId }).ToList();
                ViewBag.RiverTypeDetail = _rivertypedetail;

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

        //form/RiverBankProtectionProject :: rbpp
        [HttpPost]
        public IActionResult rbpp(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void RbppNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_38_IndvDetail _indvdetail = new CcModAppProject_38_IndvDetail { Project38IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail38 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        #region Form 3.9 River Excavation or Dredging Project
        //form/RiverExcavationDredgingProject :: redp
        public IActionResult redp()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "নদী খনন বা ড্রেজিং প্রকল্পের ছাড়পত্রের জন্য আবেদন" : "River Excavation or Dredging Project";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm39();
                RedpNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm39();

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

                CcModAppProject_39_IndvDetail _indvdetail = _db.CcModAppProject_39_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail39 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail39 = new CcModAppProject_39_IndvDetail { Project39IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                //tech
                var _ncdetail = _db.CcModNavigationClassDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModNavigationClass)
                                    .Select(x => new NavigationClassDetailTemp
                                    {
                                        NavigationClassId = x.NavigationClassId,
                                        NavigationClassName = x.LookUpCcModNavigationClass.NavigationClassName,
                                        NavigationClassNameBn = x.LookUpCcModNavigationClass.NavigationClassNameBn
                                    }).ToList();
                ViewBag.NavigationClassDetail = _ncdetail;

                var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModRiverType)
                                        .Select(x => new RiverTypeDetailTemp
                                        {
                                            RiverTypeId = x.RiverTypeId,
                                            RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                            RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                        }).ToList();
                ViewBag.RiverTypeDetail = _rtdetail;


                //Deed start
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

        //form/RiverBankProtectionProject :: redp
        [HttpPost]
        public IActionResult redp(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void RedpNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_39_IndvDetail _indvdetail = new CcModAppProject_39_IndvDetail { Project39IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail39 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        #region Form 3.10 Excavation-Excavation of Khal Project
        //form/ExcavationExcavationKhalProject :: eekp
        public IActionResult eekp()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "খাল খনন বা পুনঃখনন প্রকল্পের ছাড়পত্রের জন্য আবেদন" : "Excavation-Excavation of Khal Project";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm310();
                EekpNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm310();

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

                CcModAppProject_310_IndvDetail _indvdetail = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail310 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail310 = new CcModAppProject_310_IndvDetail { Project310IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                //tech
                var _ncdetail = _db.CcModNavigationClassDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModNavigationClass)
                                    .Select(x => new NavigationClassDetailTemp
                                    {
                                        NavigationClassId = x.NavigationClassId,
                                        NavigationClassName = x.LookUpCcModNavigationClass.NavigationClassName,
                                        NavigationClassNameBn = x.LookUpCcModNavigationClass.NavigationClassNameBn
                                    }).ToList();
                ViewBag.NavigationClassDetail = _ncdetail;

                var _rtdetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == _psi.ProjectId).Include(i => i.LookUpCcModRiverType)
                                        .Select(x => new RiverTypeDetailTemp
                                        {
                                            RiverTypeId = x.RiverTypeId,
                                            RiverTypeName = x.LookUpCcModRiverType.RiverTypeName,
                                            RiverTypeNameBn = x.LookUpCcModRiverType.RiverTypeNameBn
                                        }).ToList();
                ViewBag.RiverTypeDetail = _rtdetail;


                //Deed start
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

        //form/ExcavationExcavationKhalProject :: eekp
        [HttpPost]
        public IActionResult eekp(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void EekpNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;

            CcModAppProject_310_IndvDetail _indvdetail = new CcModAppProject_310_IndvDetail { Project310IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail310 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        #region Form 3.11 Fisheries Project in Surface Water
        //form/FisheriesProjectSurfaceWater :: fpsw
        public IActionResult fpsw()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "ভূপরিস্থ পানিতে মৎস্য উন্নয়ন প্রকল্পের ছাড়পত্রের জন্য আবেদন" : "Fisheries Project in Surface Water";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm311();
                FpswNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm311();

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

                CcModAppProject_311_IndvDetail _indvdetail = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail311 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail311 = new CcModAppProject_311_IndvDetail { Project311IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                //Deed start
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

        //form/FisheriesProjectSurfaceWater :: fpsw
        [HttpPost]
        public IActionResult fpsw(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void FpswNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;

            CcModAppProject_311_IndvDetail _indvdetail = new CcModAppProject_311_IndvDetail { Project311IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail311 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        private void LoadDropdownDataForForm311()
        {
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();

            ViewBag.LookUpCcModFishWaterArea = _db.LookUpCcModFishWaterArea.ToList();

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
        }
        #endregion

        #region Form 3.12 Project for Ground Water Withdrawal, Distribution or Use
        //form/GroundWaterWithdrawalUse :: gwwu
        public IActionResult gwwu()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "ভূপরিস্থ পানি প্রত্যাহার, বিতরণ বা ব্যবহারের জন্য প্রকল্প" : "Project for Ground Water Withdrawal, Distribution or Use";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm312();
                GwwuNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm312();

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

                CcModAppProject_312_IndvDetail _indvdetail = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail312 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail312 = new CcModAppProject_312_IndvDetail { Project312IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                //Deed start
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

        //form/GroundWaterWithdrawalUse :: gwwu
        [HttpPost]
        public IActionResult gwwu(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void GwwuNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;

            CcModAppProject_312_IndvDetail _indvdetail = new CcModAppProject_312_IndvDetail { Project312IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail312 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

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

        private void LoadDropdownDataForForm312()
        {
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();
            ViewBag.LookUpCcModPurposeOfWaterUse = _db.LookUpCcModPurposeOfWaterUse.ToList(); //multi
            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList(); //multi
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList(); //multi

            ViewBag.LookUpCcModTypeProposedWell = _db.LookUpCcModTypeProposedWell.ToList();
            ViewBag.LookUpCcModCategoryOfAquifer = _db.LookUpCcModCategoryOfAquifer.ToList(); //multi
            ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList(); //multi
            ViewBag.LookUpCcModPotGrndWtrRecharge = _db.LookUpCcModPotGrndWtrRecharge.ToList();
            ViewBag.LookUpCcModSoilType = _db.LookUpCcModSoilType.ToList(); //multi
            ViewBag.LookUpCcModWaterUseConsumption = _db.LookUpCcModWaterUseConsumption.ToList();

            ViewBag.LookUpCcModNWPArticle = _db.LookUpCcModNWPArticle.ToList();
            ViewBag.LookUpCcModNWMPProgramme = _db.LookUpCcModNWMPProgramme.ToList();
            ViewBag.LookUpCcModSDGGoal = _db.LookUpCcModSDGGoal.ToList();
            ViewBag.LookUpCcModSDGIndicator = _db.LookUpCcModSDGIndicator.ToList();
            ViewBag.LookUpCcModDeltPlan2100Goal = _db.LookUpCcModDeltPlan2100Goal.ToList();
            ViewBag.LookUpCcModGPWMGroupType = _db.LookUpCcModGPWMGroupType.ToList();
            ViewBag.RecommendationId = _db.LookUpCcModRecommendation.ToList();
        }
        #endregion

        #region Form 3.13 Special Project by DG
        //form/GroundWaterWithdrawalUse :: fcdi
        public IActionResult fcdi()
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            if (ui == null)
            {
                return RedirectToAction("login", "account");
            }

            SelectedForm sf = HttpContext.Session.GetComplexData<SelectedForm>("SelectedForm");
            if (sf == null)
            {
                return RedirectToAction("index", "form");
            }

            ViewData["Title"] = sf.LanguageTypeId == "1" ? "মহাপরিচালক যেরূপ উপযুক্ত মনে করিবে সেইরূপ অন্যান্য প্রকল্পের ছাড়পত্রের জন্য আবেদন" : "Special Project by DG";
            ViewBag.ProjectTypeId = sf.ProjectTypeId;
            ViewBag.ProjectTitle = sf.ProjectTitle;

            ProjectStatusInfo _psi = GetProjectInfoByType(sf.ProjectTypeId.ToString(), ui.UserID);

            if (_psi == null)
            {
                CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail
                {
                    ProjectId = 0
                };

                ViewBag.ProjectCommonDetail = _pcd;
                LoadDropdownDataForForm313();
                FcdiNewEmptyForm();
            }
            else if (_psi.ProjectId != 0 && _psi.AppSubmissionId == null)
            {
                TempData["Message"] = ch.ShowMessage(Sign.Warning, "Problem Occured", "Sorry, " + sf.ProjectTitle + " form submission status not found! Please contact with System Administrator.");
                return RedirectToAction("index", "form");
            }
            else
            {
                LoadDropdownDataForForm313();

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

                CcModAppProject_313_IndvDetail _indvdetail = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == _psi.ProjectId).FirstOrDefault();
                if (_indvdetail != null)
                    ViewBag.ProjectIndvDetail313 = _indvdetail;
                else
                    ViewBag.ProjectIndvDetail313 = new CcModAppProject_313_IndvDetail { Project313IndvId = 0, ProjectId = 0 };

                var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                            .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
                ViewBag.HydroRegionDetail = _hydroregiondetail;

                var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
                ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

                var _rivertypedetail = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.RiverTypeDetailId, x.ProjectId, x.RiverTypeId }).ToList();
                ViewBag.RiverTypeDetail = _rivertypedetail;

                var _typesofflooddetail = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == _psi.ProjectId)
                                        .Select(x => new { x.FloodTypeDetailId, x.ProjectId, x.FloodTypeId }).ToList();
                ViewBag.TypesOfFloodDetail = _typesofflooddetail;

                //Deed start
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

        //form/GroundWaterWithdrawalUse :: fcdi
        [HttpPost]
        public IActionResult fcdi(long id)
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
                                _pcd.IsCompletedId = 2; // need to change after re-logic check
                                _pcd.ReviewCycleNo = 0;

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

            return Json(noti);
        }

        private void FcdiNewEmptyForm()
        {
            CcModAppProjectCommonDetail _pcd = new CcModAppProjectCommonDetail { ProjectId = 0 };
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = new CcModPrjLocationDetail();
            ViewBag.ProjectLocationDetail = _locationdetail;

            CcModAppProject_313_IndvDetail _indvdetail = new CcModAppProject_313_IndvDetail { Project313IndvId = 0, ProjectId = 0 };
            ViewBag.ProjectIndvDetail313 = _indvdetail;

            List<CcModPrjHydroRegionDetail> _hydroregiondetail = new List<CcModPrjHydroRegionDetail>();
            ViewBag.HydroRegionDetail = _hydroregiondetail;
            List<CcModBDP2100HotSpotDetail> _hotspotdetail = new List<CcModBDP2100HotSpotDetail>();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;
            List<CcModPrjTypesOfFloodDetail> _typesofflood = new List<CcModPrjTypesOfFloodDetail>();
            ViewBag.TypesOfFloodDetail = _typesofflood;

            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();
            ViewBag.LookUpCcModTypeOfFlood = _db.LookUpCcModTypeOfFlood.ToList();
            ViewBag.FloodFrequencyId = _db.LookUpCcModFloodFrequency.ToList();
            ViewBag.DesignSubmittedParameterId = _db.LookUpCcModDesignSubmitParam.ToList();
            ViewBag.LookUpCcModBankStability = _db.LookUpCcModBankStability.ToList();
            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            ViewBag.LookUpCcModKhalType = _db.LookUpCcModKhalType.ToList();
            ViewBag.LookUpCcModRiverType = _db.LookUpCcModRiverType.ToList();
            ViewBag.LookUpCcModDrainageCondition = _db.LookUpCcModDrainageCondition.ToList();
            ViewBag.LookUpCcModEIAParameter = _db.LookUpCcModEIAParameter.ToList();
            ViewBag.LookUpCcModSIAParameter = _db.LookUpCcModSIAParameter.ToList();
            ViewBag.LookUpCcModEcoAndFinancial = _db.LookUpCcModEcoAndFinancial.ToList();

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

        private void LoadDropdownDataForForm313()
        {
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();
            //ViewBag.LookUpCcModPurposeOfWaterUse = _db.LookUpCcModPurposeOfWaterUse.ToList(); //multi
            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList(); //multi
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList(); //multi

            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();//multi
            ViewBag.LookUpCcModTypeOfFlood = _db.LookUpCcModTypeOfFlood.ToList();//multi
            ViewBag.FloodFrequencyId = _db.LookUpCcModFloodFrequency.ToList();
            ViewBag.DesignSubmittedParameterId = _db.LookUpCcModDesignSubmitParam.ToList();
            ViewBag.LookUpCcModBankStability = _db.LookUpCcModBankStability.ToList();
            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            ViewBag.LookUpCcModKhalType = _db.LookUpCcModKhalType.ToList();//multi
            ViewBag.LookUpCcModRiverType = _db.LookUpCcModRiverType.ToList();
            ViewBag.LookUpCcModDrainageCondition = _db.LookUpCcModDrainageCondition.ToList();//multi
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
        }
        #endregion

        #region User Defined Methods
        public string GetAppState(int appStateId, int isCompletedId)
        {
            if (isCompletedId == 2)
            {
                appStateId = appStateId - 1;
            }
            if (isCompletedId == 4)
            {
                appStateId = appStateId - 2;
            }

            if (isCompletedId == 3)
            {
                appStateId = appStateId + 1;
            }

            if (isCompletedId == 5)
            {
                appStateId = appStateId + 2;
            }

            string ApplicationState = _db.LookUpCcModApplicationState.Where(w => w.ApplicationStateId == appStateId).Select(s => s.ApplicationState).FirstOrDefault(); ;
            return ApplicationState.Replace("Pending for Final Approval of ", "").Replace("Pending for Review of ", "");
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

        /* //this method blocked on 07 Jun, 2020 and new logic written below this method on same day
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
        */

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

        private void LoadDropdownDataForForm32()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();
            //ViewBag.LookUpAdminBndUpazila = _db.LookUpAdminBndUpazila.ToList();
            //ViewBag.LookUpAdminBndUnion = _db.LookUpAdminBndUnion.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModProposedWaterUse = _db.LookUpCcModProposedWaterUse.ToList();
            ViewBag.LookUpCcModRiverType = _db.LookUpCcModRiverType.ToList();
            ViewBag.LookUpCcModTypeOfWaterBody = _db.LookUpCcModTypeOfWaterBody.ToList();
            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();
            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();

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

        private void LoadDropdownDataForForm33()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModTypeOfWaterBody = _db.LookUpCcModTypeOfWaterBody.ToList();
            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();
            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();

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

        private void LoadDropdownDataForForm34()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModNavigationClass = _db.LookUpCcModNavigationClass.ToList();
            ViewBag.LookUpCcModRiverType = _db.LookUpCcModRiverType.ToList();
            ViewBag.LookUpCcModRiverNature = _db.LookUpCcModRiverNature.ToList();
            ViewBag.LookUpCcModUsDsCondition = _db.LookUpCcModUsDsCondition.ToList();
            ViewBag.LookUpCcModFloodFrequency = _db.LookUpCcModFloodFrequency.ToList();
            ViewBag.LookUpCcModBankLineShifting = _db.LookUpCcModBankLineShifting.ToList();
            ViewBag.LookUpCcModBankStability = _db.LookUpCcModBankStability.ToList();
            ViewBag.LookUpCcModHydraInfraParam = _db.LookUpCcModHydraInfraParam.ToList();

            //ViewBag.LookUpCcModTypeOfWaterBody = _db.LookUpCcModTypeOfWaterBody.ToList();
            //ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();
            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            //ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            //ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();

            ViewBag.DesignSubmittedParameterId = _db.LookUpCcModDesignSubmitParam.ToList();
            //ViewBag.LookUpCcModDrainageCondition = _db.LookUpCcModDrainageCondition.ToList();
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

        private void LoadDropdownDataForForm35()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModStructTypeConserv = _db.LookUpCcModStructTypeConserv.ToList();
            ViewBag.LookUpCcModConservLocation = _db.LookUpCcModConservLocation.ToList();

            ViewBag.LookUpCcModRiverNature = _db.LookUpCcModRiverNature.ToList();

            ViewBag.LookUpCcModUsDsCondition = _db.LookUpCcModUsDsCondition.ToList();
            ViewBag.LookUpCcModFloodFrequency = _db.LookUpCcModFloodFrequency.ToList();
            ViewBag.LookUpCcModBankLineShifting = _db.LookUpCcModBankLineShifting.ToList();
            ViewBag.LookUpCcModBankStability = _db.LookUpCcModBankStability.ToList();
            ViewBag.LookUpCcModHydraInfraParam = _db.LookUpCcModHydraInfraParam.ToList();

            ViewBag.LookUpCcModTypeOfWaterBody = _db.LookUpCcModTypeOfWaterBody.ToList();
            //ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();
            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            //ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            //ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();

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

        private void LoadDropdownDataForForm36()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();

            //ViewBag.LookUpCcModStructTypeConserv = _db.LookUpCcModStructTypeConserv.ToList();
            //ViewBag.LookUpCcModConservLocation = _db.LookUpCcModConservLocation.ToList();
            //ViewBag.LookUpCcModRiverNature = _db.LookUpCcModRiverNature.ToList();
            //ViewBag.LookUpCcModUsDsCondition = _db.LookUpCcModUsDsCondition.ToList();
            //ViewBag.LookUpCcModFloodFrequency = _db.LookUpCcModFloodFrequency.ToList();
            //ViewBag.LookUpCcModBankLineShifting = _db.LookUpCcModBankLineShifting.ToList();
            //ViewBag.LookUpCcModBankStability = _db.LookUpCcModBankStability.ToList();
            //ViewBag.LookUpCcModHydraInfraParam = _db.LookUpCcModHydraInfraParam.ToList();

            //ViewBag.LookUpCcModTypeOfWaterBody = _db.LookUpCcModTypeOfWaterBody.ToList();
            //ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();
            //ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            //ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            //ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();

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

        private void LoadDropdownDataForForm37()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();

            ViewBag.LookUpCcModWaterBody = _db.LookUpCcModWaterBody.ToList();
            ViewBag.LookUpCcModRiverType = _db.LookUpCcModRiverType.ToList();
            ViewBag.LookUpCcModTypeOfWaterBody = _db.LookUpCcModTypeOfWaterBody.ToList();
            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();
            ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();
            ViewBag.LookUpCcModTypeOfWaterUse = _db.LookUpCcModTypeOfWaterUse.ToList();
            ViewBag.LookUpCcModWaterUseSector = _db.LookUpCcModWaterUseSector.ToList();
            ViewBag.LookUpCcModWaterUse = _db.LookUpCcModWaterUse.ToList();
            ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            ViewBag.LookUpCcModGrndWtrWthdrwParam = _db.LookUpCcModGrndWtrWthdrwParam.ToList();

            ViewBag.LookUpCcModEIAParameter = _db.LookUpCcModEIAParameter.ToList();
            ViewBag.LookUpCcModSIAParameter = _db.LookUpCcModSIAParameter.ToList();

            ViewBag.LookUpCcModNWPArticle = _db.LookUpCcModNWPArticle.ToList();
            ViewBag.LookUpCcModNWMPProgramme = _db.LookUpCcModNWMPProgramme.ToList();
            ViewBag.LookUpCcModSDGGoal = _db.LookUpCcModSDGGoal.ToList();
            ViewBag.LookUpCcModSDGIndicator = _db.LookUpCcModSDGIndicator.ToList();
            ViewBag.LookUpCcModDeltPlan2100Goal = _db.LookUpCcModDeltPlan2100Goal.ToList();
            ViewBag.LookUpCcModGPWMGroupType = _db.LookUpCcModGPWMGroupType.ToList();
            ViewBag.RecommendationId = _db.LookUpCcModRecommendation.ToList();
            #endregion
        }

        private void LoadDropdownDataForForm38()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModNavigationClass = _db.LookUpCcModNavigationClass.ToList();

            ViewBag.LookUpCcModRiverType = _db.LookUpCcModRiverType.ToList();
            ViewBag.LookUpCcModRiverNature = _db.LookUpCcModRiverNature.ToList();

            ViewBag.LookUpCcModBankLineShifting = _db.LookUpCcModBankLineShifting.ToList();
            ViewBag.LookUpCcModBankStability = _db.LookUpCcModBankStability.ToList();
            //ViewBag.LookUpCcModHydraInfraParam = _db.LookUpCcModHydraInfraParam.ToList();

            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            //ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            //ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();

            ViewBag.DesignSubmittedParameterId = _db.LookUpCcModDesignSubmitParam.ToList();
            //ViewBag.LookUpCcModDrainageCondition = _db.LookUpCcModDrainageCondition.ToList();
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

        private void LoadDropdownDataForForm39()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();

            ViewBag.LookUpCcModRiverNature = _db.LookUpCcModRiverNature.ToList();
            ViewBag.LookUpCcModUsDsCondition = _db.LookUpCcModUsDsCondition.ToList();
            ViewBag.LookUpCcModDrainageCondition = _db.LookUpCcModDrainageCondition.ToList();
            ViewBag.LookUpCcModNavigationClass = _db.LookUpCcModNavigationClass.ToList();
            ViewBag.LookUpCcModRiverType = _db.LookUpCcModRiverType.ToList();
            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();
            ViewBag.LookUpCcModBankStability = _db.LookUpCcModBankStability.ToList();
            ViewBag.LookUpCcModDredgingPlanParam = _db.LookUpCcModDredgingPlanParam.ToList();

            //ViewBag.LookUpCcModBankLineShifting = _db.LookUpCcModBankLineShifting.ToList();	
            //ViewBag.LookUpCcModHydraInfraParam = _db.LookUpCcModHydraInfraParam.ToList();	
            //ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            //ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();
            //ViewBag.DesignSubmittedParameterId = _db.LookUpCcModDesignSubmitParam.ToList();

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

        private void LoadDropdownDataForForm310()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();

            ViewBag.LookUpCcModRiverNature = _db.LookUpCcModRiverNature.ToList();
            //ViewBag.LookUpCcModUsDsCondition = _db.LookUpCcModUsDsCondition.ToList();
            ViewBag.LookUpCcModDrainageCondition = _db.LookUpCcModDrainageCondition.ToList();
            //ViewBag.LookUpCcModNavigationClass = _db.LookUpCcModNavigationClass.ToList();
            //ViewBag.LookUpCcModRiverType = _db.LookUpCcModRiverType.ToList();

            ViewBag.LookUpCcModSediOfRiverOrKhal = _db.LookUpCcModSediOfRiverOrKhal.ToList();

            //ViewBag.LookUpCcModBankStability = _db.LookUpCcModBankStability.ToList();
            //ViewBag.LookUpCcModDredgingPlanParam = _db.LookUpCcModDredgingPlanParam.ToList();

            //ViewBag.LookUpCcModBankLineShifting = _db.LookUpCcModBankLineShifting.ToList();	
            //ViewBag.LookUpCcModHydraInfraParam = _db.LookUpCcModHydraInfraParam.ToList();	
            //ViewBag.LookUpCcModWtrDiversionSource = _db.LookUpCcModWtrDiversionSource.ToList();
            //ViewBag.LookUpCcModGroundWaterQuality = _db.LookUpCcModGroundWaterQuality.ToList();
            //ViewBag.DesignSubmittedParameterId = _db.LookUpCcModDesignSubmitParam.ToList();

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
        #endregion

        #region Dropdown and General Info Save
        private ProjectStatusInfo GetProjectInfoByType(string projectTypeId, long userId)
        {
            var result = _db.CcModAppProjectCommonDetail
                        .Where(w => w.ProjectTypeId == projectTypeId.ToInt() && w.AppSubmissionId == 0 && w.UserId == userId)
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

        //Surface Water Availability at Intake Point
        private List<SWATemp> GetSurfaceWaterAvailabilityDetail(long project_id)
        {
            List<SWATemp> _details = new List<SWATemp>();

            try
            {
                _details = (from d in _db.CcModPrjSWADetail
                            join m in _db.LookUpCcModMonth on d.MonthId equals m.MonthId
                            where d.ProjectId == project_id
                            select new SWATemp
                            {
                                SWAId = d.SWAId,
                                ProjectId = d.ProjectId,
                                Month = m.MonthName,
                                MonthBn = m.MonthNameBn,
                                MinWaterFlow = d.MinWaterFlow,
                                WaterDemandMonth = d.WaterDemandMonth
                            }).OrderBy(o => o.SWAId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new SWATemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new SWATemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        //Ground Water Depth (m) from Nearest Observation Well
        private List<GrndWtrDepthDetailTemp> GetGroundWaterDepthDetail(long project_id)
        {
            List<GrndWtrDepthDetailTemp> _details = new List<GrndWtrDepthDetailTemp>();

            try
            {
                _details = (from d in _db.CcModPrjGrndWtrDepthDetail
                            join m in _db.LookUpCcModMonth on d.MonthId equals m.MonthId
                            where d.ProjectId == project_id
                            select new GrndWtrDepthDetailTemp
                            {
                                GrndWtrDepthDetailId = d.GrndWtrDepthDetailId,
                                ProjectId = d.ProjectId,
                                Month = m.MonthName,
                                MonthBn = m.MonthNameBn,
                                WaterDepth = d.WaterDepth
                            }).OrderBy(o => o.GrndWtrDepthDetailId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new GrndWtrDepthDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new GrndWtrDepthDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        //Detail Water Use
        private List<WaterUseDetailTemp> GetWaterUseDetail(long project_id)
        {
            List<WaterUseDetailTemp> _details = new List<WaterUseDetailTemp>();

            try
            {
                _details = (from d in _db.CcModWaterUseDetail
                            join m in _db.LookUpCcModWaterUse on d.WaterUseId equals m.WaterUseId
                            where d.ProjectId == project_id
                            select new WaterUseDetailTemp
                            {
                                WaterUseDetailId = d.WaterUseDetailId,
                                ProjectId = d.ProjectId,
                                WaterUseId = d.WaterUseId,
                                WaterUseName = m.WaterUseName,
                                WaterUseNameBn = m.WaterUseNameBn,
                                ExistingDemand = d.ExistingDemand,
                                ProposedDemand = d.ProposedDemand,
                                TotalDemand = d.TotalDemand,
                                YearConductingPeriod = d.YearConductingPeriod,
                                AnnualDemand = d.AnnualDemand
                            }).OrderBy(o => o.WaterUseDetailId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new WaterUseDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new WaterUseDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
        }

        //Ground Water Withdrawal
        private List<GroundWaterWithdrawalDetailTemp> GetGroundWaterWithdrawalDetail(long project_id)
        {
            List<GroundWaterWithdrawalDetailTemp> _details = new List<GroundWaterWithdrawalDetailTemp>();

            try
            {
                _details = (from d in _db.CcModGroundWaterWithdrawDetail
                            join m in _db.LookUpCcModGrndWtrWthdrwParam on d.GrndWtrWithdrawalParamId equals m.GrndWtrWithdrawalParamId
                            where d.ProjectId == project_id
                            select new GroundWaterWithdrawalDetailTemp
                            {
                                GroundWaterWithdrawDetailId = d.GroundWaterWithdrawDetailId,
                                ProjectId = d.ProjectId,
                                GrndWtrWithdrawalParamId = d.GrndWtrWithdrawalParamId,
                                ParameterName = m.ParameterName,
                                ParameterNameBn = m.ParameterNameBn,
                                ExistingInfrastructure = d.ExistingInfrastructure,
                                ProposedInfrastructure = d.ProposedInfrastructure
                            }).OrderBy(o => o.GroundWaterWithdrawDetailId).ToList();

                if (_details.Count == 0)
                {
                    _details.Add(new GroundWaterWithdrawalDetailTemp()
                    {
                        Error = "sorry, no data found!"
                    });
                }
            }
            catch (Exception ex)
            {
                var message = ch.ExtractInnerException(ex);

                _details.Add(new GroundWaterWithdrawalDetailTemp()
                {
                    Error = message
                });
            }

            return _details;
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

        #region Surface Water Availability
        //form/SurfaceWaterAvailabilityDetailSave :: swads
        [HttpPost]
        public JsonResult swads(CcModPrjSWADetail _swad)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_swad != null && _swad.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_swad.SWAId != 0)
                        {
                            _db.Entry(_swad).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _swad.SWAId.ToString(),
                                    status = "success",
                                    message = "Surface water availability information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _swad.SWAId.ToString(),
                                    status = "error",
                                    message = "Surface water availability information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjSWADetail.Add(_swad);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _swad.SWAId.ToString(),
                                        status = "success",
                                        message = "Surface water availability information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _swad.SWAId.ToString(),
                                        status = "error",
                                        message = "Surface water availability information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _swad.SWAId.ToString(),
                                    status = "error",
                                    message = "Saving data has been rollbacked. " + message
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

        //form/GetSurfaceWaterAvailabilityDetail :: get_swad
        [HttpGet]
        public JsonResult get_swad(long project_id)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            try
            {
                var _details = (from d in _db.CcModPrjSWADetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.SWAId,
                                    d.ProjectId,
                                    d.MonthId,
                                    Month = mfi.GetMonthName(d.MonthId).ToString(),
                                    d.MinWaterFlow,
                                    d.WaterDemandMonth
                                }).OrderBy(o => o.SWAId).ToList();

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

        //form/GetSurfaceWaterAvailabilityDetail:: gswad
        [HttpGet]
        public JsonResult gswad(long id, long projectId)
        {
            CcModPrjSWADetail _hsd = _db.CcModPrjSWADetail.Where(w => w.ProjectId == projectId && w.SWAId == id).FirstOrDefault();
            return Json(_hsd);
        }
        #endregion

        #region Detail Water Use
        //form/DetailWaterUse :: dwus
        [HttpPost]
        public JsonResult dwus(CcModWaterUseDetail _dwud)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_dwud != null && _dwud.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_dwud.WaterUseDetailId != 0)
                        {
                            _db.Entry(_dwud).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _dwud.WaterUseDetailId.ToString(),
                                    status = "success",
                                    message = "Detail water use information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _dwud.WaterUseDetailId.ToString(),
                                    status = "error",
                                    message = "Detail water use information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModWaterUseDetail.Add(_dwud);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _dwud.WaterUseDetailId.ToString(),
                                        status = "success",
                                        message = "Detail water use information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _dwud.WaterUseDetailId.ToString(),
                                        status = "error",
                                        message = "Detail water use information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _dwud.WaterUseDetailId.ToString(),
                                    status = "error",
                                    message = "Saving data has been rollbacked. " + message
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

        //form/DetailWaterUse :: get_dwud
        [HttpGet]
        public JsonResult get_dwud(long project_id)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            try
            {
                var _details = (from d in _db.CcModWaterUseDetail.Include(i => i.LookUpCcModWaterUse)
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.WaterUseDetailId,
                                    d.ProjectId,
                                    d.WaterUseId,
                                    WaterUseName = d.LookUpCcModWaterUse.WaterUseName,
                                    WaterUseNameBn = d.LookUpCcModWaterUse.WaterUseNameBn,
                                    d.ExistingDemand,
                                    d.ProposedDemand,
                                    d.TotalDemand,
                                    d.YearConductingPeriod,
                                    d.AnnualDemand
                                }).OrderBy(o => o.WaterUseDetailId).ToList();

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

        //form/DetailWaterUse:: gdwud
        [HttpGet]
        public JsonResult gdwud(long id, long projectId)
        {
            CcModWaterUseDetail _hsd = _db.CcModWaterUseDetail.Where(w => w.ProjectId == projectId && w.WaterUseDetailId == id).FirstOrDefault();
            return Json(_hsd);
        }
        #endregion

        #region Detail of Ground Water Withdrawal
        //form/GroundWaterWithdrawal :: gwws
        [HttpPost]
        public JsonResult gwws(CcModGroundWaterWithdrawDetail _gwwd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_gwwd != null && _gwwd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_gwwd.GroundWaterWithdrawDetailId != 0)
                        {
                            _db.Entry(_gwwd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _gwwd.GroundWaterWithdrawDetailId.ToString(),
                                    status = "success",
                                    message = "Ground water withdrawal information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _gwwd.GroundWaterWithdrawDetailId.ToString(),
                                    status = "error",
                                    message = "Ground water withdrawal information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModGroundWaterWithdrawDetail.Add(_gwwd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _gwwd.GroundWaterWithdrawDetailId.ToString(),
                                        status = "success",
                                        message = "Ground water withdrawal information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _gwwd.GroundWaterWithdrawDetailId.ToString(),
                                        status = "error",
                                        message = "Ground water withdrawal information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _gwwd.GroundWaterWithdrawDetailId.ToString(),
                                    status = "error",
                                    message = "Saving data has been rollbacked. " + message
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

        //form/GroundWaterWithdrawal :: get_gwwd
        [HttpGet]
        public JsonResult get_gwwd(long project_id)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            try
            {
                var _details = (from d in _db.CcModGroundWaterWithdrawDetail.Include(i => i.LookUpCcModGrndWtrWthdrwParam)
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.GroundWaterWithdrawDetailId,
                                    d.ProjectId,
                                    d.GrndWtrWithdrawalParamId,
                                    ParameterName = d.LookUpCcModGrndWtrWthdrwParam.ParameterName,
                                    ParameterNameBn = d.LookUpCcModGrndWtrWthdrwParam.ParameterNameBn,
                                    d.ExistingInfrastructure,
                                    d.ProposedInfrastructure
                                }).OrderBy(o => o.GroundWaterWithdrawDetailId).ToList();

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

        //form/GroundWaterWithdrawal:: gwwd
        [HttpGet]
        public JsonResult gwwd(long id, long projectId)
        {
            CcModGroundWaterWithdrawDetail _gww = _db.CcModGroundWaterWithdrawDetail.Where(w => w.ProjectId == projectId && w.GroundWaterWithdrawDetailId == id).FirstOrDefault();
            return Json(_gww);
        }
        #endregion

        #region Design parameters of hydraulic infrastructure
        //form/DesignParametersHydraulicInfrastructure :: sdphi
        [HttpPost]
        public JsonResult sdphi(CcModHydraInfraParamDetail _dphi)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_dphi != null && _dphi.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_dphi.HydraInfraParamDetailId != 0)
                        {
                            _db.Entry(_dphi).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _dphi.HydraInfraParamDetailId.ToString(),
                                    status = "success",
                                    message = "Hydraulic infrastructure information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _dphi.HydraInfraParamDetailId.ToString(),
                                    status = "error",
                                    message = "Hydraulic infrastructure information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModHydraInfraParamDetail.Add(_dphi);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _dphi.HydraInfraParamDetailId.ToString(),
                                        status = "success",
                                        message = "Hydraulic infrastructure information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _dphi.HydraInfraParamDetailId.ToString(),
                                        status = "error",
                                        message = "Hydraulic infrastructure information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _dphi.HydraInfraParamDetailId.ToString(),
                                    status = "error",
                                    message = "Saving data has been rollbacked. " + message
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

        //form/GetSurfaceWaterAvailabilityDetail :: get_swad
        [HttpGet]
        public JsonResult get_dphi(long project_id)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            try
            {
                var _details = (from d in _db.CcModHydraInfraParamDetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.HydraInfraParamDetailId,
                                    d.ProjectId,
                                    d.HydraInfraParamId,
                                    d.LookUpCcModHydraInfraParam.ParameterName,
                                    d.LookUpCcModHydraInfraParam.ParameterNameBn,
                                    d.Description
                                }).OrderBy(o => o.HydraInfraParamDetailId).ToList();

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

        //form/GetSurfaceWaterAvailabilityDetail:: gswad
        [HttpGet]
        public JsonResult gdphi(long id, long projectId)
        {
            CcModHydraInfraParamDetail _hsd = _db.CcModHydraInfraParamDetail.Where(w => w.ProjectId == projectId && w.HydraInfraParamDetailId == id).FirstOrDefault();
            return Json(_hsd);
        }
        #endregion

        #region U/S and D/S Condition for Proposed Intervention
        //form/USDSConditionProposedIntervention :: sdphi
        [HttpPost]
        public JsonResult susds(CcModUsDsConditionDetail _usds)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_usds != null && _usds.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_usds.UsDsConditionDetailId != 0)
                        {
                            _db.Entry(_usds).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _usds.UsDsConditionDetailId.ToString(),
                                    status = "success",
                                    message = "U/S and D/S Condition information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _usds.UsDsConditionDetailId.ToString(),
                                    status = "error",
                                    message = "U/S and D/S Condition information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModUsDsConditionDetail.Add(_usds);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _usds.UsDsConditionDetailId.ToString(),
                                        status = "success",
                                        message = "U/S and D/S Condition information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _usds.UsDsConditionDetailId.ToString(),
                                        status = "error",
                                        message = "U/S and D/S Condition information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _usds.UsDsConditionDetailId.ToString(),
                                    status = "error",
                                    message = "Saving data has been rollbacked. " + message
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

        //form/GetSurfaceWaterAvailabilityDetail :: get_swad
        [HttpGet]
        public JsonResult get_usds(long project_id)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            try
            {
                var _details = (from d in _db.CcModUsDsConditionDetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.UsDsConditionDetailId,
                                    d.ProjectId,
                                    d.UsDsConditionId,
                                    d.LookUpCcModUsDsCondition.ParameterName,
                                    d.LookUpCcModUsDsCondition.ParameterNameBn,
                                    d.UsParameterValue
                                }).OrderBy(o => o.UsDsConditionDetailId).ToList();

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

        //form/GetSurfaceWaterAvailabilityDetail:: gswad
        [HttpGet]
        public JsonResult gusds(long id, long projectId)
        {
            CcModUsDsConditionDetail _hsd = _db.CcModUsDsConditionDetail.Where(w => w.ProjectId == projectId && w.UsDsConditionDetailId == id).FirstOrDefault();
            return Json(_hsd);
        }
        #endregion

        #region Ground Water Depth
        //form/GroundWaterDepthDetailSave :: gwdds_gedd
        [HttpPost]
        public JsonResult gwdds(CcModPrjGrndWtrDepthDetail _gedd)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            int result = 0;

            try
            {
                if (_gedd != null && _gedd.ProjectId != 0)
                {
                    using (var dbContextTransaction = _db.Database.BeginTransaction())
                    {
                        if (_gedd.GrndWtrDepthDetailId != 0)
                        {
                            _db.Entry(_gedd).State = EntityState.Modified;
                            result = _db.SaveChanges();

                            if (result > 0)
                            {
                                dbContextTransaction.Commit();

                                noti = new Notification
                                {
                                    id = _gedd.GrndWtrDepthDetailId.ToString(),
                                    status = "success",
                                    message = "Ground water information has been updated successfully."
                                };
                            }
                            else
                            {
                                dbContextTransaction.Rollback();

                                noti = new Notification
                                {
                                    id = _gedd.GrndWtrDepthDetailId.ToString(),
                                    status = "error",
                                    message = "Ground water information not updated."
                                };
                            }
                        }
                        else
                        {
                            try
                            {
                                _db.CcModPrjGrndWtrDepthDetail.Add(_gedd);
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = _gedd.GrndWtrDepthDetailId.ToString(),
                                        status = "success",
                                        message = "Ground water information has been saved successfully."
                                    };
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();

                                    noti = new Notification
                                    {
                                        id = _gedd.GrndWtrDepthDetailId.ToString(),
                                        status = "error",
                                        message = "Ground water information not saved."
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                var message = ch.ExtractInnerException(ex);

                                noti = new Notification
                                {
                                    id = _gedd.GrndWtrDepthDetailId.ToString(),
                                    status = "error",
                                    message = "Saving data has been rollbacked. " + message
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

        //form/GetSurfaceWaterAvailabilityDetail :: get_gwdd
        [HttpGet]
        public JsonResult get_gwdd(long project_id)
        {
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            try
            {
                var _details = (from d in _db.CcModPrjGrndWtrDepthDetail
                                where d.ProjectId == project_id
                                select new
                                {
                                    d.GrndWtrDepthDetailId,
                                    d.ProjectId,
                                    d.MonthId,
                                    Month = mfi.GetMonthName(d.MonthId).ToString(),
                                    d.WaterDepth
                                }).OrderBy(o => o.GrndWtrDepthDetailId).ToList();

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

        //form/GetSingleHydrologicalSystem :: ggwdd
        [HttpGet]
        public JsonResult ggwdd(long id, long projectId)
        {
            CcModPrjGrndWtrDepthDetail _hsd = _db.CcModPrjGrndWtrDepthDetail.Where(w => w.ProjectId == projectId && w.GrndWtrDepthDetailId == id).FirstOrDefault();
            return Json(_hsd);
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
                                where d.ProjectId == project_id
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

        #region Form 3.1 One to One Save > Flood Control Management
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
        //form/AllFormRecommandDetailSave :: afrds > Administrative
        [HttpPost]
        public JsonResult afrds(CcModAppProjectCommonDetail _recDetail)
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

        #region Form 3.2 > Surface Water Withdrawal, Distribution or Use
        //Tehnical Info
        //form/Form32TechInfoOneToOneSave :: f32tiotos
        [HttpPost]
        public JsonResult f32tiotos(Form32TechInfo _form32TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form32TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form32TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form32TechInfo.CommonDetail;
                        CcModAppProject_32_IndvDetail Project32Indv = _form32TechInfo.Project32Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form32TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form32TechInfo.BDP2100HotSpot;

                        List<CcModProposedWaterUseDetail> ProposedWaterUseDetail = _form32TechInfo.ProposedWaterUseDetail;
                        List<CcModRiverTypeDetail> RiverTypeDetail = _form32TechInfo.RiverTypeDetail;
                        List<CcModGroundWaterQualityDetail> GroundWaterQltDetail = _form32TechInfo.GroundWaterQltDetail;
                        List<CcModWaterDiversSourceDetail> WaterDivSourceDetail = _form32TechInfo.WaterDivSourceDetail;
                        #endregion

                        #region Common Detail Data Binding
                        pcd.AnnualRainFallLast1Year = CommonDetail.AnnualRainFallLast1Year;
                        pcd.AnnualRainFallLast2Years = CommonDetail.AnnualRainFallLast2Years;
                        pcd.AnnualRainFallLast3Years = CommonDetail.AnnualRainFallLast3Years;
                        pcd.AnnualRainFallLast4Years = CommonDetail.AnnualRainFallLast4Years;
                        pcd.AnnualRainFallLast5Years = CommonDetail.AnnualRainFallLast5Years;
                        pcd.IssueChallageProblem = CommonDetail.IssueChallageProblem;
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
                            CcModAppProject_32_IndvDetail p32i = _db.CcModAppProject_32_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p32i == null)
                            {
                                _db.CcModAppProject_32_IndvDetail.Add(Project32Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (ProposedWaterUseDetail != null && ProposedWaterUseDetail.Count > 0)
                                {
                                    _db.CcModProposedWaterUseDetail.AddRange(ProposedWaterUseDetail);
                                }

                                if (RiverTypeDetail != null && RiverTypeDetail.Count > 0)
                                {
                                    _db.CcModRiverTypeDetail.AddRange(RiverTypeDetail);
                                }

                                if (GroundWaterQltDetail != null && GroundWaterQltDetail.Count > 0)
                                {
                                    _db.CcModGroundWaterQualityDetail.AddRange(GroundWaterQltDetail);
                                }

                                if (WaterDivSourceDetail != null && WaterDivSourceDetail.Count > 0)
                                {
                                    _db.CcModWaterDiversSourceDetail.AddRange(WaterDivSourceDetail);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project32Indv.Project32IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p32i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p32i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (ProposedWaterUseDetail != null && ProposedWaterUseDetail.Count > 0)
                                {
                                    List<CcModProposedWaterUseDetail> tempPwuList = _db.CcModProposedWaterUseDetail.Where(w => w.ProjectId == p32i.ProjectId).ToList();

                                    if (tempPwuList.Count > 0)
                                    {
                                        _db.CcModProposedWaterUseDetail.RemoveRange(tempPwuList);
                                    }
                                }

                                if (RiverTypeDetail != null && RiverTypeDetail.Count > 0)
                                {
                                    List<CcModRiverTypeDetail> tempRtList = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == p32i.ProjectId).ToList();

                                    if (tempRtList.Count > 0)
                                    {
                                        _db.CcModRiverTypeDetail.RemoveRange(tempRtList);
                                    }
                                }

                                if (GroundWaterQltDetail != null && GroundWaterQltDetail.Count > 0)
                                {
                                    List<CcModGroundWaterQualityDetail> tempGwqList = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == p32i.ProjectId).ToList();

                                    if (tempGwqList.Count > 0)
                                    {
                                        _db.CcModGroundWaterQualityDetail.RemoveRange(tempGwqList);
                                    }
                                }

                                if (WaterDivSourceDetail != null && WaterDivSourceDetail.Count > 0)
                                {
                                    List<CcModWaterDiversSourceDetail> tempWdsList = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == p32i.ProjectId).ToList();

                                    if (tempWdsList.Count > 0)
                                    {
                                        _db.CcModWaterDiversSourceDetail.RemoveRange(tempWdsList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                _db.CcModProposedWaterUseDetail.AddRange(ProposedWaterUseDetail);
                                _db.CcModRiverTypeDetail.AddRange(RiverTypeDetail);
                                _db.CcModGroundWaterQualityDetail.AddRange(GroundWaterQltDetail);
                                _db.CcModWaterDiversSourceDetail.AddRange(WaterDivSourceDetail);
                                #endregion

                                #region Project 32 Indvidual Data Binding
                                p32i.NameOfTheWaterBody = Project32Indv.NameOfTheWaterBody;
                                p32i.WaterBodyTypeId = Project32Indv.WaterBodyTypeId;
                                p32i.Offtake = Project32Indv.Offtake;
                                p32i.Outfall = Project32Indv.Outfall;
                                p32i.Connectivity = Project32Indv.Connectivity;
                                p32i.ExistingUseOfWaterFromSource = Project32Indv.ExistingUseOfWaterFromSource;
                                p32i.WaterLevelDryMax = Project32Indv.WaterLevelDryMax;
                                p32i.WaterLevelDryMin = Project32Indv.WaterLevelDryMin;
                                p32i.WaterLevelWetMax = Project32Indv.WaterLevelWetMax;
                                p32i.WaterLevelWetMin = Project32Indv.WaterLevelWetMin;
                                p32i.DischargeDryMax = Project32Indv.DischargeDryMax;
                                p32i.DischargeDryMin = Project32Indv.DischargeDryMin;
                                p32i.DischargeWetMax = Project32Indv.DischargeWetMax;
                                p32i.DischargeWetMin = Project32Indv.DischargeWetMin;
                                p32i.CrossSectionDepth = Project32Indv.CrossSectionDepth;
                                p32i.CrossSectionWidth = Project32Indv.CrossSectionWidth;
                                p32i.SedimentationId = Project32Indv.SedimentationId;
                                p32i.SedimentationRate = Project32Indv.SedimentationRate;
                                p32i.FishProduction = Project32Indv.FishProduction;
                                p32i.FishDiversity = Project32Indv.FishDiversity;
                                p32i.FishMigration = Project32Indv.FishMigration;
                                p32i.FloraAndFauna = Project32Indv.FloraAndFauna;
                                p32i.WaterWithdrawQuantityPerDay = Project32Indv.WaterWithdrawQuantityPerDay;
                                p32i.UseOfFlowMeterMeasrYNId = Project32Indv.UseOfFlowMeterMeasrYNId;
                                p32i.NoOfPump = Project32Indv.NoOfPump;
                                p32i.PumpCapacity = Project32Indv.PumpCapacity;
                                p32i.PipeDiameter = Project32Indv.PipeDiameter;
                                p32i.DivertedWaterRtnSrcYNId = Project32Indv.DivertedWaterRtnSrcYNId;
                                p32i.WaterSalinity = Project32Indv.WaterSalinity;
                                p32i.WaterDO = Project32Indv.WaterDO;
                                p32i.WaterTDS = Project32Indv.WaterTDS;
                                p32i.WaterPhLevel = Project32Indv.WaterPhLevel;
                                p32i.UseOfToolsYesNoId = Project32Indv.UseOfToolsYesNoId;
                                p32i.ToolsApplicantComments = Project32Indv.ToolsApplicantComments;
                                p32i.ToolsAuthorityComments = Project32Indv.ToolsAuthorityComments;
                                #endregion

                                _db.Entry(p32i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project32Indv.Project32IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        public JsonResult f32dootos(Form32DeedObligatory _form32DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form32DeedObli.CommonDetail.ProjectId;
            Int64 Project32IndvId = _form32DeedObli.Project32Indv.Project32IndvId;

            try
            {
                if (_form32DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_32_IndvDetail p32id = _db.CcModAppProject_32_IndvDetail.Find(Project32IndvId); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p32id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form32DeedObli.CommonDetail;
                        CcModAppProject_32_IndvDetail Project32Indv = _form32DeedObli.Project32Indv;

                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form32DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form32DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form32DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form32DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form32DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form32DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 32 Individual Data Binding
                        p32id.DuplicatYesNoId = Project32Indv.DuplicatYesNoId;
                        p32id.DuplicationApplicantComments = Project32Indv.DuplicationApplicantComments;
                        p32id.DuplicationAuthorityComments = Project32Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        _db.Entry(p32id).State = EntityState.Modified;

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
                                id = p32id.Project32IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.3 > Irrigation Project by Surface Water
        //Tehnical Info
        //form/Form32TechInfoOneToOneSave :: f32tiotos
        [HttpPost]
        public JsonResult f33tiotos(Form33TechInfo _form33TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form33TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form33TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form33TechInfo.CommonDetail;
                        CcModAppProject_33_IndvDetail Project33Indv = _form33TechInfo.Project33Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form33TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form33TechInfo.BDP2100HotSpot;
                        List<CcModGroundWaterQualityDetail> GroundWaterQltDetail = _form33TechInfo.GroundWaterQltDetail;
                        List<CcModWaterDiversSourceDetail> WaterDivSourceDetail = _form33TechInfo.WaterDivSourceDetail;
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
                            CcModAppProject_33_IndvDetail p33i = _db.CcModAppProject_33_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p33i == null)
                            {
                                _db.CcModAppProject_33_IndvDetail.Add(Project33Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (GroundWaterQltDetail != null && GroundWaterQltDetail.Count > 0)
                                {
                                    _db.CcModGroundWaterQualityDetail.AddRange(GroundWaterQltDetail);
                                }

                                if (WaterDivSourceDetail != null && WaterDivSourceDetail.Count > 0)
                                {
                                    _db.CcModWaterDiversSourceDetail.AddRange(WaterDivSourceDetail);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project33Indv.Project33IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p33i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p33i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (GroundWaterQltDetail != null && GroundWaterQltDetail.Count > 0)
                                {
                                    List<CcModGroundWaterQualityDetail> tempGwqList = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == p33i.ProjectId).ToList();

                                    if (tempGwqList.Count > 0)
                                    {
                                        _db.CcModGroundWaterQualityDetail.RemoveRange(tempGwqList);
                                    }
                                }

                                if (WaterDivSourceDetail != null && WaterDivSourceDetail.Count > 0)
                                {
                                    List<CcModWaterDiversSourceDetail> tempWdsList = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == p33i.ProjectId).ToList();

                                    if (tempWdsList.Count > 0)
                                    {
                                        _db.CcModWaterDiversSourceDetail.RemoveRange(tempWdsList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                _db.CcModGroundWaterQualityDetail.AddRange(GroundWaterQltDetail);
                                _db.CcModWaterDiversSourceDetail.AddRange(WaterDivSourceDetail);
                                #endregion

                                #region Project 33 Indvidual Data Binding
                                p33i.NameOfTheWaterBody = Project33Indv.NameOfTheWaterBody;
                                p33i.WaterBodyTypeId = Project33Indv.WaterBodyTypeId;
                                p33i.Offtake = Project33Indv.Offtake;
                                p33i.Outfall = Project33Indv.Outfall;
                                p33i.Connectivity = Project33Indv.Connectivity;
                                p33i.ExistingUseOfWaterFromSource = Project33Indv.ExistingUseOfWaterFromSource;
                                p33i.WaterLevelDryMax = Project33Indv.WaterLevelDryMax;
                                p33i.WaterLevelDryMin = Project33Indv.WaterLevelDryMin;
                                p33i.WaterLevelWetMax = Project33Indv.WaterLevelWetMax;
                                p33i.WaterLevelWetMin = Project33Indv.WaterLevelWetMin;
                                p33i.DischargeDryMax = Project33Indv.DischargeDryMax;
                                p33i.DischargeDryMin = Project33Indv.DischargeDryMin;
                                p33i.DischargeWetMax = Project33Indv.DischargeWetMax;
                                p33i.DischargeWetMin = Project33Indv.DischargeWetMin;
                                p33i.CrossSectionDepth = Project33Indv.CrossSectionDepth;
                                p33i.CrossSectionWidth = Project33Indv.CrossSectionWidth;
                                p33i.SedimentationId = Project33Indv.SedimentationId;
                                p33i.SedimentationRate = Project33Indv.SedimentationRate;
                                p33i.HighLandPercent = Project33Indv.HighLandPercent;
                                p33i.MediumHighLandPercent = Project33Indv.MediumHighLandPercent;
                                p33i.MediumLowLandPercent = Project33Indv.MediumLowLandPercent;
                                p33i.LowLandPercent = Project33Indv.LowLandPercent;
                                p33i.VeryLowLandPercent = Project33Indv.VeryLowLandPercent;
                                p33i.CultivableCrops = Project33Indv.CultivableCrops;
                                p33i.CropProduction = Project33Indv.CropProduction;
                                p33i.FishProduction = Project33Indv.FishProduction;
                                p33i.FishDiversity = Project33Indv.FishDiversity;
                                p33i.FishMigration = Project33Indv.FishMigration;
                                p33i.FloraAndFauna = Project33Indv.FloraAndFauna;
                                p33i.WaterWithdrawQuantityPerDay = Project33Indv.WaterWithdrawQuantityPerDay;
                                p33i.UseOfFlowMeterMeasrYNId = Project33Indv.UseOfFlowMeterMeasrYNId;
                                p33i.NoOfPump = Project33Indv.NoOfPump;
                                p33i.PumpCapacity = Project33Indv.PumpCapacity;
                                p33i.PipeDiameter = Project33Indv.PipeDiameter;
                                p33i.DivertedWaterRtnSrcYNId = Project33Indv.DivertedWaterRtnSrcYNId;
                                p33i.WaterSalinity = Project33Indv.WaterSalinity;
                                p33i.WaterTDS = Project33Indv.WaterTDS;
                                p33i.WaterPhLevel = Project33Indv.WaterPhLevel;
                                p33i.UseOfToolsYesNoId = Project33Indv.UseOfToolsYesNoId;
                                p33i.ToolsApplicantComments = Project33Indv.ToolsApplicantComments;
                                p33i.ToolsAuthorityComments = Project33Indv.ToolsAuthorityComments;
                                #endregion

                                _db.Entry(p33i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project33Indv.Project33IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        public JsonResult f33dootos(Form33DeedObligatory _form33DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form33DeedObli.CommonDetail.ProjectId;
            Int64 Project33IndvId = _form33DeedObli.Project33Indv.Project33IndvId;

            try
            {
                if (_form33DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_33_IndvDetail p33id = _db.CcModAppProject_33_IndvDetail.Find(Project33IndvId); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p33id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form33DeedObli.CommonDetail;
                        CcModAppProject_33_IndvDetail Project33Indv = _form33DeedObli.Project33Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form33DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form33DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form33DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form33DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form33DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form33DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 33 Individual Data Binding
                        p33id.DuplicatYesNoId = Project33Indv.DuplicatYesNoId;
                        p33id.DuplicationApplicantComments = Project33Indv.DuplicationApplicantComments;
                        p33id.DuplicationAuthorityComments = Project33Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        _db.Entry(p33id).State = EntityState.Modified;

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
                                id = p33id.Project33IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.4 > Project for Construction of Hydraulic Infrastructure
        //Tehnical Info
        //form/Form34TechInfoOneToOneSave :: f34tiotos
        [HttpPost]
        public JsonResult f34tiotos(Form34TechInfo _form34TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form34TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form34TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form34TechInfo.CommonDetail;
                        CcModAppProject_34_IndvDetail Project34Indv = _form34TechInfo.Project34Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form34TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form34TechInfo.BDP2100HotSpot;
                        List<CcModNavigationClassDetail> NavigationClassDetail = _form34TechInfo.NavigationClassDetail;
                        List<CcModRiverTypeDetail> RiverTypeDetail = _form34TechInfo.RiverTypeDetail;
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
                            CcModAppProject_34_IndvDetail p34i = _db.CcModAppProject_34_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p34i == null)
                            {
                                _db.CcModAppProject_34_IndvDetail.Add(Project34Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (NavigationClassDetail != null && NavigationClassDetail.Count > 0)
                                {
                                    _db.CcModNavigationClassDetail.AddRange(NavigationClassDetail);
                                }

                                if (RiverTypeDetail != null && RiverTypeDetail.Count > 0)
                                {
                                    _db.CcModRiverTypeDetail.AddRange(RiverTypeDetail);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project34Indv.Project34IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p34i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p34i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (NavigationClassDetail != null && NavigationClassDetail.Count > 0)
                                {
                                    List<CcModNavigationClassDetail> ncList = _db.CcModNavigationClassDetail.Where(w => w.ProjectId == p34i.ProjectId).ToList();

                                    if (ncList.Count > 0)
                                    {
                                        _db.CcModNavigationClassDetail.RemoveRange(ncList);
                                    }
                                }

                                if (RiverTypeDetail != null && RiverTypeDetail.Count > 0)
                                {
                                    List<CcModRiverTypeDetail> rtList = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == p34i.ProjectId).ToList();

                                    if (rtList.Count > 0)
                                    {
                                        _db.CcModRiverTypeDetail.RemoveRange(rtList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                _db.CcModNavigationClassDetail.AddRange(NavigationClassDetail);
                                _db.CcModRiverTypeDetail.AddRange(RiverTypeDetail);
                                #endregion

                                #region Project 34 Indvidual Data Binding
                                p34i.NameOfHydraulicStructure = Project34Indv.NameOfHydraulicStructure;
                                p34i.TypeOfStructure = Project34Indv.TypeOfStructure;
                                p34i.TypeOfTrafficNavigation = Project34Indv.TypeOfTrafficNavigation;
                                p34i.RiverNatureId = Project34Indv.RiverNatureId;
                                p34i.CommandArea = Project34Indv.CommandArea;
                                p34i.CrossSectionDepth = Project34Indv.CrossSectionDepth;
                                p34i.CrossSectionWidth = Project34Indv.CrossSectionWidth;
                                p34i.WaterLevelMax = Project34Indv.WaterLevelMax;
                                p34i.WaterLevelMin = Project34Indv.WaterLevelMin;
                                p34i.DischargeMax = Project34Indv.DischargeMax;
                                p34i.DischargeMin = Project34Indv.DischargeMin;
                                p34i.BankLineShiftingId = Project34Indv.BankLineShiftingId;
                                p34i.BankStabilityTypeId = Project34Indv.BankStabilityTypeId;
                                p34i.BankSoilCondition = Project34Indv.BankSoilCondition;
                                p34i.BankErosionLength = Project34Indv.BankErosionLength;
                                p34i.BankErosionArea = Project34Indv.BankErosionArea;
                                p34i.BankErosionLocation = Project34Indv.BankErosionLocation;
                                p34i.BankErosionRate = Project34Indv.BankErosionRate;
                                p34i.RiverBedErosion = Project34Indv.RiverBedErosion;
                                p34i.SedimentationId = Project34Indv.SedimentationId;
                                p34i.SedimentationRate = Project34Indv.SedimentationRate;
                                p34i.CharAccretionLength = Project34Indv.CharAccretionLength;
                                p34i.CharAccretionArea = Project34Indv.CharAccretionArea;
                                p34i.CharAccretionLocation = Project34Indv.CharAccretionLocation;
                                p34i.RiverTrainingWorks = Project34Indv.RiverTrainingWorks;
                                #endregion

                                _db.Entry(p34i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project34Indv.Project34IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form34DeedObligatoryOneToOneSave :: f34otos        
        [HttpPost]
        public JsonResult f34dootos(Form34DeedObligatory _form34DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form34DeedObli.CommonDetail.ProjectId;
            Int64 Project34IndvId = _form34DeedObli.Project34Indv.Project34IndvId;

            try
            {
                if (_form34DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_34_IndvDetail p34id = _db.CcModAppProject_34_IndvDetail.Find(Project34IndvId); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p34id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form34DeedObli.CommonDetail;
                        CcModAppProject_34_IndvDetail Project34Indv = _form34DeedObli.Project34Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form34DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form34DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form34DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form34DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form34DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form34DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 34 Individual Data Binding
                        p34id.DuplicatYesNoId = Project34Indv.DuplicatYesNoId;
                        p34id.DuplicationApplicantComments = Project34Indv.DuplicationApplicantComments;
                        p34id.DuplicationAuthorityComments = Project34Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        _db.Entry(p34id).State = EntityState.Modified;

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
                                id = p34id.Project34IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.5 > Surface Water Reservation Project
        //Tehnical Info
        //form/Form34TechInfoOneToOneSave :: f34tiotos
        [HttpPost]
        public JsonResult f35tiotos(Form35TechInfo _form35TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form35TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form35TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form35TechInfo.CommonDetail;
                        CcModAppProject_35_IndvDetail Project35Indv = _form35TechInfo.Project35Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form35TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form35TechInfo.BDP2100HotSpot;
                        List<CcModStructTypeConservDetail> StructTypeConservDetail = _form35TechInfo.StructTypeConservDetail;
                        List<CcModConservLocationDetail> ConservLocationDetail = _form35TechInfo.ConservLocationDetail;
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
                            CcModAppProject_35_IndvDetail p35i = _db.CcModAppProject_35_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p35i == null)
                            {
                                _db.CcModAppProject_35_IndvDetail.Add(Project35Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (StructTypeConservDetail != null && StructTypeConservDetail.Count > 0)
                                {
                                    _db.CcModStructTypeConservDetail.AddRange(StructTypeConservDetail);
                                }

                                if (ConservLocationDetail != null && ConservLocationDetail.Count > 0)
                                {
                                    _db.CcModConservLocationDetail.AddRange(ConservLocationDetail);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project35Indv.Project35IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p35i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p35i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (StructTypeConservDetail != null && StructTypeConservDetail.Count > 0)
                                {
                                    List<CcModStructTypeConservDetail> stcList = _db.CcModStructTypeConservDetail.Where(w => w.ProjectId == p35i.ProjectId).ToList();

                                    if (stcList.Count > 0)
                                    {
                                        _db.CcModStructTypeConservDetail.RemoveRange(stcList);
                                    }
                                }

                                if (ConservLocationDetail != null && ConservLocationDetail.Count > 0)
                                {
                                    List<CcModConservLocationDetail> clList = _db.CcModConservLocationDetail.Where(w => w.ProjectId == p35i.ProjectId).ToList();

                                    if (clList.Count > 0)
                                    {
                                        _db.CcModConservLocationDetail.RemoveRange(clList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                _db.CcModStructTypeConservDetail.AddRange(StructTypeConservDetail);
                                _db.CcModConservLocationDetail.AddRange(ConservLocationDetail);
                                #endregion

                                #region Project 35 Indvidual Data Binding
                                p35i.WaterBodyTypeId = Project35Indv.WaterBodyTypeId;
                                p35i.Offtake = Project35Indv.Offtake;
                                p35i.Outfall = Project35Indv.Outfall;
                                p35i.Connectivity = Project35Indv.Connectivity;
                                //p35i.ExistingUseOfWaterFromSource = Project35Indv.ExistingUseOfWaterFromSource;
                                p35i.WaterLevelDryMax = Project35Indv.WaterLevelDryMax;
                                p35i.WaterLevelDryMin = Project35Indv.WaterLevelDryMin;
                                p35i.WaterLevelWetMax = Project35Indv.WaterLevelWetMax;
                                p35i.WaterLevelWetMin = Project35Indv.WaterLevelWetMin;
                                p35i.DischargeDryMax = Project35Indv.DischargeDryMax;
                                p35i.DischargeDryMin = Project35Indv.DischargeDryMin;
                                p35i.DischargeWetMax = Project35Indv.DischargeWetMax;
                                p35i.DischargeWetMin = Project35Indv.DischargeWetMin;
                                p35i.CrossSectionDepth = Project35Indv.CrossSectionDepth;
                                p35i.CrossSectionWidth = Project35Indv.CrossSectionWidth;
                                p35i.SedimentationId = Project35Indv.SedimentationId;
                                p35i.SedimentationRate = Project35Indv.SedimentationRate;
                                p35i.DrainageConditionId = Project35Indv.DrainageConditionId;
                                p35i.HighLandPercent = Project35Indv.HighLandPercent;
                                p35i.MediumHighLandPercent = Project35Indv.MediumHighLandPercent;
                                p35i.MediumLowLandPercent = Project35Indv.MediumLowLandPercent;
                                p35i.LowLandPercent = Project35Indv.LowLandPercent;
                                p35i.VeryLowLandPercent = Project35Indv.VeryLowLandPercent;
                                p35i.CropProduction = Project35Indv.CropProduction;
                                p35i.FishProduction = Project35Indv.FishProduction;
                                p35i.FishDiversity = Project35Indv.FishDiversity;
                                p35i.FishMigration = Project35Indv.FishMigration;
                                p35i.FloraAndFauna = Project35Indv.FloraAndFauna;
                                #endregion

                                _db.Entry(p35i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project35Indv.Project35IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form35DeedObligatoryOneToOneSave :: f35otos        
        [HttpPost]
        public JsonResult f35dootos(Form35DeedObligatory _form35DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form35DeedObli.CommonDetail.ProjectId;
            Int64 Project35IndvId = _form35DeedObli.Project35Indv.Project35IndvId;

            try
            {
                if (_form35DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_35_IndvDetail p35id = _db.CcModAppProject_35_IndvDetail.Find(Project35IndvId); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p35id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form35DeedObli.CommonDetail;
                        CcModAppProject_35_IndvDetail Project35Indv = _form35DeedObli.Project35Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form35DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form35DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form35DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form35DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form35DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form35DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 35 Individual Data Binding
                        p35id.DuplicatYesNoId = Project35Indv.DuplicatYesNoId;
                        p35id.DuplicationApplicantComments = Project35Indv.DuplicationApplicantComments;
                        p35id.DuplicationAuthorityComments = Project35Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        _db.Entry(p35id).State = EntityState.Modified;

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
                                id = p35id.Project35IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.6 > Wetland Development Project
        //Tehnical Info
        //form/Form34TechInfoOneToOneSave :: f34tiotos
        [HttpPost]
        public JsonResult f36tiotos(Form36TechInfo _form36TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form36TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form36TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form36TechInfo.CommonDetail;
                        CcModAppProject_36_IndvDetail Project36Indv = _form36TechInfo.Project36Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form36TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form36TechInfo.BDP2100HotSpot;
                        #endregion

                        #region Common Detail Data Binding
                        pcd.AnnualRainFallLast1Year = CommonDetail.AnnualRainFallLast1Year;
                        pcd.AnnualRainFallLast2Years = CommonDetail.AnnualRainFallLast2Years;
                        pcd.AnnualRainFallLast3Years = CommonDetail.AnnualRainFallLast3Years;
                        pcd.AnnualRainFallLast4Years = CommonDetail.AnnualRainFallLast4Years;
                        pcd.AnnualRainFallLast5Years = CommonDetail.AnnualRainFallLast5Years;
                        pcd.EnvAndSocialYesNoId = CommonDetail.EnvAndSocialYesNoId;
                        pcd.EnvAndSocialApplicantCmt = CommonDetail.EnvAndSocialApplicantCmt;
                        pcd.EnvAndSocialAuthorityCmt = CommonDetail.EnvAndSocialAuthorityCmt;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            CcModAppProject_36_IndvDetail p36i = _db.CcModAppProject_36_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p36i == null)
                            {
                                _db.CcModAppProject_36_IndvDetail.Add(Project36Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project36Indv.Project36IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p36i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p36i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                #endregion

                                #region Project 36 Indvidual Data Binding
                                p36i.ConnectivityAmidWaterland = Project36Indv.ConnectivityAmidWaterland;
                                p36i.CatchmentArea = Project36Indv.CatchmentArea;
                                p36i.HighestFloodLevel = Project36Indv.HighestFloodLevel;
                                p36i.DrainageConditionId = Project36Indv.DrainageConditionId;
                                p36i.HighLandPercent = Project36Indv.HighLandPercent;
                                p36i.MediumHighLandPercent = Project36Indv.MediumHighLandPercent;
                                p36i.MediumLowLandPercent = Project36Indv.MediumLowLandPercent;
                                p36i.LowLandPercent = Project36Indv.LowLandPercent;
                                p36i.VeryLowLandPercent = Project36Indv.VeryLowLandPercent;
                                p36i.FishProduction = Project36Indv.FishProduction;
                                p36i.FishDiversity = Project36Indv.FishDiversity;
                                p36i.FishMigration = Project36Indv.FishMigration;
                                p36i.LandUseMapYesNoId = Project36Indv.LandUseMapYesNoId;
                                p36i.LandUseMapApplicantComments = Project36Indv.LandUseMapApplicantComments;
                                p36i.LandUseMapAuthorityComments = Project36Indv.LandUseMapAuthorityComments;
                                p36i.LandUseDesignYesNoId = Project36Indv.LandUseDesignYesNoId;
                                p36i.LandUseDesignApplicantComments = Project36Indv.LandUseDesignApplicantComments;
                                p36i.LandUseDesignAuthorityComments = Project36Indv.LandUseDesignAuthorityComments;
                                p36i.ImpactFloodPlainAreaYesNoId = Project36Indv.ImpactFloodPlainAreaYesNoId;
                                p36i.ImpctFldPlnAraApplicntComments = Project36Indv.ImpctFldPlnAraApplicntComments;
                                p36i.ImpctFldPlnAraAuthortyComments = Project36Indv.ImpctFldPlnAraAuthortyComments;
                                #endregion

                                _db.Entry(p36i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project36Indv.Project36IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form36DeedObligatoryOneToOneSave :: f36otos        
        [HttpPost]
        public JsonResult f36dootos(Form36DeedObligatory _form36DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form36DeedObli.CommonDetail.ProjectId;
            Int64 Project36IndvId = _form36DeedObli.Project36Indv.Project36IndvId;

            try
            {
                if (_form36DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_36_IndvDetail p36id = _db.CcModAppProject_36_IndvDetail.Find(Project36IndvId); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p36id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form36DeedObli.CommonDetail;
                        CcModAppProject_36_IndvDetail Project36Indv = _form36DeedObli.Project36Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form36DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form36DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form36DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form36DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form36DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form36DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 36 Individual Data Binding
                        p36id.DuplicatYesNoId = Project36Indv.DuplicatYesNoId;
                        p36id.DuplicationApplicantComments = Project36Indv.DuplicationApplicantComments;
                        p36id.DuplicationAuthorityComments = Project36Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        _db.Entry(p36id).State = EntityState.Modified;

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
                                id = p36id.Project36IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.7 > Project for Surface Water Use in Industrial Sector
        //Tehnical Info
        //form/Form34TechInfoOneToOneSave :: f37tiotos
        [HttpPost]
        public JsonResult f37tiotos(Form37TechInfo _form37TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form37TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form37TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form37TechInfo.CommonDetail;
                        CcModAppProject_37_IndvDetail Project37Indv = _form37TechInfo.Project37Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form37TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form37TechInfo.BDP2100HotSpot;

                        List<CcModRiverTypeDetail> RiverTypeDetail = _form37TechInfo.RiverTypeDetail;
                        List<CcModGroundWaterQualityDetail> GroundWaterQualityDetail = _form37TechInfo.GroundWaterQualityDetail;
                        List<CcModTypeOfWaterUseDetail> TypeOfWaterUseDetail = _form37TechInfo.TypeOfWaterUseDetail;
                        List<CcModWaterDiversSourceDetail> WaterDiversSourceDetail = _form37TechInfo.WaterDiversSourceDetail;
                        #endregion

                        #region Common Detail Data Binding
                        pcd.AnnualRainFallLast1Year = CommonDetail.AnnualRainFallLast1Year;
                        pcd.AnnualRainFallLast2Years = CommonDetail.AnnualRainFallLast2Years;
                        pcd.AnnualRainFallLast3Years = CommonDetail.AnnualRainFallLast3Years;
                        pcd.AnnualRainFallLast4Years = CommonDetail.AnnualRainFallLast4Years;
                        pcd.AnnualRainFallLast5Years = CommonDetail.AnnualRainFallLast5Years;
                        pcd.EnvAndSocialYesNoId = CommonDetail.EnvAndSocialYesNoId;
                        pcd.EnvAndSocialApplicantCmt = CommonDetail.EnvAndSocialApplicantCmt;
                        pcd.EnvAndSocialAuthorityCmt = CommonDetail.EnvAndSocialAuthorityCmt;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            CcModAppProject_37_IndvDetail p37i = _db.CcModAppProject_37_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p37i == null)
                            {
                                _db.CcModAppProject_37_IndvDetail.Add(Project37Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (RiverTypeDetail != null && RiverTypeDetail.Count > 0)
                                {
                                    _db.CcModRiverTypeDetail.AddRange(RiverTypeDetail);
                                }

                                if (GroundWaterQualityDetail != null && GroundWaterQualityDetail.Count > 0)
                                {
                                    _db.CcModGroundWaterQualityDetail.AddRange(GroundWaterQualityDetail);
                                }

                                if (TypeOfWaterUseDetail != null && TypeOfWaterUseDetail.Count > 0)
                                {
                                    _db.CcModTypeOfWaterUseDetail.AddRange(TypeOfWaterUseDetail);
                                }

                                if (WaterDiversSourceDetail != null && WaterDiversSourceDetail.Count > 0)
                                {
                                    _db.CcModWaterDiversSourceDetail.AddRange(WaterDiversSourceDetail);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project37Indv.Project37IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p37i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p37i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (RiverTypeDetail != null && RiverTypeDetail.Count > 0)
                                {
                                    List<CcModRiverTypeDetail> tempRTDList = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == p37i.ProjectId).ToList();

                                    if (tempRTDList.Count > 0)
                                    {
                                        _db.CcModRiverTypeDetail.RemoveRange(tempRTDList);
                                    }
                                }

                                if (GroundWaterQualityDetail != null && GroundWaterQualityDetail.Count > 0)
                                {
                                    List<CcModGroundWaterQualityDetail> tempGWQList = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == p37i.ProjectId).ToList();

                                    if (tempGWQList.Count > 0)
                                    {
                                        _db.CcModGroundWaterQualityDetail.RemoveRange(tempGWQList);
                                    }
                                }

                                if (TypeOfWaterUseDetail != null && TypeOfWaterUseDetail.Count > 0)
                                {
                                    List<CcModTypeOfWaterUseDetail> tempTOWUDList = _db.CcModTypeOfWaterUseDetail.Where(w => w.ProjectId == p37i.ProjectId).ToList();

                                    if (tempTOWUDList.Count > 0)
                                    {
                                        _db.CcModTypeOfWaterUseDetail.RemoveRange(tempTOWUDList);
                                    }
                                }

                                if (WaterDiversSourceDetail != null && WaterDiversSourceDetail.Count > 0)
                                {
                                    List<CcModWaterDiversSourceDetail> tempWDSDList = _db.CcModWaterDiversSourceDetail.Where(w => w.ProjectId == p37i.ProjectId).ToList();

                                    if (tempWDSDList.Count > 0)
                                    {
                                        _db.CcModWaterDiversSourceDetail.RemoveRange(tempWDSDList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);

                                _db.CcModRiverTypeDetail.AddRange(RiverTypeDetail);
                                _db.CcModGroundWaterQualityDetail.AddRange(GroundWaterQualityDetail);
                                _db.CcModTypeOfWaterUseDetail.AddRange(TypeOfWaterUseDetail);
                                _db.CcModWaterDiversSourceDetail.AddRange(WaterDiversSourceDetail);
                                #endregion

                                #region Project 37 Indvidual Data Binding
                                p37i.DiscussAboutBaselineYesNoId = Project37Indv.DiscussAboutBaselineYesNoId;
                                p37i.DabApplicantComments = Project37Indv.DabApplicantComments;
                                p37i.DabAuthorityComments = Project37Indv.DabAuthorityComments;
                                p37i.WaterBodyId = Project37Indv.WaterBodyId;
                                p37i.WaterBodyTypeId = Project37Indv.WaterBodyTypeId;
                                p37i.Offtake = Project37Indv.Offtake;
                                p37i.Outfall = Project37Indv.Outfall;
                                p37i.Connectivity = Project37Indv.Connectivity;
                                p37i.WaterLevelDryMax = Project37Indv.WaterLevelDryMax;
                                p37i.WaterLevelDryMin = Project37Indv.WaterLevelDryMin;
                                p37i.WaterLevelWetMax = Project37Indv.WaterLevelWetMax;
                                p37i.WaterLevelWetMin = Project37Indv.WaterLevelWetMin;
                                p37i.DischargeDryMax = Project37Indv.DischargeDryMax;
                                p37i.DischargeDryMin = Project37Indv.DischargeDryMin;
                                p37i.DischargeWetMax = Project37Indv.DischargeWetMax;
                                p37i.DischargeWetMin = Project37Indv.DischargeWetMin;
                                p37i.CrossSectionDepth = Project37Indv.CrossSectionDepth;
                                p37i.CrossSectionWidth = Project37Indv.CrossSectionWidth;
                                p37i.WaterSalinity = Project37Indv.WaterSalinity;
                                p37i.WaterDO = Project37Indv.WaterDO;
                                p37i.WaterTDS = Project37Indv.WaterTDS;
                                p37i.WaterPhLevel = Project37Indv.WaterPhLevel;
                                p37i.LandCadastre = Project37Indv.LandCadastre;
                                p37i.ProofOfLandOwnership = Project37Indv.ProofOfLandOwnership;
                                p37i.RoofAreaOfBuilding = Project37Indv.RoofAreaOfBuilding;
                                p37i.RoadLength = Project37Indv.RoadLength;
                                p37i.GreenArea = Project37Indv.GreenArea;
                                p37i.OpenLandArea = Project37Indv.OpenLandArea;
                                p37i.AdjacentWetland = Project37Indv.AdjacentWetland;
                                p37i.MainManufactureProduct = Project37Indv.MainManufactureProduct;
                                p37i.ByProductItem = Project37Indv.ByProductItem;
                                p37i.WasteWaterGeneration = Project37Indv.WasteWaterGeneration;
                                p37i.WasteWaterDischarge = Project37Indv.WasteWaterDischarge;
                                p37i.DischargeCapacity = Project37Indv.DischargeCapacity;
                                p37i.CPLDLYesNoId = Project37Indv.CPLDLYesNoId;
                                p37i.ADEACYesNoId = Project37Indv.ADEACYesNoId;
                                p37i.IDSEOSYesNoId = Project37Indv.IDSEOSYesNoId;
                                p37i.WaterDemandDomestic = Project37Indv.WaterDemandDomestic;
                                p37i.WaterDemandIrrigation = Project37Indv.WaterDemandIrrigation;
                                p37i.WaterDemandOther = Project37Indv.WaterDemandOther;
                                p37i.WaterUseSectorId = Project37Indv.WaterUseSectorId;
                                p37i.StepsTakenWasteWaterTreatment = Project37Indv.StepsTakenWasteWaterTreatment;
                                p37i.TotalWasteWaterGeneration = Project37Indv.TotalWasteWaterGeneration;
                                p37i.UsableTreatedWaterQuantity = Project37Indv.UsableTreatedWaterQuantity;
                                p37i.WaterWithdrawQuantityPerDay = Project37Indv.WaterWithdrawQuantityPerDay;
                                p37i.UseOfFlowMeterMeasrYNId = Project37Indv.UseOfFlowMeterMeasrYNId;
                                p37i.NoOfPump = Project37Indv.NoOfPump;
                                p37i.PumpCapacity = Project37Indv.PumpCapacity;
                                p37i.PipeDiameter = Project37Indv.PipeDiameter;
                                p37i.DivertedWaterRtnSrcYNId = Project37Indv.DivertedWaterRtnSrcYNId;
                                p37i.DischargeQuantity = Project37Indv.DischargeQuantity;
                                p37i.DischargePoint = Project37Indv.DischargePoint;
                                #endregion

                                _db.Entry(p37i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project37Indv.Project37IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form37DeedObligatoryOneToOneSave :: f37otos        
        [HttpPost]
        public JsonResult f37dootos(Form37DeedObligatory _form37DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form37DeedObli.CommonDetail.ProjectId;
            Int64 Project37IndvId = _form37DeedObli.Project37Indv.Project37IndvId;

            try
            {
                if (_form37DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_37_IndvDetail p37id = _db.CcModAppProject_37_IndvDetail.Find(Project37IndvId); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p37id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form37DeedObli.CommonDetail;
                        CcModAppProject_37_IndvDetail Project37Indv = _form37DeedObli.Project37Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form37DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form37DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form37DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form37DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form37DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form37DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 37 Individual Data Binding
                        //p37id.DuplicatYesNoId = Project37Indv.DuplicatYesNoId;
                        //p37id.DuplicationApplicantComments = Project37Indv.DuplicationApplicantComments;
                        //p37id.DuplicationAuthorityComments = Project37Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        //_db.Entry(p37id).State = EntityState.Modified;

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
                                id = p37id.Project37IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.8 > River Bank Protection Project
        //Tehnical Info
        //form/Form38TechInfoOneToOneSave :: f38tiotos
        [HttpPost]
        public JsonResult f38tiotos(Form38TechInfo _form38TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form38TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form38TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form38TechInfo.CommonDetail;
                        CcModAppProject_38_IndvDetail Project38Indv = _form38TechInfo.Project38Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form38TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form38TechInfo.BDP2100HotSpot;
                        List<CcModRiverTypeDetail> RiverTypeDetail = _form38TechInfo.RiverTypeDetail;
                        #endregion

                        #region Common Detail Data Binding
                        //pcd.AnnualRainFallLast1Year = CommonDetail.AnnualRainFallLast1Year;
                        //pcd.AnnualRainFallLast2Years = CommonDetail.AnnualRainFallLast2Years;
                        //pcd.AnnualRainFallLast3Years = CommonDetail.AnnualRainFallLast3Years;
                        //pcd.AnnualRainFallLast4Years = CommonDetail.AnnualRainFallLast4Years;
                        //pcd.AnnualRainFallLast5Years = CommonDetail.AnnualRainFallLast5Years;
                        //pcd.IssueChallageProblem = CommonDetail.IssueChallageProblem;
                        pcd.YesNoStakeId = CommonDetail.YesNoStakeId;
                        pcd.DiscussWithStakeApplicantCmt = CommonDetail.DiscussWithStakeApplicantCmt;
                        pcd.DiscussWithStakeAuthorityCmt = CommonDetail.DiscussWithStakeAuthorityCmt;
                        pcd.DiscussWithStakePosFeedback = CommonDetail.DiscussWithStakePosFeedback;
                        pcd.DiscussWithStakeNegFeedback = CommonDetail.DiscussWithStakeNegFeedback;
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
                            CcModAppProject_38_IndvDetail p38i = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p38i == null)
                            {
                                _db.CcModAppProject_38_IndvDetail.Add(Project38Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (RiverTypeDetail != null && RiverTypeDetail.Count > 0)
                                {
                                    _db.CcModRiverTypeDetail.AddRange(RiverTypeDetail);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project38Indv.Project38IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p38i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p38i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (RiverTypeDetail != null && RiverTypeDetail.Count > 0)
                                {
                                    List<CcModRiverTypeDetail> rtList = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == p38i.ProjectId).ToList();

                                    if (rtList.Count > 0)
                                    {
                                        _db.CcModRiverTypeDetail.RemoveRange(rtList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                _db.CcModRiverTypeDetail.AddRange(RiverTypeDetail);
                                #endregion

                                #region Project 38 Indvidual Data Binding
                                p38i.NameOfRiverWaterCourse = Project38Indv.NameOfRiverWaterCourse;
                                p38i.RiverNatureId = Project38Indv.RiverNatureId;
                                p38i.BankErosionLength = Project38Indv.BankErosionLength;
                                p38i.BankErosionArea = Project38Indv.BankErosionArea;
                                p38i.BankErosionLocation = Project38Indv.BankErosionLocation;
                                p38i.BankErosionRate = Project38Indv.BankErosionRate;
                                p38i.SetBackDistanceImpStruct = Project38Indv.SetBackDistanceImpStruct;
                                p38i.SoilTypeRiverBank = Project38Indv.SoilTypeRiverBank;
                                p38i.SedimentationId = Project38Indv.SedimentationId;
                                p38i.CharArea = Project38Indv.CharArea;
                                p38i.CharLocation = Project38Indv.CharLocation;
                                p38i.IrrigatedCropArea = Project38Indv.IrrigatedCropArea;
                                p38i.CropProduction = Project38Indv.CropProduction;
                                p38i.FloraAndFauna = Project38Indv.FloraAndFauna;
                                p38i.SecLandLessPeople = Project38Indv.SecLandLessPeople;
                                p38i.SecSmallFarmer = Project38Indv.SecSmallFarmer;
                                p38i.SecAvgMonthlyIncome = Project38Indv.SecAvgMonthlyIncome;
                                p38i.RiverDepth = Project38Indv.RiverDepth;
                                p38i.RiverWidth = Project38Indv.RiverWidth;
                                p38i.SedimentationRate = Project38Indv.SedimentationRate;
                                p38i.WaterLevelDryMax = Project38Indv.WaterLevelDryMax;
                                p38i.WaterLevelDryMin = Project38Indv.WaterLevelDryMin;
                                p38i.WaterLevelWetMax = Project38Indv.WaterLevelWetMax;
                                p38i.WaterLevelWetMin = Project38Indv.WaterLevelWetMin;
                                p38i.DischargeDryMax = Project38Indv.DischargeDryMax;
                                p38i.DischargeDryMin = Project38Indv.DischargeDryMin;
                                p38i.DischargeWetMax = Project38Indv.DischargeWetMax;
                                p38i.DischargeWetMin = Project38Indv.DischargeWetMin;
                                p38i.BankLineShiftingId = Project38Indv.BankLineShiftingId;
                                p38i.BankStabilityTypeId = Project38Indv.BankStabilityTypeId;
                                p38i.UseOfToolsYesNoId = Project38Indv.UseOfToolsYesNoId;
                                p38i.ToolsApplicantComments = Project38Indv.ToolsApplicantComments;
                                p38i.ToolsAuthorityComments = Project38Indv.ToolsAuthorityComments;
                                #endregion

                                _db.Entry(p38i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project38Indv.Project38IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form38DeedObligatoryOneToOneSave :: f38dootos        
        [HttpPost]
        public JsonResult f38dootos(Form38DeedObligatory _form38DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form38DeedObli.CommonDetail.ProjectId;
            //Int64 Project38IndvId = _form38DeedObli.Project38Indv.Project38IndvId;

            try
            {
                if (_form38DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_38_IndvDetail p38id = _db.CcModAppProject_38_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault(); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p38id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form38DeedObli.CommonDetail;
                        //CcModAppProject_38_IndvDetail Project38Indv = _form38DeedObli.Project38Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form38DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form38DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form38DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form38DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form38DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form38DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 38 Individual Data Binding
                        //p38id.DuplicatYesNoId = Project38Indv.DuplicatYesNoId;
                        //p38id.DuplicationApplicantComments = Project38Indv.DuplicationApplicantComments;
                        //p38id.DuplicationAuthorityComments = Project38Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        //_db.Entry(p38id).State = EntityState.Modified;

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
                                id = p38id.Project38IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.10 > Excavation -Excavation of Khal Project
        //Tehnical Info
        //form/Form310TechInfoOneToOneSave :: f310tiotos
        [HttpPost]
        public JsonResult f310tiotos(Form310TechInfo _form310TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form310TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form310TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form310TechInfo.CommonDetail;
                        CcModAppProject_310_IndvDetail Project310Indv = _form310TechInfo.Project310Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form310TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form310TechInfo.BDP2100HotSpot;
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
                            CcModAppProject_310_IndvDetail p310i = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p310i == null)
                            {
                                _db.CcModAppProject_310_IndvDetail.Add(Project310Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project310Indv.Project310IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p310i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p310i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                #endregion

                                #region Project 310 Indvidual Data Binding
                                p310i.NameExcavatedKhal = Project310Indv.NameExcavatedKhal;
                                p310i.RiverNatureId = Project310Indv.RiverNatureId;
                                p310i.CatchmentArea = Project310Indv.CatchmentArea;
                                p310i.WaterLevelDryMax = Project310Indv.WaterLevelDryMax;
                                p310i.WaterLevelDryMin = Project310Indv.WaterLevelDryMin;
                                p310i.WaterLevelWetMax = Project310Indv.WaterLevelWetMax;
                                p310i.WaterLevelWetMin = Project310Indv.WaterLevelWetMin;
                                p310i.DischargeDryMax = Project310Indv.DischargeDryMax;
                                p310i.DischargeDryMin = Project310Indv.DischargeDryMin;
                                p310i.DischargeWetMax = Project310Indv.DischargeWetMax;
                                p310i.DischargeWetMin = Project310Indv.DischargeWetMin;
                                p310i.SedimentationId = Project310Indv.SedimentationId;
                                p310i.SedimentationRate = Project310Indv.SedimentationRate;
                                p310i.LengthExcavationWork = Project310Indv.LengthExcavationWork;
                                p310i.FishHabitatArea = Project310Indv.FishHabitatArea;
                                p310i.FishHabitatProduction = Project310Indv.FishHabitatProduction;
                                p310i.ExcavatedMaterialQuality = Project310Indv.ExcavatedMaterialQuality;
                                p310i.SpoilManagementPlan = Project310Indv.SpoilManagementPlan;
                                p310i.DrainageConditionId = Project310Indv.DrainageConditionId;
                                p310i.UseOfToolsYesNoId = Project310Indv.UseOfToolsYesNoId;
                                p310i.ToolsApplicantComments = Project310Indv.ToolsApplicantComments;
                                p310i.ToolsAuthorityComments = Project310Indv.ToolsAuthorityComments;
                                #endregion

                                _db.Entry(p310i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project310Indv.Project310IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form310DeedObligatoryOneToOneSave :: f310dootos        
        [HttpPost]
        public JsonResult f310dootos(Form310DeedObligatory _form310DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form310DeedObli.CommonDetail.ProjectId;
            //Int64 Project310IndvId = _form310DeedObli.Project310Indv.Project310IndvId;

            try
            {
                if (_form310DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_310_IndvDetail p310id = _db.CcModAppProject_310_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault(); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p310id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form310DeedObli.CommonDetail;
                        //CcModAppProject_310_IndvDetail Project310Indv = _form310DeedObli.Project310Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form310DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form310DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form310DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form310DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form310DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form310DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 310 Individual Data Binding
                        //p310id.DuplicatYesNoId = Project310Indv.DuplicatYesNoId;
                        //p310id.DuplicationApplicantComments = Project310Indv.DuplicationApplicantComments;
                        //p310id.DuplicationAuthorityComments = Project310Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        //_db.Entry(p310id).State = EntityState.Modified;

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
                                id = p310id.Project310IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.11 > Fisheries Project in Surface Water
        //Tehnical Info
        //form/Form311TechInfoOneToOneSave :: f311tiotos
        [HttpPost]
        public JsonResult f311tiotos(Form311TechInfo _form311TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form311TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form311TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form311TechInfo.CommonDetail;
                        CcModAppProject_311_IndvDetail Project311Indv = _form311TechInfo.Project311Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form311TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form311TechInfo.BDP2100HotSpot;
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
                            CcModAppProject_311_IndvDetail p311i = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p311i == null)
                            {
                                _db.CcModAppProject_311_IndvDetail.Add(Project311Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project311Indv.Project311IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p311i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p311i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);
                                #endregion

                                #region Project 311 Indvidual Data Binding
                                p311i.HighestFloodLevel = Project311Indv.HighestFloodLevel;
                                p311i.WaterDepthDrySeason = Project311Indv.WaterDepthDrySeason;
                                p311i.WaterDepthWetSeason = Project311Indv.WaterDepthWetSeason;
                                p311i.FishWaterAreaId = Project311Indv.FishWaterAreaId;
                                p311i.WaterSalinity = Project311Indv.WaterSalinity;
                                p311i.WaterDO = Project311Indv.WaterDO;
                                p311i.WaterTDS = Project311Indv.WaterTDS;
                                p311i.WaterPhLevel = Project311Indv.WaterPhLevel;
                                p311i.TypeFishSpecies = Project311Indv.TypeFishSpecies;
                                p311i.IntervalFishRelease = Project311Indv.IntervalFishRelease;
                                p311i.FishInspection = Project311Indv.FishInspection;
                                p311i.FishFeed = Project311Indv.FishFeed;
                                p311i.FishHabitatArea = Project311Indv.FishHabitatArea;
                                p311i.FishHabitatProduction = Project311Indv.FishHabitatProduction;
                                p311i.ExportFishQuantity = Project311Indv.ExportFishQuantity;
                                p311i.ExportFishValue = Project311Indv.ExportFishValue;
                                #endregion

                                _db.Entry(p311i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project311Indv.Project311IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form311DeedObligatoryOneToOneSave :: f311dootos        
        [HttpPost]
        public JsonResult f311dootos(Form311DeedObligatory _form311DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form311DeedObli.CommonDetail.ProjectId;
            //Int64 Project311IndvId = _form311DeedObli.Project311Indv.Project311IndvId;

            try
            {
                if (_form311DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_311_IndvDetail p311id = _db.CcModAppProject_311_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault(); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p311id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form311DeedObli.CommonDetail;
                        //CcModAppProject_311_IndvDetail Project311Indv = _form311DeedObli.Project311Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form311DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form311DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form311DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form311DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form311DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form311DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 311 Individual Data Binding
                        //p311id.DuplicatYesNoId = Project311Indv.DuplicatYesNoId;
                        //p311id.DuplicationApplicantComments = Project311Indv.DuplicationApplicantComments;
                        //p311id.DuplicationAuthorityComments = Project311Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        //_db.Entry(p311id).State = EntityState.Modified;

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
                                id = p311id.Project311IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.12 > Project for Ground Water Withdrawal, Distribution or Use
        //Tehnical Info
        //form/Form312TechInfoOneToOneSave :: f312tiotos
        [HttpPost]
        public JsonResult f312tiotos(Form312TechInfo _form312TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form312TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form312TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form312TechInfo.CommonDetail;
                        CcModAppProject_312_IndvDetail Project312Indv = _form312TechInfo.Project312Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form312TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form312TechInfo.BDP2100HotSpot;

                        List<CcModPurposeOfWaterUseDetail> PurposeOfWaterUseDetail = _form312TechInfo.PurposeOfWaterUseDetail;
                        List<CcModCategoryOfAquiferDetail> CategoryOfAquiferDetail = _form312TechInfo.CategoryOfAquiferDetail;
                        List<CcModGroundWaterQualityDetail> GroundWaterQualityDetail = _form312TechInfo.GroundWaterQualityDetail;
                        List<CcModSoilTypeDetail> SoilTypeDetail = _form312TechInfo.SoilTypeDetail;
                        #endregion

                        #region Common Detail Data Binding
                        pcd.AnnualRainFallLast1Year = CommonDetail.AnnualRainFallLast1Year;
                        pcd.AnnualRainFallLast2Years = CommonDetail.AnnualRainFallLast2Years;
                        pcd.AnnualRainFallLast3Years = CommonDetail.AnnualRainFallLast3Years;
                        pcd.AnnualRainFallLast4Years = CommonDetail.AnnualRainFallLast4Years;
                        pcd.AnnualRainFallLast5Years = CommonDetail.AnnualRainFallLast5Years;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        result = _db.SaveChanges();

                        if (result > 0)
                        {
                            CcModAppProject_312_IndvDetail p312i = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p312i == null)
                            {
                                _db.CcModAppProject_312_IndvDetail.Add(Project312Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (PurposeOfWaterUseDetail != null && PurposeOfWaterUseDetail.Count > 0)
                                {
                                    _db.CcModPurposeOfWaterUseDetail.AddRange(PurposeOfWaterUseDetail);
                                }

                                if (CategoryOfAquiferDetail != null && CategoryOfAquiferDetail.Count > 0)
                                {
                                    _db.CcModCategoryOfAquiferDetail.AddRange(CategoryOfAquiferDetail);
                                }

                                if (GroundWaterQualityDetail != null && GroundWaterQualityDetail.Count > 0)
                                {
                                    _db.CcModGroundWaterQualityDetail.AddRange(GroundWaterQualityDetail);
                                }

                                if (SoilTypeDetail != null && SoilTypeDetail.Count > 0)
                                {
                                    _db.CcModSoilTypeDetail.AddRange(SoilTypeDetail);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project312Indv.Project312IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p312i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p312i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (PurposeOfWaterUseDetail != null && PurposeOfWaterUseDetail.Count > 0)
                                {
                                    List<CcModPurposeOfWaterUseDetail> tempPWUList = _db.CcModPurposeOfWaterUseDetail.Where(w => w.ProjectId == p312i.ProjectId).ToList();

                                    if (tempPWUList.Count > 0)
                                    {
                                        _db.CcModPurposeOfWaterUseDetail.RemoveRange(tempPWUList);
                                    }
                                }

                                if (CategoryOfAquiferDetail != null && CategoryOfAquiferDetail.Count > 0)
                                {
                                    List<CcModCategoryOfAquiferDetail> tempCoAList = _db.CcModCategoryOfAquiferDetail.Where(w => w.ProjectId == p312i.ProjectId).ToList();

                                    if (tempCoAList.Count > 0)
                                    {
                                        _db.CcModCategoryOfAquiferDetail.RemoveRange(tempCoAList);
                                    }
                                }

                                if (GroundWaterQualityDetail != null && GroundWaterQualityDetail.Count > 0)
                                {
                                    List<CcModGroundWaterQualityDetail> tempGWQList = _db.CcModGroundWaterQualityDetail.Where(w => w.ProjectId == p312i.ProjectId).ToList();

                                    if (tempGWQList.Count > 0)
                                    {
                                        _db.CcModGroundWaterQualityDetail.RemoveRange(tempGWQList);
                                    }
                                }

                                if (SoilTypeDetail != null && SoilTypeDetail.Count > 0)
                                {
                                    List<CcModSoilTypeDetail> tempSTList = _db.CcModSoilTypeDetail.Where(w => w.ProjectId == p312i.ProjectId).ToList();

                                    if (tempSTList.Count > 0)
                                    {
                                        _db.CcModSoilTypeDetail.RemoveRange(tempSTList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);

                                _db.CcModPurposeOfWaterUseDetail.AddRange(PurposeOfWaterUseDetail);
                                _db.CcModCategoryOfAquiferDetail.AddRange(CategoryOfAquiferDetail);
                                _db.CcModGroundWaterQualityDetail.AddRange(GroundWaterQualityDetail);
                                _db.CcModSoilTypeDetail.AddRange(SoilTypeDetail);
                                #endregion

                                #region Project 312 Indvidual Data Binding
                                p312i.WellDescription = Project312Indv.WellDescription;
                                p312i.TypeProposedWellId = Project312Indv.TypeProposedWellId;
                                p312i.StatusOfAquifer = Project312Indv.StatusOfAquifer;
                                p312i.PotGrndWtrRechargeId = Project312Indv.PotGrndWtrRechargeId;
                                p312i.WaterWithdrawalTarget = Project312Indv.WaterWithdrawalTarget;
                                p312i.WaterWithdrawalProcedure = Project312Indv.WaterWithdrawalProcedure;
                                p312i.UseOfMeterToMeasure = Project312Indv.UseOfMeterToMeasure;
                                p312i.PumpCapacity = Project312Indv.PumpCapacity;
                                p312i.WellDepth = Project312Indv.WellDepth;
                                p312i.PipeDiameterOfWell = Project312Indv.PipeDiameterOfWell;
                                p312i.WaterWithdrawalQtyDay = Project312Indv.WaterWithdrawalQtyDay;
                                p312i.RechargeTime = Project312Indv.RechargeTime;
                                p312i.WaterUseConsumptionId = Project312Indv.WaterUseConsumptionId;
                                p312i.DischargePoint = Project312Indv.DischargePoint;
                                p312i.DistanceProposedExtractWell = Project312Indv.DistanceProposedExtractWell;
                                p312i.Place = Project312Indv.Place;
                                p312i.WellType = Project312Indv.WellType;
                                p312i.Capacity = Project312Indv.Capacity;
                                p312i.DiameterOfWell = Project312Indv.DiameterOfWell;
                                p312i.DepthOfWell = Project312Indv.DepthOfWell;
                                p312i.RecuperationHours = Project312Indv.RecuperationHours;
                                p312i.RiverKhalName = Project312Indv.RiverKhalName;
                                p312i.NearestSurfWaterAvailDistance = Project312Indv.NearestSurfWaterAvailDistance;
                                p312i.WaterLevel = Project312Indv.WaterLevel;
                                p312i.Discharge = Project312Indv.Discharge;
                                p312i.CommandAreaOfWell = Project312Indv.CommandAreaOfWell;
                                p312i.OtherAvailableSource = Project312Indv.OtherAvailableSource;
                                p312i.FutureAvailability = Project312Indv.FutureAvailability;
                                p312i.CultivableCrops = Project312Indv.CultivableCrops;
                                p312i.AnnualRequirementOfGW = Project312Indv.AnnualRequirementOfGW;
                                p312i.CropProduction = Project312Indv.CropProduction;
                                #endregion

                                _db.Entry(p312i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project312Indv.Project312IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form312DeedObligatoryOneToOneSave :: f312dootos        
        [HttpPost]
        public JsonResult f312dootos(Form312DeedObligatory _form312DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form312DeedObli.CommonDetail.ProjectId;
            //Int64 Project312IndvId = _form312DeedObli.Project312Indv.Project312IndvId;

            try
            {
                if (_form312DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_312_IndvDetail p312id = _db.CcModAppProject_312_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault(); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p312id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form312DeedObli.CommonDetail;
                        //CcModAppProject_312_IndvDetail Project312Indv = _form312DeedObli.Project312Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form312DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form312DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form312DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form312DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form312DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form312DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 312 Individual Data Binding
                        //p312id.DuplicatYesNoId = Project312Indv.DuplicatYesNoId;
                        //p312id.DuplicationApplicantComments = Project312Indv.DuplicationApplicantComments;
                        //p312id.DuplicationAuthorityComments = Project312Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        //_db.Entry(p312id).State = EntityState.Modified;

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
                                id = p312id.Project312IndvId.ToString(),
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
                            id = "",
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

        #region Form 3.13 > Special Project by DG
        //Tehnical Info
        //form/Form313TechInfoOneToOneSave :: f313tiotos
        [HttpPost]
        public JsonResult f313tiotos(Form313TechInfo _form313TechInfo)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form313TechInfo.CommonDetail.ProjectId;

            try
            {
                if (_form313TechInfo != null && ProjectId != 0)
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
                        CcModAppProjectCommonDetail CommonDetail = _form313TechInfo.CommonDetail;
                        CcModAppProject_313_IndvDetail Project313Indv = _form313TechInfo.Project313Indv;

                        List<CcModPrjHydroRegionDetail> HydroRegion = _form313TechInfo.HydroRegion;
                        List<CcModBDP2100HotSpotDetail> BDP2100HotSpot = _form313TechInfo.BDP2100HotSpot;

                        List<CcModRiverTypeDetail> RiverType = _form313TechInfo.RiverType;
                        List<CcModPrjTypesOfFloodDetail> TypesOfFlood = _form313TechInfo.TypesOfFlood;
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
                            CcModAppProject_313_IndvDetail p313i = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

                            if (p313i == null)
                            {
                                _db.CcModAppProject_313_IndvDetail.Add(Project313Indv);

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
                                        message = "Please select Bangladesh Delta Plan 2100 hot spot!"
                                    };

                                    goto Finish;
                                }

                                if (RiverType != null && RiverType.Count > 0)
                                {
                                    _db.CcModRiverTypeDetail.AddRange(RiverType);
                                }

                                if (TypesOfFlood != null && TypesOfFlood.Count > 0)
                                {
                                    _db.CcModPrjTypesOfFloodDetail.AddRange(TypesOfFlood);
                                }

                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project313Indv.Project313IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been save successfully."
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
                                if (HydroRegion != null && HydroRegion.Count > 0)
                                {
                                    List<CcModPrjHydroRegionDetail> tempHrList = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == p313i.ProjectId).ToList();

                                    if (tempHrList.Count > 0)
                                    {
                                        _db.CcModPrjHydroRegionDetail.RemoveRange(tempHrList);
                                    }
                                }

                                if (BDP2100HotSpot != null && BDP2100HotSpot.Count > 0)
                                {
                                    List<CcModBDP2100HotSpotDetail> tempHsList = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == p313i.ProjectId).ToList();

                                    if (tempHsList.Count > 0)
                                    {
                                        _db.CcModBDP2100HotSpotDetail.RemoveRange(tempHsList);
                                    }
                                }

                                if (RiverType != null && RiverType.Count > 0)
                                {
                                    List<CcModRiverTypeDetail> tempRTList = _db.CcModRiverTypeDetail.Where(w => w.ProjectId == p313i.ProjectId).ToList();

                                    if (tempRTList.Count > 0)
                                    {
                                        _db.CcModRiverTypeDetail.RemoveRange(tempRTList);
                                    }
                                }

                                if (TypesOfFlood != null && TypesOfFlood.Count > 0)
                                {
                                    List<CcModPrjTypesOfFloodDetail> tempToFList = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == p313i.ProjectId).ToList();

                                    if (tempToFList.Count > 0)
                                    {
                                        _db.CcModPrjTypesOfFloodDetail.RemoveRange(tempToFList);
                                    }
                                }

                                _db.CcModPrjHydroRegionDetail.AddRange(HydroRegion);
                                _db.CcModBDP2100HotSpotDetail.AddRange(BDP2100HotSpot);

                                _db.CcModRiverTypeDetail.AddRange(RiverType);
                                _db.CcModPrjTypesOfFloodDetail.AddRange(TypesOfFlood);
                                #endregion

                                #region Project 313 Indvidual Data Binding
                                p313i.ConnectivityAmidWaterland = Project313Indv.ConnectivityAmidWaterland;
                                p313i.CatchmentArea = Project313Indv.CatchmentArea;
                                p313i.WaterLevelDryMax = Project313Indv.WaterLevelDryMax;
                                p313i.WaterLevelDryMin = Project313Indv.WaterLevelDryMin;
                                p313i.WaterLevelWetMax = Project313Indv.WaterLevelWetMax;
                                p313i.WaterLevelWetMin = Project313Indv.WaterLevelWetMin;
                                p313i.DischargeDryMax = Project313Indv.DischargeDryMax;
                                p313i.DischargeDryMin = Project313Indv.DischargeDryMin;
                                p313i.DischargeWetMax = Project313Indv.DischargeWetMax;
                                p313i.DischargeWetMin = Project313Indv.DischargeWetMin;
                                p313i.BankErosionLength = Project313Indv.BankErosionLength;
                                p313i.BankErosionArea = Project313Indv.BankErosionArea;
                                p313i.BankErosionLocation = Project313Indv.BankErosionLocation;
                                p313i.BankErosionRate = Project313Indv.BankErosionRate;
                                p313i.BankStabilityTypeId = Project313Indv.BankStabilityTypeId;
                                p313i.CharAccretionLength = Project313Indv.CharAccretionLength;
                                p313i.CharAccretionArea = Project313Indv.CharAccretionArea;
                                p313i.CharAccretionLocation = Project313Indv.CharAccretionLocation;
                                p313i.SedimentationId = Project313Indv.SedimentationId;
                                p313i.SedimentationRate = Project313Indv.SedimentationRate;
                                p313i.KhalTypeId = Project313Indv.KhalTypeId;
                                p313i.DrainageConditionId = Project313Indv.DrainageConditionId;
                                p313i.HighLandPercent = Project313Indv.HighLandPercent;
                                p313i.MediumHighLandPercent = Project313Indv.MediumHighLandPercent;
                                p313i.MediumLowLandPercent = Project313Indv.MediumLowLandPercent;
                                p313i.LowLandPercent = Project313Indv.LowLandPercent;
                                p313i.VeryLowLandPercent = Project313Indv.VeryLowLandPercent;
                                p313i.CultivableCrops = Project313Indv.CultivableCrops;
                                p313i.CropProduction = Project313Indv.CropProduction;
                                p313i.FishProduction = Project313Indv.FishProduction;
                                p313i.FishDiversity = Project313Indv.FishDiversity;
                                p313i.FishMigration = Project313Indv.FishMigration;
                                p313i.FloraAndFauna = Project313Indv.FloraAndFauna;
                                #endregion

                                _db.Entry(p313i).State = EntityState.Modified;
                                result = _db.SaveChanges();

                                if (result > 0)
                                {
                                    dbContextTransaction.Commit();

                                    noti = new Notification
                                    {
                                        id = Project313Indv.Project313IndvId.ToString(),
                                        status = "success",
                                        message = "Technical information has been updated successfully."
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
                            id = "0",
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
        //form/Form313DeedObligatoryOneToOneSave :: f313dootos        
        [HttpPost]
        public JsonResult f313dootos(Form313DeedObligatory _form313DeedObli)
        {
            UserInfo ui = HttpContext.Session.GetComplexData<UserInfo>("LoggerUserInfo");
            using var dbContextTransaction = _db.Database.BeginTransaction();
            int result = 0;
            Int64 ProjectId = _form313DeedObli.CommonDetail.ProjectId;
            Int64 Project313IndvId = _form313DeedObli.Project313Indv.Project313IndvId;

            try
            {
                if (_form313DeedObli != null && ProjectId != 0)
                {
                    try
                    {
                        CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(ProjectId);
                        CcModAppProject_313_IndvDetail p313id = _db.CcModAppProject_313_IndvDetail.Where(w => w.ProjectId == ProjectId).FirstOrDefault(); //.Where(w => w.ProjectId == ProjectId).FirstOrDefault();

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

                        if (p313id == null)
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
                        CcModAppProjectCommonDetail CommonDetail = _form313DeedObli.CommonDetail;
                        CcModAppProject_313_IndvDetail Project313Indv = _form313DeedObli.Project313Indv;
                        List<CcModPrjCompatNWPDetail> CompatNWPDetail = _form313DeedObli.CompatNWPDetail;
                        List<CcModPrjCompatNWMPDetail> CompatNWMPDetail = _form313DeedObli.CompatNWMPDetail;
                        List<CcModPrjCompatSDGDetail> CompatSDGDetail = _form313DeedObli.CompatSDGDetail;
                        List<CcModPrjCompatSDGIndiDetail> CompatSDGIndiDetail = _form313DeedObli.CompatSDGIndiDetail;
                        List<CcModBDP2100GoalDetail> BDP2100GoalDetail = _form313DeedObli.BDP2100GoalDetail;
                        List<CcModGPWMGroupTypeDetail> GPWMGroupTypeDetail = _form313DeedObli.GPWMGroupTypeDetail;
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

                        #region Project 313 Individual Data Binding
                        p313id.DuplicatYesNoId = Project313Indv.DuplicatYesNoId;
                        p313id.DuplicationApplicantComments = Project313Indv.DuplicationApplicantComments;
                        p313id.DuplicationAuthorityComments = Project313Indv.DuplicationAuthorityComments;
                        #endregion

                        _db.Entry(pcd).State = EntityState.Modified;
                        _db.Entry(p313id).State = EntityState.Modified;

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
                                id = p313id.Project313IndvId.ToString(),
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
                            id = "",
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
                    CcModAppProjectCommonDetail pcd = _db.CcModAppProjectCommonDetail.Find(pda.ProjectId);
                    int ReviewCycleNo = pcd.ReviewCycleNo.Value;

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
                                                                                w.LabelNameOfControl == pda.LabelNameOfControl &&
                                                                                w.ReviewCycleNo == ReviewCycleNo)
                                                                     .FirstOrDefault();

                                if (exists != null)
                                {
                                    exists.Comments = pda.Comments;
                                    _db.Entry(exists).State = EntityState.Modified;
                                }
                                else
                                {
                                    pda.ReviewCycleNo = ReviewCycleNo;
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
            int CurrentAppState = 0, NextAppState = 0, CurrentIsCompletedState = 0, NextIsCompletedState = 0, result = 0;

            #region
            //if (!string.IsNullOrEmpty(uli.UnionGeoCode))
            //{
            //    userGroupList = _db.LookUpAdminModUserGroup.Where(w => w.UnionGeoCode == uli.UnionGeoCode).ToList();
            //}

            //if (string.IsNullOrEmpty(uli.UnionGeoCode) && !string.IsNullOrEmpty(uli.UpazilaGeoCode))
            //{
            //    userGroupList = _db.LookUpAdminModUserGroup.Where(w => w.UpazilaGeoCode == uli.UpazilaGeoCode).ToList();
            //}

            //if (string.IsNullOrEmpty(uli.UnionGeoCode) && string.IsNullOrEmpty(uli.UpazilaGeoCode) && !string.IsNullOrEmpty(uli.DistrictGeoCode))
            //{
            //    userGroupList = _db.LookUpAdminModUserGroup.Where(w => w.DistrictGeoCode == uli.DistrictGeoCode).ToList();
            //}

            //if (userGroupList.Count > 0)
            //{
            //    userGroupList = userGroupList.Where(w => w.AuthorityLevelId < uli.AuthorityLevelId).ToList();
            //    int maxAuthLevelId = userGroupList.Max(m => m.AuthorityLevelId).Value;

            //    LookUpAdminModUserGroup nextLevelInfo = userGroupList.Where(w => w.AuthorityLevelId == maxAuthLevelId).FirstOrDefault();
            //    appState = _db.LookUpCcModApplicationState.Where(w => w.AuthorityLevelId == nextLevelInfo.AuthorityLevelId).Select(s => s.ApplicationStateId).FirstOrDefault();
            //}
            #endregion

            CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(pid);

            if (_pcd != null)
            {
                CurrentAppState = _pcd.ApplicationStateId;
                CurrentIsCompletedState = _pcd.IsCompletedId.Value;

                if (CurrentIsCompletedState == 2)
                {
                    NextAppState = CurrentAppState - 1;
                    NextIsCompletedState = 3;
                }

                if (CurrentIsCompletedState == 3)
                {
                    NextAppState = CurrentAppState + 1;
                    NextIsCompletedState = 4;
                }

                if (CurrentIsCompletedState == 4)
                {
                    NextAppState = CurrentAppState - 2;
                    NextIsCompletedState = 5;
                }

                if (CurrentIsCompletedState == 5)
                {
                    NextAppState = CurrentAppState + 2;
                    NextIsCompletedState = 6;
                }
            }

            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                //CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(pid);

                if (_pcd != null)
                {
                    _pcd.ApplicationStateId = NextAppState;
                    _pcd.IsCompletedId = NextIsCompletedState;

                    _db.Entry(_pcd).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    string ForwardedToMsg = GetAppState(CurrentAppState, CurrentIsCompletedState);

                    if (result > 0)
                    {
                        dbContextTransaction.Commit();

                        noti = new Notification
                        {
                            id = pid.ToString(),
                            status = "success",
                            title = "Success",
                            message = "Application has been successfully forwarded to " + ForwardedToMsg + ". Application tracking code is: " + _pcd.AppSubmissionId
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

        //form/ApplicationRe-evaluateToTechnicalCommittee :: appreeva
        [HttpPost]
        public JsonResult appreevatotc(long pid)
        {
            int result = 0;
            string message = string.Empty;

            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(pid);
                int IsCompletedState = _pcd.IsCompletedId.Value;

                if (_pcd != null)
                {
                    _pcd.ApplicationStateId -= 1; //sending to TC
                    //_pcd.ApprovalStatusId = 2; //nees to discuss
                    _pcd.IsCompletedId = 3; // higher committ to tc

                    if (IsCompletedState == 6)
                    {
                        _pcd.ReviewCycleNo += 1;
                    }

                    _db.Entry(_pcd).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    if (result > 0)
                    {
                        dbContextTransaction.Commit();
                        message = "Application has been forwarded to Technical Committee for re-evaluation purpose.";

                        noti = new Notification
                        {
                            id = pid.ToString(),
                            status = "success",
                            title = "Success",
                            message = message + " Application tracking code is: " + _pcd.AppSubmissionId
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
                        message = "Application not forwarded to re-evaluate. Application tracking code is: " + _pcd.AppSubmissionId
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

        //form/ApplicationRe-evaluateToIWRMC :: appreevatoiwrmc
        [HttpPost]
        public JsonResult appreevatoiwrmc(long pid)
        {
            int result = 0;
            string message = string.Empty;

            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(pid);
                int IsCompletedState = _pcd.IsCompletedId.Value;

                if (_pcd != null)
                {
                    _pcd.ApplicationStateId -= 2; //sending to IWRMC
                    //_pcd.ApprovalStatusId = 2; //nees to discuss
                    _pcd.IsCompletedId = 5; // higher committ to IWRMC

                    if (IsCompletedState == 6)
                    {
                        _pcd.ReviewCycleNo += 1;
                    }

                    _db.Entry(_pcd).State = EntityState.Modified;
                    result = _db.SaveChanges();

                    if (result > 0)
                    {
                        dbContextTransaction.Commit();
                        message = "Application has been forwarded to Integrated Water Resource Management Committee for re-checking purpose.";

                        noti = new Notification
                        {
                            id = pid.ToString(),
                            status = "success",
                            title = "Success",
                            message = message + " Application tracking code is: " + _pcd.AppSubmissionId
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
                        message = "Application not forwarded to re-evaluate. Application tracking code is: " + _pcd.AppSubmissionId
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
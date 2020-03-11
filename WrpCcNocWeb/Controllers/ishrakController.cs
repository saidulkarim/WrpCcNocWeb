using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Helpers;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.TempModels;
using WrpCcNocWeb.Models.UserManagement;
using WrpCcNocWeb.Models.Utility;
using WrpCcNocWeb.ViewModels;
using static WrpCcNocWeb.Helpers.CommonHelper;

namespace WrpCcNocWeb.Controllers
{
    public class ishrakController : Controller
    {
        #region Initialization
        private readonly LoggedUserInfo iLoggedUser;
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        private readonly CommonHelper ch = new CommonHelper();
        private Notification noti = new Notification();
        #endregion       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult bangla()
        {
            long ProjectID = 8;
            CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(ProjectID);
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == ProjectID).FirstOrDefault();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_31_IndvDetail _indvdetail = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == ProjectID).FirstOrDefault();
            ViewBag.ProjectIndvDetail31 = _indvdetail;

            var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == ProjectID)
                                        .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).ToList();
            ViewBag.HydroRegionDetail = _hydroregiondetail;

            var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == ProjectID)
                                    .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).ToList();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

            var _typesofflood = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new { x.FloodTypeDetailId, x.ProjectId, x.FloodTypeId }).ToList();
            ViewBag.TypesOfFloodDetail = _typesofflood;

            var _compatnwpdetail = _db.CcModPrjCompatNWPDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new
                                   {
                                       x.PrjCompatNWPId,
                                       x.ProjectId,
                                       x.NationalWaterPolicyArticleId,
                                       x.LookUpCcModNWPArticle.NationalWtrPolicyShortTitle,
                                       x.LookUpCcModNWPArticle.NationalWtrPolicyArticleTitle,
                                       x.LookUpCcModNWPArticle.NWPArticleLink
                                   }).ToList();
            ViewBag.CompatNWPDetail = _compatnwpdetail;

            var _compatnwmpdetail = _db.CcModPrjCompatNWMPDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new { x.PrjCompatNWMPId, x.ProjectId, x.NWMPProgrammeId }).ToList();
            ViewBag.CompatNWMPDetail = _compatnwmpdetail;

            var _compatsdgdetail = _db.CcModPrjCompatSDGDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new { x.SDGCompabilityId, x.ProjectId, x.SDGGoalId }).ToList();
            ViewBag.CompatSDGDetail = _compatsdgdetail;

            var _compatsdgindidetail = _db.CcModPrjCompatSDGIndiDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new { x.SDGIndicatorDetailId, x.ProjectId, x.SDGIndicatorId }).ToList();
            ViewBag.CompatSDGIndiDetail = _compatsdgindidetail;

            var _bdp2100goaldetail = _db.CcModBDP2100GoalDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new { x.DeltaGoalDetailId, x.ProjectId, x.DeltPlan2100GoalId }).ToList();
            ViewBag.BDP2100GoalDetail = _bdp2100goaldetail;

            var _gpwmgrouptype = _db.CcModGPWMGroupTypeDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new { x.GPWMGroupTypeDetailId, x.ProjectId, x.GPWMGroupTypeId }).ToList();
            ViewBag.GPWMGroupType = _gpwmgrouptype;

            return View();
        }

        public IActionResult preview_form31()
        {
            long ProjectID = 48;

            CcModAppProjectCommonDetail _pcd = _db.CcModAppProjectCommonDetail.Find(ProjectID);
            ViewBag.ProjectCommonDetail = _pcd;
            CcModPrjLocationDetail _locationdetail = _db.CcModPrjLocationDetail.Where(w => w.ProjectId == ProjectID).FirstOrDefault();
            ViewBag.ProjectLocationDetail = _locationdetail;
            CcModAppProject_31_IndvDetail _indvdetail = _db.CcModAppProject_31_IndvDetail.Where(w => w.ProjectId == ProjectID).FirstOrDefault();
            ViewBag.ProjectIndvDetail31 = _indvdetail;

            var _hydroregiondetail = _db.CcModPrjHydroRegionDetail.Where(w => w.ProjectId == ProjectID)
                                        .Select(x => new { x.PrjHydroRegionDetailId, x.ProjectId, x.HydroRegionId }).OrderBy(o => o.HydroRegionId).ToList();
            ViewBag.HydroRegionDetail = _hydroregiondetail;

            var _hotspotdetail = _db.CcModBDP2100HotSpotDetail.Where(w => w.ProjectId == ProjectID)
                                    .Select(x => new { x.HotSpotDetailId, x.ProjectId, x.DeltaPlanHotSpotId }).OrderBy(o => o.DeltaPlanHotSpotId).ToList();
            ViewBag.BDP2100HotSpotDetail = _hotspotdetail;

            var _typesofflood = _db.CcModPrjTypesOfFloodDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new { x.FloodTypeDetailId, x.ProjectId, x.FloodTypeId }).OrderBy(o => o.FloodTypeId).ToList();
            ViewBag.TypesOfFloodDetail = _typesofflood;

            var _compatnwpdetail = _db.CcModPrjCompatNWPDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new
                                   {
                                       x.PrjCompatNWPId,
                                       x.ProjectId,
                                       x.NationalWaterPolicyArticleId,
                                       x.LookUpCcModNWPArticle.NationalWtrPolicyShortTitle,
                                       x.LookUpCcModNWPArticle.NationalWtrPolicyArticleTitle,
                                       x.LookUpCcModNWPArticle.NWPArticleLink
                                   }).OrderBy(o => o.NationalWaterPolicyArticleId).ToList();
            ViewBag.CompatNWPDetail = _compatnwpdetail;

            var _compatnwmpdetail = _db.CcModPrjCompatNWMPDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new
                                   {
                                       x.PrjCompatNWMPId,
                                       x.ProjectId,
                                       x.NWMPProgrammeId,
                                       x.LookUpCcModNWMPProgramme.NWMPProgrammeTitle
                                   }).OrderBy(o => o.NWMPProgrammeId).ToList();
            ViewBag.CompatNWMPDetail = _compatnwmpdetail;

            var _compatsdgdetail = _db.CcModPrjCompatSDGDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new
                                   {
                                       x.SDGCompabilityId,
                                       x.ProjectId,
                                       x.SDGGoalId,
                                       x.LookUpCcModSDGGoal.SDGGoalNumber,
                                       x.LookUpCcModSDGGoal.SDGDocLink
                                   }).OrderBy(o => o.SDGGoalId).ToList();
            ViewBag.CompatSDGDetail = _compatsdgdetail;

            var _compatsdgindidetail = _db.CcModPrjCompatSDGIndiDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new
                                   {
                                       x.SDGIndicatorDetailId,
                                       x.ProjectId,
                                       x.SDGIndicatorId,
                                       x.LookUpCcModSDGIndicator.SDGIndicatorName
                                   }).OrderBy(o => o.SDGIndicatorId).ToList();
            ViewBag.CompatSDGIndiDetail = _compatsdgindidetail;

            var _bdp2100goaldetail = _db.CcModBDP2100GoalDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new
                                   {
                                       x.DeltaGoalDetailId,
                                       x.ProjectId,
                                       x.DeltPlan2100GoalId,
                                       x.LookUpCcModDeltPlan2100Goal.DeltPlan2100Goal
                                   }).OrderBy(o => o.DeltPlan2100GoalId).ToList();
            ViewBag.BDP2100GoalDetail = _bdp2100goaldetail;

            var _gpwmgrouptype = _db.CcModGPWMGroupTypeDetail.Where(w => w.ProjectId == ProjectID)
                                   .Select(x => new
                                   {
                                       x.GPWMGroupTypeDetailId,
                                       x.ProjectId,
                                       x.GPWMGroupTypeId,
                                       x.LookUpCcModGPWMGroupType.GPWMGroupTypeName
                                   }).OrderBy(o => o.GPWMGroupTypeId).ToList();
            ViewBag.GPWMGroupType = _gpwmgrouptype;

            return View();
        }
        public IActionResult form32()
        {
            #region Dropdown Data Loading
            ViewBag.LookUpAdminBndDistrict = _db.LookUpAdminBndDistrict.ToList();
            ViewBag.LookUpAdminBndUpazila = _db.LookUpAdminBndUpazila.ToList();
            ViewBag.LookUpAdminBndUnion = _db.LookUpAdminBndUnion.ToList();

            ViewBag.LookUpCcModHydroRegion = _db.LookUpCcModHydroRegion.ToList();
            ViewBag.LookUpCcModDeltPlan2100HotSpot = _db.LookUpCcModDeltPlan2100HotSpot.ToList();
            ViewBag.LookUpCcModHydroSystem = _db.LookUpCcModHydroSystem.ToList();

            ViewBag.LookUpCcModTypeOfFlood = _db.LookUpCcModTypeOfFlood.ToList();
            ViewBag.LookUpCcModFloodFrequency = _db.LookUpCcModFloodFrequency.ToList();
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
            #endregion

            return View();
        }
    }
}
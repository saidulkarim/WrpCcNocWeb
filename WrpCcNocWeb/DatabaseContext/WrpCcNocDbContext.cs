using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.AdminModule;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.DatabaseContext
{
    public class WrpCcNocDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseOracle(@"User Id=ARH; Password=cegis; Data Source=130.180.3.155:1521/WRPCCNOC");
            optionsBuilder.UseOracle(@"User Id=ARH; Password=cegis; Data Source=202.53.173.179:1521/WRPCCNOC",
                options => options.UseOracleSQLCompatibility("11"));
        }

        #region Detail --- Admin  Module
        public DbSet<AdminModErrorLogDetail> AdminModErrorLogDetail { get; set; }
        public DbSet<AdminModEventLogDetail> AdminModEventLogDetail { get; set; }
        public DbSet<AdminModUserGrpDistDetail> AdminModUserGrpDistDetail { get; set; }
        public DbSet<AdminModUserGrpWiseMenuDetail> AdminModUserGrpWiseMenuDetail { get; set; }
        public DbSet<AdminModUserLogHistoryDetail> AdminModUserLogHistoryDetail { get; set; }
        public DbSet<AdminModUserRegistrationDetail> AdminModUserRegistrationDetail { get; set; }
        public DbSet<AdminModUsersDetail> AdminModUsersDetail { get; set; }
        public DbSet<LookUpAdminModCostRange> LookUpAdminModCostRange { get; set; }
        public DbSet<CcModAppProjDataAnalysis> CcModAppProjDataAnalysis { get; set; }
        #endregion

        #region LookUp --- Admin Module

        public DbSet<LookUpAdminBndDistrict> LookUpAdminBndDistrict { get; set; }
        public DbSet<LookUpAdminBndUpazila> LookUpAdminBndUpazila { get; set; }
        public DbSet<LookUpAdminBndUnion> LookUpAdminBndUnion { get; set; }
        public DbSet<LookUpAdminModAuthorityLevel> LookUpAdminModAuthorityLevel { get; set; }
        public DbSet<LookUpAdminModErrorSlvStatus> LookUpAdminModErrorSlvStatus { get; set; }
        public DbSet<LookUpAdminModMenu> LookUpAdminModMenu { get; set; }
        public DbSet<LookUpAdminModSecurityQuestion> LookUpAdminModSecurityQuestion { get; set; }
        public DbSet<LookUpAdminModSubMenu> LookUpAdminModSubMenu { get; set; }
        public DbSet<LookUpAdminModUserGroup> LookUpAdminModUserGroup { get; set; }
        #endregion

        #region Detail --- CC Module        
        public DbSet<CcModAnalyzeOptionsDetail> CcModAnalyzeOptionsDetail { get; set; }
        public DbSet<CcModAppProject_31_IndvDetail> CcModAppProject_31_IndvDetail { get; set; }
        public DbSet<CcModAppProjectCommonDetail> CcModAppProjectCommonDetail { get; set; }
        public DbSet<CcModBDP2100GoalDetail> CcModBDP2100GoalDetail { get; set; }
        public DbSet<CcModBDP2100HotSpotDetail> CcModBDP2100HotSpotDetail { get; set; }
        public DbSet<CcModDesignSubmitDetail> CcModDesignSubmitDetail { get; set; }
        public DbSet<CcModFloodFrequencyDetail> CcModFloodFrequencyDetail { get; set; }
        public DbSet<CcModGPWMGroupTypeDetail> CcModGPWMGroupTypeDetail { get; set; }
        public DbSet<CcModHydroSystemDetail> CcModHydroSystemDetail { get; set; }
        public DbSet<CcModPrjCompatNWMPDetail> CcModPrjCompatNWMPDetail { get; set; }
        public DbSet<CcModPrjCompatNWPDetail> CcModPrjCompatNWPDetail { get; set; }
        public DbSet<CcModPrjCompatSDGDetail> CcModPrjCompatSDGDetail { get; set; }
        public DbSet<CcModPrjCompatSDGIndiDetail> CcModPrjCompatSDGIndiDetail { get; set; }
        public DbSet<CcModPrjEcoFinAnalysisDetail> CcModPrjEcoFinAnalysisDetail { get; set; }
        public DbSet<CcModPrjEIADetail> CcModPrjEIADetail { get; set; }
        public DbSet<CcModPrjHydroRegionDetail> CcModPrjHydroRegionDetail { get; set; }
        public DbSet<CcModPrjIrrigCropAreaDetail> CcModPrjIrrigCropAreaDetail { get; set; }
        public DbSet<CcModPrjLocationDetail> CcModPrjLocationDetail { get; set; }
        public DbSet<CcModPrjSIADetail> CcModPrjSIADetail { get; set; }
        public DbSet<CcModPrjTypesOfFloodDetail> CcModPrjTypesOfFloodDetail { get; set; }
        #endregion

        #region LookUp --- CC Module          
        public DbSet<LookUpCcModApplicationState> LookUpCcModApplicationState { get; set; }
        public DbSet<LookUpCcModApprovalStatus> LookUpCcModApprovalStatus { get; set; }
        public DbSet<LookUpCcModBankDocType> LookUpCcModBankDocType { get; set; }
        public DbSet<LookUpCcModBankLineShifting> LookUpCcModBankLineShifting { get; set; }
        public DbSet<LookUpCcModBankStability> LookUpCcModBankStability { get; set; }
        public DbSet<LookUpCcModConservLocation> LookUpCcModConservLocation { get; set; }
        public DbSet<LookUpCcModDeltPlan2100Goal> LookUpCcModDeltPlan2100Goal { get; set; }
        public DbSet<LookUpCcModDeltPlan2100HotSpot> LookUpCcModDeltPlan2100HotSpot { get; set; }
        public DbSet<LookUpCcModDesignSubmitParam> LookUpCcModDesignSubmitParam { get; set; }
        public DbSet<LookUpCcModDrainageCondition> LookUpCcModDrainageCondition { get; set; }
        public DbSet<LookUpCcModEcoAndFinancial> LookUpCcModEcoAndFinancial { get; set; }
        public DbSet<LookUpCcModEIAParameter> LookUpCcModEIAParameter { get; set; }
        public DbSet<LookUpCcModFloodFrequency> LookUpCcModFloodFrequency { get; set; }
        public DbSet<LookUpCcModGPWMGroupType> LookUpCcModGPWMGroupType { get; set; }
        public DbSet<LookUpCcModGroundWaterQuality> LookUpCcModGroundWaterQuality { get; set; }
        public DbSet<LookUpCcModHydroRegion> LookUpCcModHydroRegion { get; set; }
        public DbSet<LookUpCcModHydroSystem> LookUpCcModHydroSystem { get; set; }
        public DbSet<LookUpCcModKhalType> LookUpCcModKhalType { get; set; }
        public DbSet<LookUpCcModLandType> LookUpCcModLandType { get; set; }
        public DbSet<LookUpCcModNameOfWaterBody> LookUpCcModNameOfWaterBody { get; set; }
        public DbSet<LookUpCcModNOCType> LookUpCcModNOCType { get; set; }
        public DbSet<LookUpCcModNWMPProgramme> LookUpCcModNWMPProgramme { get; set; }
        public DbSet<LookUpCcModNWPArticle> LookUpCcModNWPArticle { get; set; }
        public DbSet<LookUpCcModPotGrndWtrRecharge> LookUpCcModPotGrndWtrRecharge { get; set; }
        public DbSet<LookUpCcModProjectType> LookUpCcModProjectType { get; set; }
        public DbSet<LookUpCcModProposedWaterUse> LookUpCcModProposedWaterUse { get; set; }
        public DbSet<LookUpCcModPurposeOfWaterUse> LookUpCcModPurposeOfWaterUse { get; set; }
        public DbSet<LookUpCcModRecommendation> LookUpCcModRecommendation { get; set; }
        public DbSet<LookUpCcModRiverNature> LookUpCcModRiverNature { get; set; }
        public DbSet<LookUpCcModRiverType> LookUpCcModRiverType { get; set; }
        public DbSet<LookUpCcModRockType> LookUpCcModRockType { get; set; }
        public DbSet<LookUpCcModSDGGoal> LookUpCcModSDGGoal { get; set; }
        public DbSet<LookUpCcModSDGIndicator> LookUpCcModSDGIndicator { get; set; }
        public DbSet<LookUpCcModSediOfRiverOrKhal> LookUpCcModSediOfRiverOrKhal { get; set; }
        public DbSet<LookUpCcModSIAParameter> LookUpCcModSIAParameter { get; set; }
        public DbSet<LookUpCcModStructTypeConserv> LookUpCcModStructTypeConserv { get; set; }
        public DbSet<LookUpCcModSurfWaterQuality> LookUpCcModSurfWaterQuality { get; set; }
        public DbSet<LookUpCcModTypeOfFlood> LookUpCcModTypeOfFlood { get; set; }
        public DbSet<LookUpCcModTypeOfWaterBody> LookUpCcModTypeOfWaterBody { get; set; }
        public DbSet<LookUpCcModTypeOfWaterUse> LookUpCcModTypeOfWaterUse { get; set; }
        public DbSet<LookUpCcModWaterUse> LookUpCcModWaterUse { get; set; }
        public DbSet<LookUpCcModWtrDiversionSource> LookUpCcModWtrDiversionSource { get; set; }
        public DbSet<LookUpCcModYesNo> LookUpCcModYesNo { get; set; }
        #endregion
    }
}
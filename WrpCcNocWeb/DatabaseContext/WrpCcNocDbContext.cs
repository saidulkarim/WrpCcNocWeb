using Microsoft.EntityFrameworkCore;
using WrpCcNocWeb.Models;
using WrpCcNocWeb.Models.AdminModule;
using WrpCcNocWeb.Models.CcModule;
using WrpCcNocWeb.Models.NocModule;
using WrpCcNocWeb.Models.NocModule.Lookup;

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

        #endregion

        #region LookUp --- Admin Module        
        public DbSet<LookUpAdminModAuthorityLevel> LookUpAdminModAuthorityLevel { get; set; }
        public DbSet<LookUpAdminModCostRange> LookUpAdminModCostRange { get; set; }
        public DbSet<LookUpAdminModEmailFormat> LookUpAdminModEmailFormat { get; set; }
        public DbSet<LookUpAdminModErrorSlvStatus> LookUpAdminModErrorSlvStatus { get; set; }
        public DbSet<LookUpAdminModLanguage> LookUpAdminModLanguage { get; set; }
        public DbSet<LookUpAdminModMenu> LookUpAdminModMenu { get; set; }
        public DbSet<LookUpAdminModSecurityQuestion> LookUpAdminModSecurityQuestion { get; set; }
        public DbSet<LookUpAdminModSubMenu> LookUpAdminModSubMenu { get; set; }
        public DbSet<LookUpAdminModUrlType> LookUpAdminModUrlType { get; set; }
        public DbSet<LookUpAdminModUrl> LookUpAdminModUrl { get; set; }
        public DbSet<LookUpAdminModUserGroup> LookUpAdminModUserGroup { get; set; }

        public DbSet<LookUpAdminModSmsFormat> LookUpAdminModSmsFormat { get; set; }
        public DbSet<LookUpAdminModSmsHistory> LookUpAdminModSmsHistory { get; set; }
        public DbSet<LookUpCcModApplicantType> LookUpCcModApplicantType { get; set; }
        #endregion

        #region Detail --- CC Module        
        public DbSet<CcModAnalyzeOptionsDetail> CcModAnalyzeOptionsDetail { get; set; }
        public DbSet<CcModAppProjDataAnalysis> CcModAppProjDataAnalysis { get; set; }
        public DbSet<CcModAppProject_31_IndvDetail> CcModAppProject_31_IndvDetail { get; set; }
        public DbSet<CcModAppProject_310_IndvDetail> CcModAppProject_310_IndvDetail { get; set; }
        public DbSet<CcModAppProject_311_IndvDetail> CcModAppProject_311_IndvDetail { get; set; }
        public DbSet<CcModAppProject_312_IndvDetail> CcModAppProject_312_IndvDetail { get; set; }
        public DbSet<CcModAppProject_313_IndvDetail> CcModAppProject_313_IndvDetail { get; set; }
        public DbSet<CcModAppProject_32_IndvDetail> CcModAppProject_32_IndvDetail { get; set; }
        public DbSet<CcModAppProject_33_IndvDetail> CcModAppProject_33_IndvDetail { get; set; }
        public DbSet<CcModAppProject_34_IndvDetail> CcModAppProject_34_IndvDetail { get; set; }
        public DbSet<CcModAppProject_35_IndvDetail> CcModAppProject_35_IndvDetail { get; set; }
        public DbSet<CcModAppProject_36_IndvDetail> CcModAppProject_36_IndvDetail { get; set; }
        public DbSet<CcModAppProject_37_IndvDetail> CcModAppProject_37_IndvDetail { get; set; }
        public DbSet<CcModAppProject_38_IndvDetail> CcModAppProject_38_IndvDetail { get; set; }
        public DbSet<CcModAppProject_39_IndvDetail> CcModAppProject_39_IndvDetail { get; set; }
        public DbSet<CcModAppProjectCommonDetail> CcModAppProjectCommonDetail { get; set; }
        public DbSet<CcModBDP2100GoalDetail> CcModBDP2100GoalDetail { get; set; }
        public DbSet<CcModBDP2100HotSpotDetail> CcModBDP2100HotSpotDetail { get; set; }
        public DbSet<CcModCategoryOfAquiferDetail> CcModCategoryOfAquiferDetail { get; set; }
        public DbSet<CcModConservLocationDetail> CcModConservLocationDetail { get; set; }
        public DbSet<CcModDesignSubmitDetail> CcModDesignSubmitDetail { get; set; }
        public DbSet<CcModFloodFrequencyDetail> CcModFloodFrequencyDetail { get; set; }
        public DbSet<CcModGPWMGroupTypeDetail> CcModGPWMGroupTypeDetail { get; set; }
        public DbSet<CcModGroundWaterQualityDetail> CcModGroundWaterQualityDetail { get; set; }
        public DbSet<CcModGroundWaterWithdrawDetail> CcModGroundWaterWithdrawDetail { get; set; }
        public DbSet<CcModHydraInfraParamDetail> CcModHydraInfraParamDetail { get; set; }
        public DbSet<CcModHydroSystemDetail> CcModHydroSystemDetail { get; set; }
        public DbSet<CcModNavigationClassDetail> CcModNavigationClassDetail { get; set; }
        public DbSet<CcModPrjCompatNWMPDetail> CcModPrjCompatNWMPDetail { get; set; }
        public DbSet<CcModPrjCompatNWPDetail> CcModPrjCompatNWPDetail { get; set; }
        public DbSet<CcModPrjCompatSDGDetail> CcModPrjCompatSDGDetail { get; set; }
        public DbSet<CcModPrjCompatSDGIndiDetail> CcModPrjCompatSDGIndiDetail { get; set; }
        public DbSet<CcModPrjEcoFinAnalysisDetail> CcModPrjEcoFinAnalysisDetail { get; set; }
        public DbSet<CcModPrjEIADetail> CcModPrjEIADetail { get; set; }
        public DbSet<CcModPrjGrndWtrDepthDetail> CcModPrjGrndWtrDepthDetail { get; set; }
        public DbSet<CcModPrjHydroRegionDetail> CcModPrjHydroRegionDetail { get; set; }
        public DbSet<CcModPrjIrrigCropAreaDetail> CcModPrjIrrigCropAreaDetail { get; set; }
        public DbSet<CcModPrjLocationDetail> CcModPrjLocationDetail { get; set; }
        public DbSet<CcModPrjSIADetail> CcModPrjSIADetail { get; set; }
        public DbSet<CcModPrjSWADetail> CcModPrjSWADetail { get; set; }
        public DbSet<CcModPrjTypesOfFloodDetail> CcModPrjTypesOfFloodDetail { get; set; }
        public DbSet<CcModProposedWaterUseDetail> CcModProposedWaterUseDetail { get; set; }
        public DbSet<CcModPurposeOfWaterUseDetail> CcModPurposeOfWaterUseDetail { get; set; }
        public DbSet<CcModRiverTypeDetail> CcModRiverTypeDetail { get; set; }
        public DbSet<CcModSoilTypeDetail> CcModSoilTypeDetail { get; set; }
        public DbSet<CcModStructTypeConservDetail> CcModStructTypeConservDetail { get; set; }
        public DbSet<CcModTypeOfWaterUseDetail> CcModTypeOfWaterUseDetail { get; set; }
        public DbSet<CcModUsDsConditionDetail> CcModUsDsConditionDetail { get; set; }
        public DbSet<CcModWaterDiversSourceDetail> CcModWaterDiversSourceDetail { get; set; }
        public DbSet<CcModWaterUseDetail> CcModWaterUseDetail { get; set; }

        //added by rony
        public DbSet<CcModTypeOfDredgingPlanDetail> CcModTypeOfDredgingPlanDetail { get; set; }
        public DbSet<CcModAppAdditionalAttachment> CcModAppAdditionalAttachment { get; set; }
        public DbSet<CcModAppHearingAttachment> CcModAppHearingAttachment { get; set; }
        public DbSet<CcModAppPrjLocationFiles> CcModAppPrjLocationFiles { get; set; }
        public DbSet<CcModProjectQueryDetail> CcModProjectQueryDetail { get; set; }
        public DbSet<CcModAppProjHearing> CcModAppProjHearing { get; set; }
        public DbSet<CcModAnnualRainfallDetail> CcModAnnualRainfallDetail { get; set; }
        public DbSet<CcModHighestFloodLevelDetail> CcModHighestFloodLevelDetail { get; set; }
        public DbSet<CcModMaxDischargeDetail> CcModMaxDischargeDetail { get; set; }
        public DbSet<CcModFishProdDiversityDetail> CcModFishProdDiversityDetail { get; set; }
        public DbSet<CcModSubDesignSubmitDetail> CcModSubDesignSubmitDetail { get; set; }
        public DbSet<CcModDownloadCertificateHist> CcModDownloadCertificateHist { get; set; }
        #endregion

        #region LookUp --- CC Module
        public DbSet<LookUpAdminBndDistrict> LookUpAdminBndDistrict { get; set; }
        public DbSet<LookUpAdminBndUnion> LookUpAdminBndUnion { get; set; }
        public DbSet<LookUpAdminBndUpazila> LookUpAdminBndUpazila { get; set; }
        public DbSet<LookUpCcModApplicationState> LookUpCcModApplicationState { get; set; }
        public DbSet<LookUpCcModApprovalStatus> LookUpCcModApprovalStatus { get; set; }
        public DbSet<LookUpCcModBankLineShifting> LookUpCcModBankLineShifting { get; set; }
        public DbSet<LookUpCcModBankStability> LookUpCcModBankStability { get; set; }
        public DbSet<LookUpCcModCategoryOfAquifer> LookUpCcModCategoryOfAquifer { get; set; }
        public DbSet<LookUpCcModCertificateFormat> LookUpCcModCertificateFormat { get; set; }
        public DbSet<LookUpCcModConservLocation> LookUpCcModConservLocation { get; set; }
        public DbSet<LookUpCcModDeltPlan2100Goal> LookUpCcModDeltPlan2100Goal { get; set; }
        public DbSet<LookUpCcModDeltPlan2100HotSpot> LookUpCcModDeltPlan2100HotSpot { get; set; }
        public DbSet<LookUpCcModDesignSubmitParam> LookUpCcModDesignSubmitParam { get; set; }
        public DbSet<LookUpCcModDrainageCondition> LookUpCcModDrainageCondition { get; set; }
        public DbSet<LookUpCcModEcoAndFinancial> LookUpCcModEcoAndFinancial { get; set; }
        public DbSet<LookUpCcModEIAParameter> LookUpCcModEIAParameter { get; set; }
        public DbSet<LookUpCcModFishWaterArea> LookUpCcModFishWaterArea { get; set; }
        public DbSet<LookUpCcModFloodFrequency> LookUpCcModFloodFrequency { get; set; }
        public DbSet<LookUpCcModGPWMGroupType> LookUpCcModGPWMGroupType { get; set; }
        public DbSet<LookUpCcModGrndWtrWthdrwParam> LookUpCcModGrndWtrWthdrwParam { get; set; }
        public DbSet<LookUpCcModGroundWaterQuality> LookUpCcModGroundWaterQuality { get; set; }
        public DbSet<LookUpCcModHydraInfraParam> LookUpCcModHydraInfraParam { get; set; }
        public DbSet<LookUpCcModHydroRegion> LookUpCcModHydroRegion { get; set; }
        public DbSet<LookUpCcModHydroSystem> LookUpCcModHydroSystem { get; set; }
        public DbSet<LookUpCcModIsCompletedState> LookUpCcModIsCompletedState { get; set; }
        public DbSet<LookUpCcModKhalType> LookUpCcModKhalType { get; set; }
        public DbSet<LookUpCcModLandType> LookUpCcModLandType { get; set; }
        public DbSet<LookUpCcModMonth> LookUpCcModMonth { get; set; }
        public DbSet<LookUpCcModNameOfWaterBody> LookUpCcModNameOfWaterBody { get; set; }
        public DbSet<LookUpCcModNavigationClass> LookUpCcModNavigationClass { get; set; }
        public DbSet<LookUpCcModNocType> LookUpCcModNocType { get; set; }
        public DbSet<LookUpCcModNWMPProgramme> LookUpCcModNWMPProgramme { get; set; }
        public DbSet<LookUpCcModNWPArticle> LookUpCcModNWPArticle { get; set; }
        public DbSet<LookUpCcModPaymentMethod> LookUpCcModPaymentMethod { get; set; }
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
        public DbSet<LookUpCcModSoilType> LookUpCcModSoilType { get; set; }
        public DbSet<LookUpCcModStructTypeConserv> LookUpCcModStructTypeConserv { get; set; }
        public DbSet<LookUpCcModSurfWaterQuality> LookUpCcModSurfWaterQuality { get; set; }
        public DbSet<LookUpCcModTypeOfFlood> LookUpCcModTypeOfFlood { get; set; }
        public DbSet<LookUpCcModTypeOfWaterBody> LookUpCcModTypeOfWaterBody { get; set; }
        public DbSet<LookUpCcModTypeOfWaterUse> LookUpCcModTypeOfWaterUse { get; set; }
        public DbSet<LookUpCcModTypeProposedWell> LookUpCcModTypeProposedWell { get; set; }
        public DbSet<LookUpCcModUsDsCondition> LookUpCcModUsDsCondition { get; set; }
        public DbSet<LookUpCcModWaterBody> LookUpCcModWaterBody { get; set; }
        public DbSet<LookUpCcModWaterUse> LookUpCcModWaterUse { get; set; }
        public DbSet<LookUpCcModWaterUseConsumption> LookUpCcModWaterUseConsumption { get; set; }
        public DbSet<LookUpCcModWaterUseSector> LookUpCcModWaterUseSector { get; set; }
        public DbSet<LookUpCcModWtrDiversionSource> LookUpCcModWtrDiversionSource { get; set; }
        public DbSet<LookUpCcModYesNo> LookUpCcModYesNo { get; set; }

        //added by rony
        public DbSet<LookUpCcModDredgingPlanParam> LookUpCcModDredgingPlanParam { get; set; }
        public DbSet<LookUpCcModGeneralSetting> LookUpCcModGeneralSetting { get; set; }
        public DbSet<LookUpCcModQueryState> LookUpCcModQueryState { get; set; }
        public DbSet<LookUpCcModUndertakingFormat> LookUpCcModUndertakingFormat { get; set; }
        public DbSet<LookUpCcModConnAmidKhalRiver> LookUpCcModConnAmidKhalRiver { get; set; }
        public DbSet<LookUpCcModTypesOfConsultation> LookUpCcModTypesOfConsultation { get; set; }
        public DbSet<LookUpMinistry> LookUpMinistry { get; set; }
        public DbSet<LookUpAgency> LookUpAgency { get; set; }
        public DbSet<LookUpCcModMemberType> LookUpCcModMemberType { get; set; }
        public DbSet<LookUpCcModUserLevelType> LookUpCcModUserLevelType { get; set; }
        public DbSet<LookUpAdpSector> LookUpAdpSector { get; set; }
        public DbSet<LookUpCcModIWRMCMember> LookUpCcModIWRMCMember { get; set; }
        public DbSet<CcModIWRMCMemberDetail> CcModIWRMCMemberDetail { get; set; }
        #endregion

        #region NoC Module Models
        public DbSet<LookUpNocAquiferCategory> LookUpNocAquiferCategory { get; set; }
        public DbSet<LookUpNocModYesNo> LookUpNocModYesNo { get; set; }
        public DbSet<LookUpNocModGrndWtrQtyParams> LookUpNocModGrndWtrQtyParams { get; set; }
        public DbSet<LookUpNocModHydroSystem> LookUpNocModHydroSystem { get; set; }
        public DbSet<LookUpNocUserType> LookUpNocUserType { get; set; }
        public DbSet<LookUpAdminBndMouza> LookUpAdminBndMouza { get; set; }
        public DbSet<LookUpNocWellType> LookUpNocWellType { get; set; }
        public DbSet<LookUpNocAppMode> LookUpNocAppMode { get; set; }
        public DbSet<LookUpNocAppObjective> LookUpNocAppObjective { get; set; }
        public DbSet<LookUpNocWithdrawalQuantity> LookUpNocWithdrawalQuantity { get; set; }
        public DbSet<LookUpNocAuthority> LookUpNocAuthority { get; set; }

        public DbSet<NocModAppCommonDetail> NocModAppCommonDetail { get; set; }
        public DbSet<NocModAppCompatNWMPDetail> NocModAppCompatNWMPDetail { get; set; }
        public DbSet<NocModAppCompatNWPDetail> NocModAppCompatNWPDetail { get; set; }
        public DbSet<NocModAppCompatSDGDetail> NocModAppCompatSDGDetail { get; set; }
        public DbSet<NocModAppGrndWtrDepthDetail> NocModAppGrndWtrDepthDetail { get; set; }
        public DbSet<NocModAppIndvDetail> NocModAppIndvDetail { get; set; }
        public DbSet<NocModAppLocationDetail> NocModAppLocationDetail { get; set; }
        public DbSet<NocModBDP2100GoalDetail> NocModBDP2100GoalDetail { get; set; }
        public DbSet<NocModGrndWtrQualityDetail> NocModGrndWtrQualityDetail { get; set; }
        public DbSet<NocModSurfaceWaterAvailDetail> NocModSurfaceWaterAvailDetail { get; set; }
        #endregion
    }
}
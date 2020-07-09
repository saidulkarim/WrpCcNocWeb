using System.ComponentModel;

namespace WrpCcNocWeb.Models
{
    public class ProjectHydrologicalRegionTemp
    {
        public int HydroRegionId { get; set; }
        public long ProjectId { get; set; }
        public string HydroRegionShortName { get; set; }
        public string HydroRegionShortNameBn { get; set; }
        public string HydroRegionFullName { get; set; }
        public string HydroRegionFullNameBn { get; set; }

        [DefaultValue(false)]
        public bool IsSelected { get; set; }
        public string Error { get; set; }
    }

    public class BDP2100HotSpotDetailTemp
    {
        public int DeltaPlanHotSpotId { get; set; }
        public string PlanName { get; set; }
        public string PlanNameBn { get; set; }
        public string Error { get; set; }
    }

    public class ProposedWaterUseDetailTemp
    {
        public int ProposedWaterUseId { get; set; }
        public string WaterUseTypeName { get; set; }
        public string WaterUseTypeNameBn { get; set; }
        public string Error { get; set; }
    }

    public class NavigationClassDetailTemp
    {
        public int NavigationClassId { get; set; }
        public string NavigationClassName { get; set; }
        public string NavigationClassNameBn { get; set; }
        public string Error { get; set; }
    }

    public class ConservLocationDetailTemp
    {
        public int ConservLocationId { get; set; }
        public string ConservLocation { get; set; }
        public string ConservLocationBn { get; set; }
        public string Error { get; set; }
    }

    public class StructTypeConDetailTemp
    {
        public int TypeOfStructureId { get; set; }
        public string StructureName { get; set; }
        public string StructureNameBn { get; set; }
        public string Error { get; set; }
    }

    public class RiverTypeDetailTemp
    {
        public int RiverTypeId { get; set; }
        public string RiverTypeName { get; set; }
        public string RiverTypeNameBn { get; set; }
        public string Error { get; set; }
    }

    public class RiverNatureDetailTemp
    {
        public int RiverNatureId { get; set; }
        public string RiverNatureTitle { get; set; }
        public string RiverNatureTitleBn { get; set; }
        public string Error { get; set; }
    }

    public class SediOfRiverOrKhalDetailTemp
    {
        public int SedimentationId { get; set; }
        public string SedimentationName { get; set; }
        public string SedimentationNameBn { get; set; }
        public string Error { get; set; }
    }

    //Surface Water Availability at Intake Point
    public class SWATemp
    {
        public long SWAId { get; set; }
        public long ProjectId { get; set; }
        public int MonthId { get; set; }
        public string Month { get; set; }
        public string MonthBn { get; set; }
        public double? MinWaterFlow { get; set; }
        public double? WaterDemandMonth { get; set; }
        public string Error { get; set; }
    }

    public class WtrDiversionSourceDetailTemp
    {
        public int WaterDiversionSourceId { get; set; }
        public string SourceName { get; set; }
        public string SourceNameBn { get; set; }
        public string Error { get; set; }
    }

    public class GroundWaterQualityDetailTemp
    {
        public long GroundWaterQualityId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterNameBn { get; set; }
        public string Error { get; set; }
    }

    //Ground Water Depth (m) from Nearest Observation Well
    public class GrndWtrDepthDetailTemp
    {
        public long GrndWtrDepthDetailId { get; set; }
        public long ProjectId { get; set; }
        public int MonthId { get; set; }
        public string Month { get; set; }
        public string MonthBn { get; set; }
        public double? WaterDepth { get; set; }
        public string Error { get; set; }
    }

    public class TypeOfWaterBodyDetailTemp
    {
        public int WaterBodyTypeId { get; set; }
        public string WaterBodyType { get; set; }
        public string WaterBodyTypeBn { get; set; }
        public string Error { get; set; }
    }

    public class DrainageConditionlTemp
    {
        public int DrainageConditionId { get; set; }
        public string DrainageCondition { get; set; }
        public string DrainageConditionBn { get; set; }
        public string Error { get; set; }
    }

    public class HydroSystemDetailTemp
    {
        public long HydroSysDetailId { get; set; }
        public long ProjectId { get; set; }
        public int HydroSystemCategoryId { get; set; }
        public string HydroSystemCategory { get; set; }
        public string HydroSystemCategoryBn { get; set; }
        public string NameOfHydroSystem { get; set; }
        public string HydroSystemLengthArea { get; set; }
        public string HydroSystemUnit { get; set; }
        public string HydroSystemUnitBn { get; set; }
        public string Error { get; set; }
    }

    public class TypesOfFloodTemp
    {
        public long FloodTypeDetailId { get; set; }
        public long ProjectId { get; set; }
        public int FloodTypeId { get; set; }
        public string FloodTypeName { get; set; }
        public string FloodTypeNameBn { get; set; }
        public string Error { get; set; }
    }

    public class FloodFrequencyDetailTemp
    {
        public long FloodFrequencyDetailId { get; set; }
        public long ProjectId { get; set; }
        public int FloodFrequencyId { get; set; }
        public string FloodFrequency { get; set; }
        public string FloodFrequencyBn { get; set; }
        public string FloodFrequencyLevel { get; set; }
        public string Error { get; set; }
    }

    public class IrrigCropAreaDetailTemp
    {
        public long IrrigatedCropId { get; set; }
        public long ProjectId { get; set; }
        public string CropName { get; set; }
        public string Area { get; set; }
        public string Error { get; set; }
    }

    public class AnalyzeOptionsDetailTemp
    {
        public long AnalyzeOptionsId { get; set; }
        public long ProjectId { get; set; }
        public string OptionNumber { get; set; }
        public string AnalyzeDescription { get; set; }
        public string AnalyzeRemarks { get; set; }
        public string Error { get; set; }
    }

    public class DesignSubmitDetailTemp
    {
        public long DesignSubmittedId { get; set; }
        public long ProjectId { get; set; }
        public int DesignSubmittedParameterId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterNameBn { get; set; }
        public int dswpdYN { get; set; }
        public string DesignSubmitApplicantCmt { get; set; }
        public string DesignSubmitAuthorityCmt { get; set; }
        public string Error { get; set; }
    }

    public class EcoFinAnalysisDetailTemp
    {
        public long EconomicalAndFinancialId { get; set; }
        public long ProjectId { get; set; }
        public int EcoAndFinancialParamId { get; set; }
        public string EcoAndFinancialParamName { get; set; }
        public string EcoAndFinancialParamNameBn { get; set; }
        public string EcoAndFinancialParamUnit { get; set; }
        public string EcoAndFinancialApplicantCmt { get; set; }
        public string EcoAndFinancialAuthorityCmt { get; set; }
        public string Error { get; set; }
    }

    public class EiaDetailTemp
    {
        public long EIAId { get; set; }
        public long ProjectId { get; set; }
        public int EIAParameterId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterNameBn { get; set; }
        public string PreProjectSituation { get; set; }
        public string PostProjectSituation { get; set; }
        public string PositiveNegativeImpact { get; set; }
        public string MitigationPlan { get; set; }
        public string Error { get; set; }
    }

    public class SiaDetailTemp
    {
        public long SIAId { get; set; }
        public long ProjectId { get; set; }
        public int SIAParameterId { get; set; }
        public string SIAParameterName { get; set; }
        public string SIAParameterNameBn { get; set; }
        public string PreProjectSituation { get; set; }
        public string PostProjectSituation { get; set; }
        public string PositiveNegativeImpact { get; set; }
        public string MitigationPlan { get; set; }
        public string Error { get; set; }
    }

    public class ProjectNWPArticleTemp
    {
        public int NWPArticleId { get; set; }
        public long ProjectId { get; set; }
        public string NWPShortTitle { get; set; }
        public string NWPShortTitleBn { get; set; }
        public string NWPArticleTitle { get; set; }
        public string NWPArticleTitleBn { get; set; }

        [DefaultValue(false)]
        public bool IsSelected { get; set; }
        public string Error { get; set; }
    }

    public class ProjectNWMPProgramTemp
    {
        public int NWMPProgrammeId { get; set; }
        public long ProjectId { get; set; }
        public string NWMPProgShortName { get; set; }
        public string NWMPProgrammeTitle { get; set; }
        public string NWMPLink { get; set; }

        [DefaultValue(false)]
        public bool IsSelected { get; set; }
        public string Error { get; set; }
    }

    public class ProjectSDGTemp
    {
        public int SDGGoalId { get; set; }
        public long ProjectId { get; set; }
        public string SDGGoalNumber { get; set; }
        public string SDGGoalNumberBn { get; set; }
        public string SDGDocLink { get; set; }

        [DefaultValue(false)]
        public bool IsSelected { get; set; }
        public string Error { get; set; }
    }

    public class ProjectSDGITemp
    {
        public int SDGIndicatorId { get; set; }
        public long ProjectId { get; set; }
        public string SDGIndicatorName { get; set; }
        public string SDGIndicatorNameBn { get; set; }
        public string SDGIndicatorDocLink { get; set; }

        [DefaultValue(false)]
        public bool IsSelected { get; set; }
        public string Error { get; set; }
    }

    public class BDP2100GoalDetailTemp
    {
        public int DeltPlan2100GoalId { get; set; }
        public long ProjectId { get; set; }
        public string DeltPlan2100Goal { get; set; }
        public string DeltPlan2100GoalBn { get; set; }
        public string DeltPlan2100GoaDocLink { get; set; }

        [DefaultValue(false)]
        public bool IsSelected { get; set; }
        public string Error { get; set; }
    }

    public class GPWMGroupTypeTemp
    {
        public int GPWMGroupTypeId { get; set; }
        public long ProjectId { get; set; }
        public string GPWMGroupTypeName { get; set; }
        public string GPWMGroupTypeNameBn { get; set; }

        [DefaultValue(false)]
        public bool IsSelected { get; set; }
        public string Error { get; set; }
    }

    public class DataAnalysisControlHeaderGroup
    {
        public string ControlTitle { get; set; }
        public string ControlId { get; set; }
    }

    public class DataAnalysisControlComments
    {
        public string ControlTitle { get; set; }
        public string Comments { get; set; }
    }

    public class AdminModUsersDetailTemp
    {
        public long UserRegistrationId { get; set; }
        public string UserFullName { get; set; }
        public string UserFullNameBn { get; set; }
        public string UserFatherName { get; set; }
        public string UserDateOfBirth { get; set; }
        public string UserNID { get; set; }
        public string UserPassportNo { get; set; }
        public string UserProfession { get; set; }
        public string UserDesignation { get; set; }
        public string UserAddress { get; set; }
        public string UserAddressBn { get; set; }
        public string UserAlternateEmail { get; set; }
        public string UserAlternateMobile { get; set; }
        public int SecurityQuestionId { get; set; }
        public string SecurityQuestionAnswer { get; set; }
        public int? IsProfileSubmitted { get; set; }
    }

    //Detail Water Use
    public class WaterUseDetailTemp
    {
        public long WaterUseDetailId { get; set; }
        public long ProjectId { get; set; }
        public int WaterUseId { get; set; }
        public string WaterUseName { get; set; }
        public string WaterUseNameBn { get; set; }
        public double? ExistingDemand { get; set; }
        public double? ProposedDemand { get; set; }
        public double? TotalDemand { get; set; }
        public double? YearConductingPeriod { get; set; }
        public double? AnnualDemand { get; set; }
        public string Error { get; set; }
    }

    //Ground Water Withdrawal
    public class GroundWaterWithdrawalDetailTemp
    {
        public long GroundWaterWithdrawDetailId { get; set; }
        public long ProjectId { get; set; }
        public int GrndWtrWithdrawalParamId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterNameBn { get; set; }
        public string ExistingInfrastructure { get; set; }
        public string ProposedInfrastructure { get; set; }
        public string Error { get; set; }
    }

    public class WaterBodyDetailTemp
    {
        public int WaterBodyId { get; set; }
        public string NameOfWaterBody { get; set; }
        public string NameOfWaterBodyBn { get; set; }
        public string Error { get; set; }
    }

    public class TypeOfWaterUseDetailTemp
    {
        public int TypeOfWaterUseId { get; set; }
        public string WaterUseName { get; set; }
        public string WaterUseNameBn { get; set; }
        public string Error { get; set; }
    }

    public class WaterUseSectorDetailTemp
    {
        public int WaterUseSectorId { get; set; }
        public string WaterUseSectorName { get; set; }
        public string WaterUseSectorNameBn { get; set; }
        public string Error { get; set; }
    }

    public class BankLineShiftingDetailTemp
    {
        public int BankLineShiftingId { get; set; }
        public string BankLineTitle { get; set; }
        public string BankLineTitleBn { get; set; }
        public string Error { get; set; }
    }

    public class BankStabilityDetailTemp
    {
        public int BankStabilityTypeId { get; set; }
        public string BankStabilityType { get; set; }
        public string BankStabilityTypeBn { get; set; }
        public string Error { get; set; }
    }
}
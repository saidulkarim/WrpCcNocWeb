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
}

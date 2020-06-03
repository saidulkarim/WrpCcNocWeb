﻿using System.ComponentModel;

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
}
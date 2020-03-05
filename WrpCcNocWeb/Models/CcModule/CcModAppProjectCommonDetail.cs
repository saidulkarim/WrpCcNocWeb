using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProjectCommonDetail
    {
        [Key]
        [Column("ProjectId", Order = 0)]
        public long ProjectId { get; set; }
        
        [Required]
        [Column("UserId", Order = 1)]
        [Display(Name = "User Name")]
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AdminModUsersDetail AdminModUsersDetail { get; set; }
                    
        [Column("AppSubmissionId", Order = 2)]
        [Display(Name = "Submission Id")]
        public long? AppSubmissionId { get; set; }
        
        [Column("ApplicationStateId", Order = 3)]
        [Display(Name = "Application State")]
        public int ApplicationStateId { get; set; }
        [ForeignKey("ApplicationStateId")]
        public virtual LookUpCcModApplicationState LookUpCcModApplicationState { get; set; }

        [Required]
        [Column("ProjectTypeId", Order = 4)]
        [Display(Name = "Project Type")]
        public int ProjectTypeId { get; set; }
        [ForeignKey("ProjectTypeId")]
        public virtual LookUpCcModProjectType LookUpCcModProjectType { get; set; }
        
        [Required]
        [Column("ProjectName", Order = 5)]
        [Display(Name = "Name of Project")]
        [MaxLength(200)]
        public string ProjectName { get; set; }
        
        [Required]
        [Column("BackgroundAndRationale", Order = 6)]
        [Display(Name = "Background And Rationale")]
        [MaxLength(550)]
        public string BackgroundAndRationale { get; set; }
        
        [Column("ProjectTarget", Order = 7)]
        [Display(Name = "Target")]
        [MaxLength(350)]
        public string ProjectTarget { get; set; }
        
        [Required]
        [Column("ProjectObjective", Order = 8)]
        [Display(Name = "Objectives")]
        [MaxLength(450)]
        public string ProjectObjective { get; set; }
        
        [Column("ProjectActivity", Order = 9)]
        [Display(Name = "Activity")]
        [MaxLength(250)]
        public string ProjectActivity { get; set; }
        
        [Column("ProjectStartDate", Order = 10)]
        [Display(Name = "Starting Date")]        
        public DateTime? ProjectStartDate { get; set; }
        
        [Column("ProjectCompletionDate", Order = 11)]
        [Display(Name = "Completion Date")]        
        public DateTime? ProjectCompletionDate { get; set; }
        
        [Column("ProjectEstimatedCost", Order = 12)]
        [Display(Name = "Project Estimated Cost")]
        public double ProjectEstimatedCost { get; set; }
        
        [Column("AnnualRainFallLast1Year", Order = 13)]
        [Display(Name = "Annual Rain Fall Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast1Year { get; set; }
        
        [Column("AnnualRainFallLast2Years", Order = 14)]
        [Display(Name = "Annual Rain Fall Second Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast2Years { get; set; }
        
        [Column("AnnualRainFallLast3Years", Order = 15)]
        [Display(Name = "Annual Rain Fall Third Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast3Years { get; set; }
        
        [Column("AnnualRainFallLast4Years", Order = 16)]
        [Display(Name = "Annual Rain Fall fourth Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast4Years { get; set; }
        
        [Column("AnnualRainFallLast5Years", Order = 17)]
        [Display(Name = "Annual Rain Fall fifth Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast5Years { get; set; }
        
        [Column("IssueChallageProblem", Order = 18)]
        [Display(Name = "Issue Challage Problem")]
        [MaxLength(150)]
        public string IssueChallageProblem { get; set; }
                
        [Column("YesNoStakeId", Order = 19)]
        [Display(Name = "Was Discussed With Stakeholder?")]
        public int? YesNoStakeId { get; set; }
        [ForeignKey("YesNoStakeId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoStake { get; set; }
        
        [Column("DiscussWithStakeApplicantCmt", Order = 20)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string DiscussWithStakeApplicantCmt { get; set; }
        
        [Column("DiscussWithStakeAuthorityCmt", Order = 21)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string DiscussWithStakeAuthorityCmt { get; set; }
        
        [Column("DiscussWithStakePosFeedback", Order = 22)]
        [Display(Name = "Discuss With Stake Positive Feedback")]
        [MaxLength(150)]
        public string DiscussWithStakePosFeedback { get; set; }
        
        [Column("DiscussWithStakeNegFeedback", Order = 23)]
        [Display(Name = "Discuss With Stake Negative Feedback")]
        [MaxLength(150)]
        public string DiscussWithStakeNegFeedback { get; set; }
        
        [Column("DiscussWithStakeParticipntLst", Order = 24)]
        [Display(Name = "Discuss With Stakeholder/Participant")]
        [MaxLength(150)]
        public string DiscussWithStakeParticipntLst { get; set; }
        
        [Column("DiscussWithStakeMeetingMin", Order = 25)]
        [Display(Name = "Discussion Meeting Minutes")]
        public string DiscussWithStakeMeetingMin { get; set; }
               
        [Column("AnalyzeOptYesNoId", Order = 26)]
        [Display(Name = "Was there Analyze Options?")]
        public int? AnalyzeOptYesNoId { get; set; }
        [ForeignKey("AnalyzeOptYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoAnalyze { get; set; }
        
        [Column("AnalyzeOptionsApplicantCmt", Order = 27)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string AnalyzeOptionsApplicantCmt { get; set; }
        
        [Column("AnalyzeOptionsAuthorityCmt", Order = 28)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string AnalyzeOptionsAuthorityCmt { get; set; }
        
        [Column("EnvAndSocialYesNoId", Order = 29)]
        [Display(Name = "Was there Environmenta and Social discussion")]
        public int? EnvAndSocialYesNoId { get; set; }
        [ForeignKey("EnvAndSocialYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoEnv { get; set; }
        
        [Column("EnvAndSocialApplicantCmt", Order = 30)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string EnvAndSocialApplicantCmt { get; set; }
        
        [Column("EnvAndSocialAuthorityCmt", Order = 31)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string EnvAndSocialAuthorityCmt { get; set; }
        
        [Column("CompatNWPYesNoId", Order = 32)]
        [Display(Name = "Was it Compatibile with NWP")]
        public int? CompatNWPYesNoId { get; set; }
        [ForeignKey("CompatNWPYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoNWPCompat { get; set; }
        
        [Column("CompatibilityNWPApplicantCmt", Order = 33)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string CompatibilityNWPApplicantCmt { get; set; }
        
        [Column("CompatibilityNWPAuthorityCmt", Order = 34)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string CompatibilityNWPAuthorityCmt { get; set; }
        
        [Column("CompatibilityNWPDocLink", Order = 35)]
        [Display(Name = "Document Link")]
        [MaxLength(150)]
        public string CompatibilityNWPDocLink { get; set; }
        
        [Column("NWMPCompatYesNoId", Order = 36)]
        [Display(Name = "Was it Compabile with NWMP?")]
        public int? NWMPCompatYesNoId { get; set; }
        [ForeignKey("NWMPCompatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoNWMPCompat { get; set; }
        
        [Column("NWMPApplicantCmt", Order = 37)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string NWMPApplicantCmt { get; set; }
        
        [Column("NWMPAuthorityCmt", Order = 38)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string NWMPAuthorityCmt { get; set; }
        
        [Column("NWMPDocLink", Order = 39)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string NWMPDocLink { get; set; }
        
        [Column("FYPYesNoId", Order = 40)]
        [Display(Name = "Was it Compabile with FYP?")]
        public int? FYPYesNoId { get; set; }
        [ForeignKey("FYPYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoFYP { get; set; }
        
        [Column("FYPApplicantCmt", Order = 41)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string FYPApplicantCmt { get; set; }
        
        [Column("FYPAuthorityCmt", Order = 42)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string FYPAuthorityCmt { get; set; }
        
        [Column("SDGYesNoId", Order = 43)]
        [Display(Name = "Was it Compabile with SDG?")]
        public int? SDGYesNoId { get; set; }
        [ForeignKey("SDGYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoSDG { get; set; }
        
        [Column("SDGApplicantCmt", Order = 44)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string SDGApplicantCmt { get; set; }
        
        [Column("SDGAuthorityCmt", Order = 45)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string SDGAuthorityCmt { get; set; }
        
        [Column("SDGDocLink", Order = 46)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string SDGDocLink { get; set; }
        
        [Column("DeltaPlanYesNoId", Order = 47)]
        [Display(Name = "Was it Compabile with Delta Plan 2100?")]
        public int? DeltaPlanYesNoId { get; set; }
        [ForeignKey("DeltaPlanYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDeltaPlan { get; set; }
        
        [Column("DeltaPlan2100ApplicantCmt", Order = 48)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string DeltaPlan2100ApplicantCmt { get; set; }
        
        [Column("DeltaPlan2100AuthorityCmt", Order = 49)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string DeltaPlan2100AuthorityCmt { get; set; }
        
        [Column("DeltaPlan2100DocLink", Order = 50)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string DeltaPlan2100DocLink { get; set; }
        
        [Column("CostalZoneYesNoId", Order = 51)]
        [Display(Name = "Was it Compabile with Costal Zone policy?")]
        public int? CostalZoneYesNoId { get; set; }
        [ForeignKey("CostalZoneYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoCostalZone { get; set; }
        
        [Column("CostalZoneApplicantCmt", Order = 52)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string CostalZoneApplicantCmt { get; set; }
        
        [Column("CostalZoneAuthorityCmt", Order = 53)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string CostalZoneAuthorityCmt { get; set; }
        
        [Column("CostalZoneDocLink", Order = 54)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string CostalZoneDocLink { get; set; }
               
        [Column("AgriculturalYesNoId", Order = 55)]
        [Display(Name = "Was it Compabile with Agricultural policy")]
        public int? AgriculturalYesNoId { get; set; }
        [ForeignKey("AgriculturalYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoAgricultural { get; set; }
        
        [Column("AgriApplicantCmt", Order = 56)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string AgriApplicantCmt { get; set; }
        
        [Column("AgriAuthorityCmt", Order = 57)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string AgriAuthorityCmt { get; set; }
        
        [Column("AgriDocLink", Order = 58)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string AgriDocLink { get; set; }
        
        [Column("FisheriesYesNoId", Order = 59)]
        [Display(Name = "Was it Compabile with Fisheries Policy?")]
        public int? FisheriesYesNoId { get; set; }
        [ForeignKey("FisheriesYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoFishery { get; set; }
        
        [Column("FisheriesApplicantCmt", Order = 60)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string FisheriesApplicantCmt { get; set; }
        
        [Column("FisheriesAuthorityCmt", Order = 61)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string FisheriesAuthorityCmt { get; set; }
        
        [Column("FisheriesDocLink", Order = 62)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string FisheriesDocLink { get; set; }
        
        [Column("IWRMYesNoId", Order = 63)]
        [Display(Name = "Was it Compabile with IWRM")]
        public int? IWRMYesNoId { get; set; }
        [ForeignKey("IWRMYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoIWRM { get; set; }
        
        [Column("IWRMApplicantCmt", Order = 64)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string IWRMApplicantCmt { get; set; }
        
        [Column("IWRMAuthorityCmt", Order = 65)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string IWRMAuthorityCmt { get; set; }
        
        [Column("GPWMYesNoId", Order = 66)]
        [Display(Name = "Was it Compabile with GPWM?")]
        public int? GPWMYesNoId { get; set; }
        [ForeignKey("GPWMYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoGPWM { get; set; }
        
        [Column("GPWMApplicantCmt", Order = 67)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string GPWMApplicantCmt { get; set; }
        
        [Column("GPWMAuthorityCmt", Order = 68)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string GPWMAuthorityCmt { get; set; }
        
        [Column("FeasibilityYesNoId", Order = 69)]
        [Display(Name = "Was it Feasibile?")]
        public int? FeasibilityYesNoId { get; set; }
        [ForeignKey("FeasibilityYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoFeasibility { get; set; }
        
        [Column("FeasibilityApplicantCmt", Order = 70)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string FeasibilityApplicantCmt { get; set; }
        
        [Column("FeasibilityAuthorityCmt", Order = 71)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string FeasibilityAuthorityCmt { get; set; }
        
        [Column("SocialIssuesYesNoId", Order = 72)]
        [Display(Name = "Was it Compabile with Social Issues")]
        public int? SocialIssuesYesNoId { get; set; }
        [ForeignKey("SocialIssuesYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoSocialIssue { get; set; }
        
        [Column("SocialIssuesApplicantCmt", Order = 73)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string SocialIssuesApplicantCmt { get; set; }
        
        [Column("SocialIssuesAuthorityCmt", Order = 74)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string SocialIssuesAuthorityCmt { get; set; }
        
        [Column("BankDocTypeId", Order = 75)]
        [Display(Name = "Bank Document Type")]
        public int? BankDocTypeId { get; set; }
        [ForeignKey("BankDocTypeId")]
        public virtual LookUpCcModBankDocType LookUpCcModBankDocType { get; set; }

        [Column("BankDocumentNumber", Order = 76)]
        [Display(Name = "Bank Document Number")]
        public int? BankDocumentNumber { get; set; }
        
        [Column("RecommendationId", Order = 77)]
        [Display(Name = "Recommendation")]
        public int? RecommendationId { get; set; }
        [ForeignKey("RecommendationId")]
        public virtual LookUpCcModRecommendation LookUpRecommendation { get; set; }

        [Column("RecommandCmt", Order = 78)]
        [Display(Name = "Comments")]
        [MaxLength(150)]
        public string RecommandCmt { get; set; }

        public virtual List<CcModAnalyzeOptionsDetail> CcModAnalyzeOptionsDetails { get; set; }
        public virtual List<CcModAppProject_31_IndvDetail> CcModAppProject_31_IndvDetails { get; set; }
        public virtual List<CcModBDP2100GoalDetail> CcModBDP2100GoalDetails { get; set; }
        public virtual List<CcModBDP2100HotSpotDetail> CcModBDP2100HotSpotDetails { get; set; }
        public virtual List<CcModDesignSubmitDetail> CcModDesignSubmitDetails { get; set; }
        public virtual List<CcModFloodFrequencyDetail> CcModFloodFrequencyDetails { get; set; }
        public virtual List<CcModGPWMGroupTypeDetail> CcModGPWMGroupTypeDetails { get; set; }
        public virtual List<CcModHydroSystemDetail> CcModHydroSystemDetails { get; set; }
        public virtual List<CcModPrjCompatNWMPDetail> CcModPrjCompatNWMPDetails { get; set; }
        public virtual List<CcModPrjCompatNWPDetail> CcModPrjCompatNWPDetails { get; set; }
        public virtual List<CcModPrjCompatSDGDetail> CcModPrjCompatSDGDetails { get; set; }
        public virtual List<CcModPrjCompatSDGIndiDetail> CcModPrjCompatSDGIndiDetails { get; set; }
        public virtual List<CcModPrjEcoFinAnalysisDetail> CcModPrjEcoFinAnalysisDetails { get; set; }
        public virtual List<CcModPrjEIADetail> CcModPrjEIADetails { get; set; }
        public virtual List<CcModPrjHydroRegionDetail> CcModPrjHydroRegionDetails { get; set; }
        public virtual List<CcModPrjIrrigCropAreaDetail> CcModPrjIrrigCropAreaDetails { get; set; }
        public virtual List<CcModPrjLocationDetail> CcModPrjLocationDetails { get; set; }
        public virtual List<CcModPrjSIADetail> CcModPrjSIADetails { get; set; }
        public virtual List<CcModPrjTypesOfFloodDetail> CcModPrjTypesOfFloodDetails { get; set; } 
    }
}

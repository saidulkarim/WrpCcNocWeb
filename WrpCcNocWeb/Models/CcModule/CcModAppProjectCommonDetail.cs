﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.AdminModule;
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

        [Column("ApprovalStatusId", Order = 4)]
        [Display(Name = "Approval Status")]
        public int? ApprovalStatusId { get; set; }
        [ForeignKey("ApprovalStatusId")]
        public virtual LookUpCcModApprovalStatus LookUpCcModApprovalStatus { get; set; }

        [Required]
        [Column("ProjectTypeId", Order = 5)]
        [Display(Name = "Project Type")]
        public int ProjectTypeId { get; set; }
        [ForeignKey("ProjectTypeId")]
        public virtual LookUpCcModProjectType LookUpCcModProjectType { get; set; }

        [Required]
        [Column("ProjectName", Order = 6)]
        [Display(Name = "Name of Project")]
        [MaxLength(500)]
        public string ProjectName { get; set; }

        [Required]
        [Column("BackgroundAndRationale", Order = 7)]
        [Display(Name = "Background And Rationale")]
        [MaxLength(1000)]
        public string BackgroundAndRationale { get; set; }

        [Column("ProjectTarget", Order = 8)]
        [Display(Name = "Target")]
        [MaxLength(1000)]
        public string ProjectTarget { get; set; }

        [Required]
        [Column("ProjectObjective", Order = 9)]
        [Display(Name = "Objectives")]
        [MaxLength(1000)]
        public string ProjectObjective { get; set; }

        [Column("ProjectActivity", Order = 10)]
        [Display(Name = "Activity")]
        [MaxLength(1000)]
        public string ProjectActivity { get; set; }

        [Column("ProjectStartDate", Order = 11)]
        [Display(Name = "Starting Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProjectStartDate { get; set; }

        [Column("ProjectCompletionDate", Order = 12)]
        [Display(Name = "Completion Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProjectCompletionDate { get; set; }

        [Column("ProjectEstimatedCost", Order = 13)]
        [Display(Name = "Project Estimated Cost")]
        public double ProjectEstimatedCost { get; set; }

        [Column("AnnualRainFallLast1Year", Order = 14)]
        [Display(Name = "Annual Rain Fall Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast1Year { get; set; }

        [Column("AnnualRainFallLast2Years", Order = 15)]
        [Display(Name = "Annual Rain Fall Second Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast2Years { get; set; }

        [Column("AnnualRainFallLast3Years", Order = 16)]
        [Display(Name = "Annual Rain Fall Third Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast3Years { get; set; }

        [Column("AnnualRainFallLast4Years", Order = 17)]
        [Display(Name = "Annual Rain Fall fourth Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast4Years { get; set; }

        [Column("AnnualRainFallLast5Years", Order = 18)]
        [Display(Name = "Annual Rain Fall fifth Last Year")]
        [MaxLength(50)]
        public string AnnualRainFallLast5Years { get; set; }

        [Column("IssueChallageProblem", Order = 19)]
        [Display(Name = "Issue Challage Problem")]
        [MaxLength(150)]
        public string IssueChallageProblem { get; set; }

        [Column("YesNoStakeId", Order = 20)]
        [Display(Name = "Was Discussed With Stakeholder?")]
        public int? YesNoStakeId { get; set; }
        [ForeignKey("YesNoStakeId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoStake { get; set; }

        [Column("DiscussWithStakeApplicantCmt", Order = 21)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string DiscussWithStakeApplicantCmt { get; set; }

        [Column("DiscussWithStakeAuthorityCmt", Order = 22)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string DiscussWithStakeAuthorityCmt { get; set; }

        [Column("DiscussWithStakePosFeedback", Order = 23)]
        [Display(Name = "Discuss With Stake Positive Feedback")]
        [MaxLength(150)]
        public string DiscussWithStakePosFeedback { get; set; }

        [Column("DiscussWithStakeNegFeedback", Order = 24)]
        [Display(Name = "Discuss With Stake Negative Feedback")]
        [MaxLength(150)]
        public string DiscussWithStakeNegFeedback { get; set; }

        [Column("DiscussWithStakeParticipntLst", Order = 25)]
        [Display(Name = "Discuss With Stakeholder/Participant")]
        [MaxLength(150)]
        public string DiscussWithStakeParticipntLst { get; set; }

        [Column("DiscussWithStakeMeetingMin", Order = 26)]
        [Display(Name = "Discussion Meeting Minutes")]
        public string DiscussWithStakeMeetingMin { get; set; }

        [Column("AnalyzeOptYesNoId", Order = 27)]
        [Display(Name = "Was there Analyze Options?")]
        public int? AnalyzeOptYesNoId { get; set; }
        [ForeignKey("AnalyzeOptYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoAnalyze { get; set; }

        [Column("AnalyzeOptionsApplicantCmt", Order = 28)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string AnalyzeOptionsApplicantCmt { get; set; }

        [Column("AnalyzeOptionsAuthorityCmt", Order = 29)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string AnalyzeOptionsAuthorityCmt { get; set; }

        [Column("EnvAndSocialYesNoId", Order = 30)]
        [Display(Name = "Was there Environmenta and Social discussion")]
        public int? EnvAndSocialYesNoId { get; set; }
        [ForeignKey("EnvAndSocialYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoEnv { get; set; }

        [Column("EnvAndSocialApplicantCmt", Order = 31)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string EnvAndSocialApplicantCmt { get; set; }

        [Column("EnvAndSocialAuthorityCmt", Order = 32)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string EnvAndSocialAuthorityCmt { get; set; }

        [Column("CompatNWPYesNoId", Order = 33)]
        [Display(Name = "Was it Compatibile with NWP")]
        public int? CompatNWPYesNoId { get; set; }
        [ForeignKey("CompatNWPYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoNWPCompat { get; set; }

        [Column("CompatibilityNWPApplicantCmt", Order = 34)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string CompatibilityNWPApplicantCmt { get; set; }

        [Column("CompatibilityNWPAuthorityCmt", Order = 35)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string CompatibilityNWPAuthorityCmt { get; set; }

        [Column("CompatibilityNWPDocLink", Order = 36)]
        [Display(Name = "Document Link")]
        [MaxLength(150)]
        public string CompatibilityNWPDocLink { get; set; }

        [Column("NWMPCompatYesNoId", Order = 37)]
        [Display(Name = "Was it Compabile with NWMP?")]
        public int? NWMPCompatYesNoId { get; set; }
        [ForeignKey("NWMPCompatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoNWMPCompat { get; set; }

        [Column("NWMPApplicantCmt", Order = 38)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string NWMPApplicantCmt { get; set; }

        [Column("NWMPAuthorityCmt", Order = 39)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string NWMPAuthorityCmt { get; set; }

        [Column("NWMPDocLink", Order = 40)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string NWMPDocLink { get; set; }

        [Column("FYPYesNoId", Order = 41)]
        [Display(Name = "Was it Compabile with FYP?")]
        public int? FYPYesNoId { get; set; }
        [ForeignKey("FYPYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoFYP { get; set; }

        [Column("FYPApplicantCmt", Order = 42)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string FYPApplicantCmt { get; set; }

        [Column("FYPAuthorityCmt", Order = 43)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string FYPAuthorityCmt { get; set; }

        [Column("SDGYesNoId", Order = 44)]
        [Display(Name = "Was it Compabile with SDG?")]
        public int? SDGYesNoId { get; set; }
        [ForeignKey("SDGYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoSDG { get; set; }

        [Column("SDGApplicantCmt", Order = 45)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string SDGApplicantCmt { get; set; }

        [Column("SDGAuthorityCmt", Order = 46)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string SDGAuthorityCmt { get; set; }

        [Column("SDGDocLink", Order = 47)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string SDGDocLink { get; set; }

        [Column("DeltaPlanYesNoId", Order = 48)]
        [Display(Name = "Was it Compabile with Delta Plan 2100?")]
        public int? DeltaPlanYesNoId { get; set; }
        [ForeignKey("DeltaPlanYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDeltaPlan { get; set; }

        [Column("DeltaPlan2100ApplicantCmt", Order = 49)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string DeltaPlan2100ApplicantCmt { get; set; }

        [Column("DeltaPlan2100AuthorityCmt", Order = 50)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string DeltaPlan2100AuthorityCmt { get; set; }

        [Column("DeltaPlan2100DocLink", Order = 51)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string DeltaPlan2100DocLink { get; set; }

        [Column("CostalZoneYesNoId", Order = 52)]
        [Display(Name = "Was it Compabile with Costal Zone policy?")]
        public int? CostalZoneYesNoId { get; set; }
        [ForeignKey("CostalZoneYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoCostalZone { get; set; }

        [Column("CostalZoneApplicantCmt", Order = 53)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string CostalZoneApplicantCmt { get; set; }

        [Column("CostalZoneAuthorityCmt", Order = 54)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string CostalZoneAuthorityCmt { get; set; }

        [Column("CostalZoneDocLink", Order = 55)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string CostalZoneDocLink { get; set; }

        [Column("AgriculturalYesNoId", Order = 56)]
        [Display(Name = "Was it Compabile with Agricultural policy")]
        public int? AgriculturalYesNoId { get; set; }
        [ForeignKey("AgriculturalYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoAgricultural { get; set; }

        [Column("AgriApplicantCmt", Order = 57)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string AgriApplicantCmt { get; set; }

        [Column("AgriAuthorityCmt", Order = 58)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string AgriAuthorityCmt { get; set; }

        [Column("AgriDocLink", Order = 59)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string AgriDocLink { get; set; }

        [Column("FisheriesYesNoId", Order = 60)]
        [Display(Name = "Was it Compabile with Fisheries Policy?")]
        public int? FisheriesYesNoId { get; set; }
        [ForeignKey("FisheriesYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoFishery { get; set; }

        [Column("FisheriesApplicantCmt", Order = 61)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string FisheriesApplicantCmt { get; set; }

        [Column("FisheriesAuthorityCmt", Order = 62)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string FisheriesAuthorityCmt { get; set; }

        [Column("FisheriesDocLink", Order = 63)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string FisheriesDocLink { get; set; }

        [Column("IWRMYesNoId", Order = 64)]
        [Display(Name = "Was it Compabile with IWRM")]
        public int? IWRMYesNoId { get; set; }
        [ForeignKey("IWRMYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoIWRM { get; set; }

        [Column("IWRMApplicantCmt", Order = 65)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string IWRMApplicantCmt { get; set; }

        [Column("IWRMAuthorityCmt", Order = 66)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string IWRMAuthorityCmt { get; set; }

        [Column("GPWMYesNoId", Order = 67)]
        [Display(Name = "Was it Compabile with GPWM?")]
        public int? GPWMYesNoId { get; set; }
        [ForeignKey("GPWMYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoGPWM { get; set; }

        [Column("GPWMApplicantCmt", Order = 68)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string GPWMApplicantCmt { get; set; }

        [Column("GPWMAuthorityCmt", Order = 69)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string GPWMAuthorityCmt { get; set; }

        [Column("FeasibilityYesNoId", Order = 70)]
        [Display(Name = "Was it Feasibile?")]
        public int? FeasibilityYesNoId { get; set; }
        [ForeignKey("FeasibilityYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoFeasibility { get; set; }

        [Column("FeasibilityApplicantCmt", Order = 71)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string FeasibilityApplicantCmt { get; set; }

        [Column("FeasibilityAuthorityCmt", Order = 72)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string FeasibilityAuthorityCmt { get; set; }

        [Column("SocialIssuesYesNoId", Order = 73)]
        [Display(Name = "Was it Compabile with Social Issues")]
        public int? SocialIssuesYesNoId { get; set; }
        [ForeignKey("SocialIssuesYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoSocialIssue { get; set; }

        [Column("SocialIssuesApplicantCmt", Order = 74)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string SocialIssuesApplicantCmt { get; set; }

        [Column("SocialIssuesAuthorityCmt", Order = 75)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string SocialIssuesAuthorityCmt { get; set; }

        [Column("NocTypeId", Order = 76)]
        [Display(Name = "NOC Type")]
        public int? NocTypeId { get; set; }
        [ForeignKey("NocTypeId")]
        public virtual LookUpCcModNocType LookUpCcModNocType { get; set; }

        [Column("NocFileName", Order = 77)]
        [Display(Name = "Upload NOC")]
        [MaxLength(100)]
        public string NocFileName { get; set; }

        [Column("PaymentMethodId", Order = 78)]
        [Display(Name = "Payment Method")]
        public int? PaymentMethodId { get; set; }
        [ForeignKey("PaymentMethodId")]
        public virtual LookUpCcModPaymentMethod LookUpCcModPaymentMethod { get; set; }

        [Column("PaymentDocNumber", Order = 79)]
        [Display(Name = "Payment Document Number")]
        [MaxLength(50)]
        public string PaymentDocNumber { get; set; }

        [Column("PaidAmount", Order = 80)]
        [Display(Name = "Paid Amount")]
        public double? PaidAmount { get; set; }

        [Column("PaymentDocFileName", Order = 81)]
        [Display(Name = "Upload Payment Document")]
        [MaxLength(100)]
        public string PaymentDocFileName { get; set; }

        [Column("RecommendationId", Order = 82)]
        [Display(Name = "Recommendation")]
        public int? RecommendationId { get; set; }
        [ForeignKey("RecommendationId")]
        public virtual LookUpCcModRecommendation LookUpRecommendation { get; set; }

        [Column("RecommendationComment", Order = 83)]
        [Display(Name = "Comments")]
        [MaxLength(350)]
        public string RecommendationComment { get; set; }

        [Column("IsCompletedId", Order = 84)]
        [Display(Name = "Is Completed")]
        public int? IsCompletedId { get; set; }
        //[ForeignKey("IsCompletedId")]
        //public virtual LookUpCcModIsCompletedState LookUpCcModIsCompletedState { get; set; }

        [Column("LanguageId", Order = 85)]
        [Display(Name = "Language")]
        public int? LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual LookUpAdminModLanguage LookUpAdminModLanguage { get; set; }

        [Column("ReasonOfRejection", Order = 86)]
        [Display(Name = "Reason Of Rejection")]
        [MaxLength(550)]
        public string ReasonOfRejection { get; set; }

        [Column("ReviewCycleNo", Order = 87)]
        [Display(Name = "Review Cycle No.")]
        public int? ReviewCycleNo { get; set; }

        [Column("AppSubmissionDate", Order = 88)]
        [Display(Name = "App Submission Date")]
        public DateTime? AppSubmissionDate { get; set; }

        [Column("PaymentDate", Order = 89)]
        [Display(Name = "Payment Date")]
        public DateTime? PaymentDate { get; set; }

        [Column("FYPDocLink", Order = 90)]
        [Display(Name = "FYP Doc Link")]
        [MaxLength(250)]
        public string FYPDocLink { get; set; }

        [Column("CiwupYesNoId", Order = 91)]
        [Display(Name = "Compatibility with Industrial Water Use Policy")]
        public int? CiwupYesNoId { get; set; }
        [ForeignKey("CiwupYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoCiwup { get; set; }

        [Column("CiwupApplicantCmt", Order = 92)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string CiwupApplicantCmt { get; set; }

        [Column("CiwupAuthorityCmt", Order = 93)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string CiwupAuthorityCmt { get; set; }

        [Column("CiwupDocLink", Order = 94)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string CiwupDocLink { get; set; }

        [Column("AdditionalTextComment", Order = 95)]
        [Display(Name = "Additional Text Comment")]
        [MaxLength(500)]
        public string AdditionalTextComment { get; set; }

        [Column("AppApprovalDate", Order = 96)]
        [Display(Name = "App Approval Date")]
        public DateTime? AppApprovalDate { get; set; }

        [Column("AnyHearingOccured", Order = 97)]
        [Display(Name = "Any Hearing Occured?")]
        public int? AnyHearingOccured { get; set; }
        //[ForeignKey("AnyHearingOccured")]
        //public virtual LookUpCcModYesNo LookUpYesNoAHO { get; set; }

        [Column("HearingDescription", Order = 98)]
        [Display(Name = "Hearing Details")]
        [MaxLength(500)]
        public string HearingDescription { get; set; }

        [Column("RecommendedByIwrmc", Order = 99)]
        [Display(Name = "Recommendation By IWRMC?")]
        public int? RecommendedByIwrmc { get; set; }
        //[ForeignKey("AnyHearingOccured")]
        //public virtual LookUpCcModYesNo LookUpYesNoRBI { get; set; }

        [Column("RecommendedByIwrmcNote", Order = 100)]
        [Display(Name = "Recommendation Details")]
        [MaxLength(500)]
        public string RecommendedByIwrmcNote { get; set; }

        [Column("UndertakingSubmitYesNoId", Order = 101)]
        [Display(Name = "Is Undertaking Submitted?")]
        public int? UndertakingSubmitYesNoId { get; set; }
        //[ForeignKey("UndertakingSubmitYesNoId")]
        //public virtual LookUpCcModYesNo LookUpYesNoUndertakingSubmit { get; set; }

        [Column("UndertakingSubmitDate", Order = 102)]
        [Display(Name = "Undertaking Submission Date")]
        public DateTime? UndertakingSubmitDate { get; set; }

        [Column("UndertakingCheckByHigherAuth", Order = 103)]
        [Display(Name = "Is Undertaking Checked by Higher Authority?")]
        public int? UndertakingCheckByHigherAuth { get; set; }

        [Column("ProjectOutcome", Order = 104)]
        [Display(Name = "Project Outcome")]
        [MaxLength(1000)]
        public string ProjectOutcome { get; set; }

        [Column("ProjectOutput", Order = 105)]
        [Display(Name = "Project Output")]
        [MaxLength(1000)]
        public string ProjectOutput { get; set; }

        [Column("ProjectBoundaryMap", Order = 106)]
        [Display(Name = "Project Boundary Map")]
        [MaxLength(50)]
        public string ProjectBoundaryMap { get; set; }

        [Column("TypesOfConsultationId", Order = 107)]
        [Display(Name = "Types of Consultation")]
        public int? TypesOfConsultationId { get; set; }

        [Column("RecommendedOption", Order = 108)]
        [Display(Name = "Recommended Option")]
        [MaxLength(500)]
        public string RecommendedOption { get; set; }

        [Column("EcoFinAnalysisFile", Order = 109)]
        [Display(Name = "Analysis File Upload")]
        [MaxLength(50)]
        public string EcoFinAnalysisFile { get; set; }

        [Column("EnvAndSocialEiaFile", Order = 110)]
        [Display(Name = "EIA File Upload")]
        [MaxLength(50)]
        public string EnvAndSocialEiaFile { get; set; }

        [Column("EnvAndSocialSiaFile", Order = 111)]
        [Display(Name = "SIA File Upload")]
        [MaxLength(50)]
        public string EnvAndSocialSiaFile { get; set; }

        #region added on 19-Jan-2021
        [Column("GuidelinesFollowedYesNoId", Order = 112)]
        [Display(Name = "Project Appraisal Followed the Guidelines for Project Assessment (GPA)")]
        public int? GuidelinesFollowedYesNoId { get; set; }
        [ForeignKey("GuidelinesFollowedYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoGuidelinesFollowed { get; set; }

        [Column("GuidelinesFollowedAppCmt", Order = 113)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string GuidelinesFollowedAppCmt { get; set; }

        [Column("GuidelinesFollowedAuthCmt", Order = 114)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string GuidelinesFollowedAuthCmt { get; set; }

        [Column("FeasibilityStudyReportFile", Order = 115)]
        [Display(Name = "Feasibility Study Report")]
        [MaxLength(50)]
        public string FeasibilityStudyReportFile { get; set; }

        [Column("PrjDuplicateAttestedLtrFile", Order = 116)]
        [Display(Name = "Attested Letter Upload")]
        [MaxLength(50)]
        public string PrjDuplicateAttestedLtrFile { get; set; }

        [Column("OrgHeadAttestedLetterFile", Order = 117)]
        [Display(Name = "Attested letter from head of the organization including Signature (Upload)")]
        [MaxLength(50)]
        public string OrgHeadAttestedLetterFile { get; set; }
        #endregion

        [Column("RespectiveAgency", Order = 118)]
        [Display(Name = "Respective Agency")]
        public int? RespectiveAgency { get; set; }
        //[ForeignKey("AgencyId")]
        //public virtual LookUpAgency LookUpAgency { get; set; }

        [Column("RespectiveMinistry", Order = 119)]
        [Display(Name = "Respective Ministry")]
        public int? RespectiveMinistry { get; set; }
        //[ForeignKey("MinistryId")]
        //public virtual LookUpMinistry LookUpMinistry { get; set; }

        [Column("AnyInterventionOfOtherPrj", Order = 120)]
        [Display(Name = "Is There Any Intervention of Other Projects?")]
        public int? AnyInterventionOfOtherPrj { get; set; }
        //[ForeignKey("AnyInterventionPrjId")]
        //public virtual LookUpCcModAnyInterventionPrj LookUpCcModAnyInterventionPrj { get; set; }

        [Column("AnyGovCircularName", Order = 121)]
        [Display(Name = "Any Circular of the Government has been Used?")]
        [MaxLength(150)]
        public string AnyGovCircularName { get; set; }

        [Column("AnyGovCircularFile", Order = 122)]
        [Display(Name = "Circular File Upload")]
        [MaxLength(50)]
        public string AnyGovCircularFile { get; set; }

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.AdminModule;
using WrpCcNocWeb.Models.CcModule;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModAppCommonDetail
    {
        [Key]
        [Column("NocApplicationId", Order = 0)]
        public long NocApplicationId { get; set; }

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
        [Column("TitleOfTubeWell", Order = 5)]
        [Display(Name = "Title of Tube Well")]
        [MaxLength(250)]
        public string TitleOfTubeWell { get; set; }
                
        [Column("TubeWellUserTypeId", Order = 6)]
        [Display(Name = "Tube Well User Type")]        
        public int TubeWellUserTypeId { get; set; }
        [ForeignKey("TubeWellUserTypeId")]
        public virtual LookUpNocUserType LookUpNocUserType { get; set; }
        
        [Column("ModeId", Order = 7)]
        [Display(Name = "Mode")]
        public int ModeId { get; set; }
        [ForeignKey("ModeId")]
        public virtual LookUpNocAppMode LookUpNocAppMode { get; set; }
        
        [Column("ObjectiveId", Order = 8)]
        [Display(Name = "Objective/ Purpose")]
        public int ObjectiveId { get; set; }
        [ForeignKey("ObjectiveId")]
        public virtual LookUpNocAppObjective LookUpNocAppObjective { get; set; }
        
        [Column("WithdrawalQuantityId", Order = 9)]
        [Display(Name = "Withdrawal Quantity")]
        public int WithdrawalQuantityId { get; set; }
        [ForeignKey("WithdrawalQuantityId")]
        public virtual LookUpNocWithdrawalQuantity LookUpNocWithdrawalQuantity { get; set; }
        
        [Column("AuthorityId", Order = 10)]
        [Display(Name = "NOC Authority")]
        public int? AuthorityId { get; set; }
        [ForeignKey("AuthorityId")]
        public virtual LookUpNocAuthority LookUpNocAuthority { get; set; }
        
        [Column("CompatNWPYesNoId", Order = 11)]
        [Display(Name = "Was it Compatibile with NWP")]
        public int? CompatNWPYesNoId { get; set; }
        [ForeignKey("CompatNWPYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoNWPCompat { get; set; }

        [Column("CompatibilityNWPApplicantCmt", Order = 12)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string CompatibilityNWPApplicantCmt { get; set; }

        [Column("CompatibilityNWPAuthorityCmt", Order = 13)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string CompatibilityNWPAuthorityCmt { get; set; }

        [Column("CompatibilityNWPDocLink", Order = 14)]
        [Display(Name = "Document Link")]
        [MaxLength(150)]
        public string CompatibilityNWPDocLink { get; set; }

        [Column("NWMPCompatYesNoId", Order = 15)]
        [Display(Name = "Was it Compabile with NWMP?")]
        public int? NWMPCompatYesNoId { get; set; }
        [ForeignKey("NWMPCompatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoNWMPCompat { get; set; }

        [Column("NWMPApplicantCmt", Order = 16)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string NWMPApplicantCmt { get; set; }

        [Column("NWMPAuthorityCmt", Order = 17)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string NWMPAuthorityCmt { get; set; }

        [Column("NWMPDocLink", Order = 18)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string NWMPDocLink { get; set; }

        [Column("FYPYesNoId", Order = 19)]
        [Display(Name = "Was it Compabile with FYP?")]
        public int? FYPYesNoId { get; set; }
        [ForeignKey("FYPYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoFYP { get; set; }

        [Column("FYPApplicantCmt", Order = 20)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string FYPApplicantCmt { get; set; }

        [Column("FYPAuthorityCmt", Order = 21)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string FYPAuthorityCmt { get; set; }

        [Column("SDGYesNoId", Order = 22)]
        [Display(Name = "Was it Compabile with SDG?")]
        public int? SDGYesNoId { get; set; }
        [ForeignKey("SDGYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoSDG { get; set; }

        [Column("SDGApplicantCmt", Order = 23)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string SDGApplicantCmt { get; set; }

        [Column("SDGAuthorityCmt", Order = 24)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string SDGAuthorityCmt { get; set; }

        [Column("SDGDocLink", Order = 25)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string SDGDocLink { get; set; }

        [Column("DeltaPlanYesNoId", Order = 26)]
        [Display(Name = "Was it Compabile with Delta Plan 2100?")]
        public int? DeltaPlanYesNoId { get; set; }
        [ForeignKey("DeltaPlanYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDeltaPlan { get; set; }

        [Column("DeltaPlan2100ApplicantCmt", Order = 27)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string DeltaPlan2100ApplicantCmt { get; set; }

        [Column("DeltaPlan2100AuthorityCmt", Order = 28)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string DeltaPlan2100AuthorityCmt { get; set; }

        [Column("DeltaPlan2100DocLink", Order = 29)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string DeltaPlan2100DocLink { get; set; }
        
        [Column("IsAppViolateYesNoId", Order = 30)]
        [Display(Name = "Is the Applicant Violate the Conditions?")]
        public int? IsAppViolateYesNoId { get; set; }
        [ForeignKey("IsAppViolateYesNoId")]
        public virtual LookUpNocModYesNo LookUpYesNoAppVio { get; set; }

        [Column("IsAppViolateApplicantComment", Order = 31)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string IsAppViolateApplicantComment { get; set; }

        [Column("IsAppViolateAuthorityComment", Order = 32)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string IsAppViolateAuthorityComment { get; set; }

        [Column("IsAppViolateDocLink", Order = 33)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string IsAppViolateDocLink { get; set; }
        
        [Column("IsAppAlrdyIssuByOthrOrgYesNoId", Order = 34)]
        [Display(Name = "Is the Applicant Already Issues NOC?")]
        public int? IsAppAlrdyIssuByOthrOrgYesNoId { get; set; }
        [ForeignKey("IsAppAlrdyIssuByOthrOrgYesNoId")]
        public virtual LookUpNocModYesNo LookUpYesNoAppIss { get; set; }

        [Column("IsAppAIBOOApplicantComment", Order = 35)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string IsAppAIBOOApplicantComment { get; set; }

        [Column("IsAppAIBOOAuthorityComment", Order = 36)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string IsAppAIBOOAuthorityComment { get; set; }

        [Column("IsAppAIBOODocLink", Order = 37)]
        [Display(Name = "Document Link")]
        [MaxLength(250)]
        public string IsAppAIBOODocLink { get; set; }
        
        [Column("NOCFromLocalAuthority", Order = 38)]
        [Display(Name = "NOC from Local Authority (if applicable)")]
        [MaxLength(250)]
        public string NOCFromLocalAuthority { get; set; }
        
        [Column("WaterBillPaySystem", Order = 39)]
        [Display(Name = "Water Bill Payment System")]
        [MaxLength(250)]
        public string WaterBillPaySystem { get; set; }
        
        [Column("RecommendationYesNoId", Order = 40)]
        [Display(Name = "Recommendation")]
        public int? RecommendationYesNoId { get; set; }
        [ForeignKey("RecommendationId")]
        public virtual LookUpCcModRecommendation LookUpRecommendation { get; set; }

        [Column("ConditionalNote", Order = 41)]
        [Display(Name = "Conditional Note")]
        [MaxLength(350)]
        public string ConditionalNote { get; set; }
    }
}
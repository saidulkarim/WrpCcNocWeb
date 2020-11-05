using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class AdminModUsersDetail
    {
        [Key]
        [Column("UserId", Order = 0)]
        public long UserId { get; set; }

        [Required]
        [Column("UserRegistrationId", Order = 1)]
        [Display(Name = "User Registration")]
        public long UserRegistrationId { get; set; }
        [ForeignKey("UserRegistrationId")]
        public virtual AdminModUserRegistrationDetail adminModUserRegistrationDetails { get; set; }

        [Required]
        [Column("SecurityQuestionId", Order = 2)]
        [Display(Name = "Security Question")]
        public int SecurityQuestionId { get; set; }
        [ForeignKey("SecurityQuestionId")]
        public virtual LookUpAdminModSecurityQuestion lookUpAdminModSecurityQuestion { get; set; }

        [Required]
        [Column("SecurityQuestionAnswer", Order = 3)]
        [Display(Name = "Answer of Security Question")]
        [MaxLength(50)]
        public string SecurityQuestionAnswer { get; set; }

        [Display(Name = "Is Profile Submitted")]
        [Column("IsProfileSubmitted", Order = 4)]
        public int? IsProfileSubmitted { get; set; }

        [Column("ApplicantSignature", Order = 5)]
        [MaxLength(50)]
        public string ApplicantSignature { get; set; }

        [Column("HigherAuthSignature", Order = 6)]
        [MaxLength(50)]
        public string HigherAuthSignature { get; set; }

        [Column("HigherAuthSeal", Order = 7)]
        [MaxLength(50)]
        public string HigherAuthSeal { get; set; }

        [Required]
        [Column("ApplicantTypeId", Order = 8)]
        [Display(Name = "Applicant Type")]
        public int ApplicantTypeId { get; set; }
        [ForeignKey("ApplicantTypeId")]
        public virtual LookUpCcModApplicantType LookUpCcModApplicantType { get; set; }

        [Required]
        [Column("ApplicantName", Order = 9)]
        [MaxLength(100)]
        [Display(Name = "Applicant Name (English)")]
        public string ApplicantName { get; set; }

        [Required]
        [Column("ApplicantNameBn", Order = 10)]
        [MaxLength(250)]
        [Display(Name = "আবেদনকারীর নাম (বাংলায়)")]
        public string ApplicantNameBn { get; set; }

        [Column("OrganizationName", Order = 11)]
        [MaxLength(150)]
        [Display(Name = "Organization Name (English)")]
        public string OrganizationName { get; set; }

        [Column("OrganizationNameBn", Order = 12)]
        [MaxLength(250)]
        [Display(Name = "প্রতিষ্ঠানের নাম (বাংলায়)")]
        public string OrganizationNameBn { get; set; }

        [Column("OrganizationAddress", Order = 13)]
        [MaxLength(150)]
        [Display(Name = "Organization Address (English)")]
        public string OrganizationAddress { get; set; }

        [Column("OrganizationAddressBn", Order = 14)]
        [MaxLength(250)]
        [Display(Name = "প্রতিষ্ঠানের ঠিকানা (বাংলায়)")]
        public string OrganizationAddressBn { get; set; }

        [Column("UserProfession", Order = 15)]
        [Display(Name = "Profession")]
        [MaxLength(50)]
        public string UserProfession { get; set; }

        [Column("UserDesignation", Order = 16)]
        [Display(Name = "Designation")]
        [MaxLength(50)]
        public string UserDesignation { get; set; }

        [Column("UserNID", Order = 17)]
        [Display(Name = "National ID No")]
        [MaxLength(20)]
        public string UserNID { get; set; }

        [Column("UserNIDFile", Order = 18)]
        [Display(Name = "National ID File")]
        [MaxLength(50)]
        public string UserNIDFile { get; set; }

        [Column("PostalAddress", Order = 19)]
        [MaxLength(250)]
        [Display(Name = "Postal Address (English)")]
        public string PostalAddress { get; set; }

        [Column("PostalAddressBn", Order = 20)]
        [MaxLength(500)]
        [Display(Name = "পত্রের ঠিকানা (বাংলায়)")]
        public string PostalAddressBn { get; set; }

        [InverseProperty("AdminModUsersDetail_SenderId")]
        public virtual List<CcModProjectQueryDetail> AdminModUsersDetail_SenderId { get; set; }

        [InverseProperty("AdminModUsersDetail_ReceiverId")]
        public virtual List<CcModProjectQueryDetail> AdminModUsersDetail_ReceiverId { get; set; }
    }
}
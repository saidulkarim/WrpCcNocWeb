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
        [Column("UserFullName", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Name")]
        public string UserFullName { get; set; }


        [Required]
        [Column("UserFatherName", Order = 3)]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "Father's Name")]
        public string UserFatherName { get; set; }

        [Required]
        [Column("UserDateOfBirth", Order = 4)]
        [Display(Name = "Date Of Birth")]
        public DateTime UserDateOfBirth { get; set; }


        [Required]
        [Column("UserNID", Order = 5)]
        [Display(Name = "National ID No")]
        [MaxLength(20)]
        public string UserNID { get; set; }


        [Column("UserPassportNo", Order = 6)]
        [Display(Name = "Passport No")]
        [MaxLength(20)]
        public string UserPassportNo { get; set; }


        [Column("UserProfession", Order = 7)]
        [Display(Name = "Profession")]
        [MaxLength(50)]
        public string UserProfession { get; set; }


        [Column("UserDesignation", Order = 8)]
        [Display(Name = "Designation")]
        [MaxLength(50)]
        public string UserDesignation { get; set; }


        [Required]
        [Column("UserAddress", Order = 9)]
        [Display(Name = "Address")]
        [MaxLength(200)]
        public string UserAddress { get; set; }


        [Column("UserAlternateEmail", Order = 10)]
        [Display(Name = "Alternate Email")]
        [MaxLength(50)]
        public string UserAlternateEmail { get; set; }


        [Column("UserAlternateMobile", Order = 11)]
        [Display(Name = "Alternate Mobile")]
        [MaxLength(20)]
        public string UserAlternateMobile { get; set; }


        [Required]
        [Column("SecurityQuestionId", Order = 12)]
        [Display(Name = "Security Question")]
        public int SecurityQuestionId { get; set; }
        [ForeignKey("SecurityQuestionId")]
        public virtual LookUpAdminModSecurityQuestion lookUpAdminModSecurityQuestion { get; set; }


        [Required]
        [Column("SecurityQuestionAnswer", Order = 13)]
        [Display(Name = "Answer of Security Question")]
        [MaxLength(50)]
        public string SecurityQuestionAnswer { get; set; }

        [Display(Name = "Is Profile Submitted")]
        [Column("IsProfileSubmitted", Order = 14)]
        public int IsProfileSubmitted { get; set; }
    }
}

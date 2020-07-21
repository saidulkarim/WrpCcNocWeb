using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class AdminModUserRegistrationDetail
    {
        [Key]
        [Column("UserRegistrationId", Order = 0)]
        public long UserRegistrationId { get; set; }


        [Required]
        [Display(Name = "User Name")]
        [Column("UserName", Order = 1)]
        [MaxLength(20)]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "Password")]
        [Column("UserPassword", Order = 2)]
        [MaxLength(550)]
        public string UserPassword { get; set; }


        [Display(Name = "Activation Status")]
        [Column("UserActivationStatus", Order = 3)]
        public int? UserActivationStatus { get; set; }


        [Required]
        [Display(Name = "Email")]
        [Column("UserEmail", Order = 4)]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Please enter a valid email address.")]
        public string UserEmail { get; set; }


        [Required(ErrorMessage = "Mobile number is required.")]
        [MaxLength(11, ErrorMessage = "Please enter valid (11 digit) mobile number. e.g. 01511XXXXXX")]
        [MinLength(11, ErrorMessage = "Please enter valid (11 digit) mobile number. e.g. 01511XXXXXX")]
        [Display(Name = "Mobile")]
        [Column("UserMobile", Order = 5)]
        public string UserMobile { get; set; }


        [Display(Name = "Date Of Creation")]
        [Column("DateOfCreation", Order = 6)]
        public DateTime? DateOfCreation { get; set; }


        [Display(Name = "Last Modified Date")]
        [Column("LastModifiedDate", Order = 7)]
        public DateTime? LastModifiedDate { get; set; }


        [Display(Name = "Email Verification Code")]
        [Column("EmailVerificationCode", Order = 8)]
        [MaxLength(50)]
        public string EmailVerificationCode { get; set; }


        [Display(Name = "Is Verified by Email")]
        [Column("IsEmailVerified", Order = 9)]
        public int? IsEmailVerified { get; set; }


        [Display(Name = "Mobile Verification Code")]
        [Column("MobileVerificationCode", Order = 10)]
        [MaxLength(50)]
        public string MobileVerificationCode { get; set; }


        [Display(Name = "Is Verified by Mobile")]
        [Column("IsMobileVerified", Order = 11)]
        public int? IsMobileVerified { get; set; }

        [Display(Name = "Is Deleted")]
        [Column("IsDeleted", Order = 12)]
        public int? IsDeleted { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModIWRMCMemberDetail
    {
        [Key]
        [Column("IWRMCMemberDetailId", Order = 0)]
        public long IWRMCMemberDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("IWRMCMemberId", Order = 2)]
        [Display(Name = "IWRMC Member")]
        public int IWRMCMemberId { get; set; }
        [ForeignKey("IWRMCMemberId")]
        public virtual LookUpCcModIWRMCMember LookUpCcModIWRMCMember { get; set; }

        [Required]
        [Column("MemberTypeId", Order = 3)]
        [Display(Name = "Member Type")]
        public int MemberTypeId { get; set; }
        [ForeignKey("MemberTypeId")]
        public virtual LookUpCcModMemberType LookUpCcModMemberType { get; set; }

        [Required]
        [Display(Name = "Email")]
        [Column("MemberEmail", Order = 4)]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Please enter a valid email address.")]
        public string MemberEmail { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [MaxLength(11, ErrorMessage = "Please enter valid (11 digit) mobile number. e.g. 01511XXXXXX")]
        [MinLength(11, ErrorMessage = "Please enter valid (11 digit) mobile number. e.g. 01511XXXXXX")]
        [Display(Name = "Mobile")]
        [Column("MemberMobile", Order = 5)]
        public string MemberMobile { get; set; }
    }
}
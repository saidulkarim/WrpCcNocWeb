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
    }
}
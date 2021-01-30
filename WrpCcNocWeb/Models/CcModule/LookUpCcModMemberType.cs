using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModMemberType
    {
        [Key]
        [Column("MemberTypeId", Order = 0)]
        public int MemberTypeId { get; set; }

        [Required]
        [Column("MemberTypeName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Member Type")]
        public string MemberTypeName { get; set; }

        [Column("MemberTypeNameBn", Order = 2)]
        [MaxLength(250)]
        [Display(Name = "Member Type (Bangla)")]
        public string MemberTypeNameBn { get; set; }
    }
}
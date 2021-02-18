using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModIWRMCMember
    {
        [Key]
        [Column("IWRMCMemberId", Order = 0)]
        public int IWRMCMemberId { get; set; }

        [Required]
        [Column("UserLevelTypeId", Order = 1)]
        [Display(Name = "User Level Type")]
        public int UserLevelTypeId { get; set; }
        [ForeignKey("UserLevelTypeId")]
        public virtual LookUpCcModUserLevelType LookUpCcModUserLevelType { get; set; }

        [Column("AgencyId", Order = 2)]
        [Display(Name = "Agency")]
        public int? AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        public virtual LookUpAgency LookUpAgency { get; set; }

        [Column("Designation", Order = 3)]
        [MaxLength(150)]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Column("DesignationBn", Order = 4)]
        [MaxLength(250)]
        [Display(Name = "Designation (Bangla)")]
        public string DesignationBn { get; set; }

        [Column("IsCommitteeFormatter", Order = 5)]
        [Display(Name = "Is Committee Formatter?")]
        public int? IsCommitteeFormatter { get; set; }
    }
}
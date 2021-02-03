using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModAnyInterventionPrj
    {
        [Key]
        [Column("AnyInterventionPrjId", Order = 0)]
        public int AnyInterventionPrjId { get; set; }

        [Required]
        [Column("InterventionName", Order = 1)]
        [Display(Name = "Intervention Name")]
        [MaxLength(250)]
        public string InterventionName { get; set; }

        [Required]
        [Column("InterventionNameBn", Order = 2)]
        [Display(Name = "Intervention Name (Bangla)")]
        [MaxLength(500)]
        public string InterventionNameBn { get; set; }
    }
}
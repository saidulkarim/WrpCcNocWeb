using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModHighestFloodLevelDetail
    {
        [Key]
        [Column("HighestFloodLevelDetailId", Order = 0)]
        public long HighestFloodLevelDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("FloodYear", Order = 2)]
        [Display(Name = "Year")]
        public int FloodYear { get; set; }

        [Column("HighestFloodLevel", Order = 3)]
        [Display(Name = "Highest Flood Level (m)")]
        public double? HighestFloodLevel { get; set; }

        [Column("Datum", Order = 4)]
        [Display(Name = "Datum")]
        [MaxLength(10)]
        public string Datum { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAnnualRainfallDetail
    {
        [Key]
        [Column("AnnualRainfallDetailId", Order = 0)]
        public long AnnualRainfallDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("RainfallYear", Order = 2)]
        [Display(Name = "Year")]
        public int RainfallYear { get; set; }

        [Column("RainfallMm", Order = 3)]
        [Display(Name = "Rainfall (mm)")]
        public double? RainfallMm { get; set; }

        [Column("CollectedStationName", Order = 4)]
        [Display(Name = "Collected Station")]
        [MaxLength(250)]
        public string CollectedStationName { get; set; }

        [Column("Season", Order = 5)]
        [Display(Name = "Season")]
        [MaxLength(10)]
        public string Season { get; set; } //Dry or Wet
    }
}
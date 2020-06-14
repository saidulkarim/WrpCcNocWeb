using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModGroundWaterQualityDetail //Ground Water Quality
    {
        [Key]
        [Column("GroundWaterQualityDetailId", Order = 0)]
        public long GroundWaterQualityDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("GroundWaterQualityId", Order = 2)]
        [Display(Name = "Ground Water Quality")]
        public int GroundWaterQualityId { get; set; }
        [ForeignKey("GroundWaterQualityId")]
        public virtual LookUpCcModGroundWaterQuality LookUpCcModGroundWaterQuality { get; set; }
    }
}
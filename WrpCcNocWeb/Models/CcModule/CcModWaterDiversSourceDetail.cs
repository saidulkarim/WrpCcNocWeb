using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModWaterDiversSourceDetail //Water Diversion from Source
    {
        [Key]
        [Column("WaterDiversSourceDetailId", Order = 0)]
        public long WaterDiversSourceDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("WaterDiversionSourceId", Order = 2)]
        [Display(Name = "Water Diversion Source")]
        public int WaterDiversionSourceId { get; set; }
        [ForeignKey("WaterDiversionSourceId")]
        public virtual LookUpCcModWtrDiversionSource LookUpCcModWtrDiversionSource { get; set; }
    }
}
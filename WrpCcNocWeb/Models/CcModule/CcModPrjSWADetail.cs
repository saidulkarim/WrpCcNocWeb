using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjSWADetail
    {
        [Key]
        [Column("SWAId", Order = 0)]
        public long SWAId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Id")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("MonthId", Order = 2)]
        [Display(Name = "Month")]
        public int MonthId { get; set; }
        [ForeignKey("MonthId")]
        public virtual LookUpCcModMonth LookUpCcModMonth { get; set; }

        [Column("MinWaterFlow", Order = 3)]
        [Display(Name = "Minimum Water Flow (m3/s)")]
        public double? MinWaterFlow { get; set; }
        
        [Column("WaterDemandMonth", Order = 4)]
        [Display(Name = "Water Demand per Month (m3)")]
        public double? WaterDemandMonth { get; set; }        
    }
}
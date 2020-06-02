using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjGrndWtrDepthDetail
    {
        [Key]
        [Column("GrndWtrDepthDetailId", Order = 0)]
        public long GrndWtrDepthDetailId { get; set; }

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

        [Column("WaterDepth", Order = 3)]
        [Display(Name = "Ground Water Depth (m)")]
        public double? WaterDepth  { get; set; }      
    }
}
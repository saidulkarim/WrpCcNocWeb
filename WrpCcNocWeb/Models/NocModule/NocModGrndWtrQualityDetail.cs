using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModGrndWtrQualityDetail
    {
        [Key]
        [Column("GrndWtrQualityDetailId", Order = 0)]
        public long GrndWtrQualityDetailId { get; set; }
        
        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual NocModAppCommonDetail NocModAppCommonDetail { get; set; }
        
        [Column("GroundWaterQualityId", Order = 2)]        
        [Display(Name = "Water Quality")]
        public int? GroundWaterQualityId { get; set; }

        [Column("ParamsResult", Order = 3)]        
        [Display(Name = "Parameter Result")]
        public double? ParamsResult { get; set; }     
    }
}
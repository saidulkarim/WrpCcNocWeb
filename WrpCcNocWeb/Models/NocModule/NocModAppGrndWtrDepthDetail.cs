using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.CcModule;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModAppGrndWtrDepthDetail
    {
        [Key]
        [Column("GrndWtrDepthDetailId", Order = 0)]
        public long GrndWtrDepthDetailId { get; set; }
        
        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual NocModAppCommonDetail NocModAppCommonDetail { get; set; }
        
        [Column("MonthId", Order = 2)]        
        [Display(Name = "Month")]
        public int? MonthId { get; set; }
		[ForeignKey("MonthId")]
        public virtual LookUpCcModMonth LookUpCcModMonth { get; set; }
		
        [Column("WaterDepth", Order = 3)]        
        [Display(Name = "WaterDepth")]
        public double? WaterDepth { get; set; }     
    }
}
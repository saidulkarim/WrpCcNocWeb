using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModSurfaceWaterAvailDetail
    {
        [Key]
        [Column("SurfWaterAvailDetailId", Order = 0)]
        public long SurfWaterAvailDetailId { get; set; }
        
        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual NocModAppCommonDetail NocModAppCommonDetail { get; set; }
        
        [Column("HydroSystemId", Order = 2)]        
        [Display(Name = "Hydro System")]
        public int? HydroSystemId { get; set; }

        [Column("DistanceFromProposedWell", Order = 3)]        
        [Display(Name = "Distance from Proposed Tube-Well")]
        public double? DistanceFromProposedWell { get; set; }  

		[Column("WaterLevel", Order = 4)]        
        [Display(Name = "Water Level")]
        public double? WaterLevel { get; set; }

		[Column("DischargeAmount", Order = 5)]        
        [Display(Name = "Discharge Amount")]
        public double? DischargeAmount { get; set; }		
    }
}
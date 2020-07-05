using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_311_IndvDetail
    {
        [Key]
        [Column("Project311IndvId", Order = 0)]
        public long Project311IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Id")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }               
       
        [Column("HighestFloodLevel", Order = 2)]
        [Display(Name = "Highest Flood Level")]
        [MaxLength(250)]
        public string HighestFloodLevel  { get; set; }
        
		[Column("WaterDepthDrySeason", Order = 3)]
        [Display(Name = "Water Depth Dry Season")]
        public double? WaterDepthDrySeason { get; set; }

        [Column("WaterDepthWetSeason", Order = 4)]
        [Display(Name = "Water Level Wet Min")]
        public double? WaterDepthWetSeason { get; set; }
		
		[Column("FishWaterAreaId", Order = 5)]
        [Display(Name = "Fish WaterArea")]
        public int? FishWaterAreaId { get; set; }
        [ForeignKey("FishWaterAreaId")]
        public virtual LookUpCcModFishWaterArea LookUpCcModFishWaterArea { get; set; }
		
		[Column("WaterSalinity", Order = 6)]
        [Display(Name = "Salinity (ppm)")]
        [MaxLength(50)]
        public string WaterSalinity { get; set; }

        [Column("WaterDO", Order = 7)]
        [Display(Name = "DO (mg/l)")]
        [MaxLength(50)]
        public string WaterDO { get; set; }

        [Column("WaterTDS", Order = 8)]
        [Display(Name = "TDS")]
        [MaxLength(50)]
        public string WaterTDS { get; set; }

        [Column("WaterPhLevel", Order = 9)]
        [Display(Name = "pH")]
        [MaxLength(50)]
        public string WaterPhLevel { get; set; }
		
		[Column("TypeFishSpecies", Order = 10)]
        [Display(Name = "Type of Fish Species")]
        [MaxLength(250)]
        public string TypeFishSpecies { get; set; }
		
		[Column("IntervalFishRelease", Order = 11)]
        [Display(Name = "Interval of Fish Release")]
        [MaxLength(250)]
        public string IntervalFishRelease { get; set; }
		
		[Column("FishInspection", Order = 12)]
        [Display(Name = "Fish Inspection, Quality control and Management")]
        [MaxLength(250)]
        public string FishInspection { get; set; }
		
		[Column("FishFeed", Order = 13)]
        [Display(Name = "Fish Feed")]
        [MaxLength(250)]
        public string FishFeed { get; set; }
					
		[Column("FishHabitatArea", Order = 14)]
        [Display(Name = "Area (ha)")]        
        public double? FishHabitatArea { get; set; }
		
		[Column("FishHabitatProduction", Order = 15)]
        [Display(Name = "Production (Ton)")]        
        public double? FishHabitatProduction { get; set; }
				
		[Column("ExportFishQuantity", Order = 16)]
        [Display(Name = "Quantity")]        
        public double? ExportFishQuantity { get; set; }
		
		[Column("ExportFishValue", Order = 17)]
        [Display(Name = "Value (BDT)")]        
        public double? ExportFishValue { get; set; }		
    }
}
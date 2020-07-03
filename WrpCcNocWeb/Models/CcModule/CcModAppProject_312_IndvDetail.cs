using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_312_IndvDetail
    {
        [Key]
        [Column("Project312IndvId", Order = 0)]
        public long Project312IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Id")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }               
       
        [Column("WellDescription", Order = 2)]
        [Display(Name = "Well Description")]
        [MaxLength(250)]
        public string WellDescription { get; set; }
        
		[Column("TypeProposedWellId", Order = 3)]
        [Display(Name = "Type of Proposed Well")]
        public int? TypeProposedWellId { get; set; }
        [ForeignKey("TypeProposedWellId")]
        public virtual LookUpCcModTypeProposedWell LookUpCcModTypeProposedWell { get; set; }
		
		[Column("StatusOfAquifer", Order = 4)]
        [Display(Name = "Status of Aquifer for Existing Extraction")]
        [MaxLength(250)]
        public string StatusOfAquifer { get; set; }
		
		[Column("SedimentationId", Order = 5)]
        [Display(Name = "Potential Ground Water Recharge")]
        public int? SedimentationId { get; set; }
        [ForeignKey("SedimentationId")]
        public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfKhal_310 { get; set; }
		
		[Column("WaterWithdrawalTarget", Order = 6)]
        [Display(Name = "Water Withdrawal Target (ft3/sec)")]        
        public double? WaterWithdrawalTarget { get; set; }
		
		[Column("WaterWithdrawalProcedure", Order = 7)]
        [Display(Name = "Water Withdrawal Procedure")]
        [MaxLength(250)]
        public string WaterWithdrawalProcedure { get; set; }
		
		[Column("UseOfMeterToMeasure", Order = 8)]
        [Display(Name = "Use of Meter to Measure Groundwater Withdrawal")]
        [MaxLength(250)]
        public string UseOfMeterToMeasure { get; set; }
		
		[Column("PumpCapacity", Order = 9)]
        [Display(Name = "Pump Capacity (hp)")]        
        public double? PumpCapacity { get; set; }
		
		[Column("WellDepth", Order = 10)]
        [Display(Name = "Well Depth (ft)")]        
        public double? WellDepth { get; set; }
		
		[Column("PipeDiameterOfWell", Order = 11)]
        [Display(Name = "Pipe Diameter of Well (inch)")]        
        public double? PipeDiameterOfWell { get; set; }
		
		[Column("WaterWithdrawalQtyDay ", Order = 12)]
        [Display(Name = "Water Withdrawal Quantity Per Day (m3/day)")]        
        public double? WaterWithdrawalQtyDay { get; set; }
		
		[Column("RechargeTime", Order = 13)]
        [Display(Name = "Recharge Time")]
        [MaxLength(150)]
        public string RechargeTime { get; set; }
				
		[Column("WaterUseConsumptionId", Order = 14)]
        [Display(Name = "Water Use")]
        public int? WaterUseConsumptionId { get; set; }
        [ForeignKey("WaterUseConsumptionId")]
        public virtual LookUpCcModWaterUseConsumption LookUpCcModWaterUseConsumption { get; set; }
		
		[Column("DischargePoint", Order = 15)]
        [Display(Name = "Discharge Point (if any)")]
        [MaxLength(250)]
        public string DischargePoint { get; set; }
				
		[Column("DistanceProposedExtractWell", Order = 16)]
        [Display(Name = "Distance (m) from Proposed Extraction Well")]        
        public double? DistanceProposedExtractWell { get; set; }
		
		[Column("Place", Order = 17)]
        [Display(Name = "Place")]
        [MaxLength(250)]
        public string Place { get; set; }
		
		[Column("WellType", Order = 18)]
        [Display(Name = "Well Type")]
        [MaxLength(150)]
        public string WellType { get; set; }
		
		[Column("Capacity", Order = 19)]
        [Display(Name = "Capacity")]       
        public double? Capacity { get; set; }
		
		[Column("DiameterOfWell", Order = 20)]
        [Display(Name = "Diameter of Well (m)")]      
        public double? DiameterOfWell { get; set; }
		
		[Column("DepthOfWell", Order = 21)]
        [Display(Name = "Depth of Well (m)")]       
        public double? DepthOfWell { get; set; }
		
		[Column("RecuperationHours", Order = 22)]
        [Display(Name = "Recuperation Hours")]
        [MaxLength(250)]
        public string RecuperationHours { get; set; }
		
		[Column("RiverKhalName", Order = 23)]
        [Display(Name = "River/ Khal")]
        [MaxLength(250)]
        public string RiverKhalName { get; set; }
		
		[Column("NearestSurfWaterAvailDistance", Order = 24)]
        [Display(Name = "Distance (m) from Proposed Extraction Well")]       
        public double? NearestSurfWaterAvailDsitance { get; set; }
		
		[Column("WaterLevel", Order = 25)]
        [Display(Name = "Water Level (m)")]       
        public double? WaterLevel { get; set; }
		
		[Column("Discharge", Order = 26)]
        [Display(Name = "Discharge (m3/s)")]       
        public double? Discharge { get; set; }
		
		[Column("CommandAreaOfWell", Order = 27)]
        [Display(Name = "Command Area of Well (Ha)")]       
        public double? CommandAreaOfWell { get; set; }
		
		[Column("OtherAvailableSource", Order = 28)]
        [Display(Name = "Other Available Source")]
        [MaxLength(250)]
        public string OtherAvailableSource { get; set; }
		
		[Column("FutureAvailability", Order = 29)]
        [Display(Name = "Future Availability at Withdrawal Point")]
        [MaxLength(250)]
        public string FutureAvailability { get; set; }
		
		[Column("CultivableCrops", Order = 30)]
        [Display(Name = "Cultivable Crops")]
        [MaxLength(250)]
        public string CultivableCrops { get; set; }
		
		[Column("AnnualRequirementOfGW", Order = 31)]
        [Display(Name = "Annual Requirement of GW for Crop Production")]
        [MaxLength(250)]
        public string AnnualRequirementOfGW  { get; set; }
		
		[Column("CropProduction", Order = 32)]
        [Display(Name = "Crop Production (Ton)")]       
        public double? CropProduction { get; set; }			
    }
}
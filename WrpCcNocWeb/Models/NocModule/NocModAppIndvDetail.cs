using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.AdminModule;
using WrpCcNocWeb.Models.CcModule;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModAppIndvDetail
    {
        [Key]
        [Column("NocAppIndvId", Order = 0)]
        public long NocAppIndvId { get; set; }
        
        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual NocModAppCommonDetail NocModAppCommonDetail { get; set; }

        [Column("InstallationPlaceSuitability", Order = 2)]
        [Display(Name = "Suitability of the Place for Tube Well Installation")]
		[MaxLength(250)]
        public string InstallationPlaceSuitability { get; set; }
		
		[Column("WaterWithdrawalTarget", Order = 3)]
        [Display(Name = "Water withdrawal target (ft³/sec)")]
        public double? WaterWithdrawalTarget { get; set; }
		
		[Column("PumpCapacity", Order = 4)]
        [Display(Name = "Pump Capacity (hp)")]
        public double? PumpCapacity { get; set; }
		
		[Column("WellDepth", Order = 5)]
        [Display(Name = "Well depth (ft)")]
        public double? WellDepth { get; set; }
		
		[Column("WellPipeDiameter", Order = 6)]
        [Display(Name = "Pipe Diameter of Well (inch)")]
        public double? WellPipeDiameter { get; set; }
		
		[Column("MaxWaterWithdrawalQuantity", Order = 7)]
        [Display(Name = "Maximum Water Withdrawal Quantity Per Day (m³/day)")]
        public double? MaxWaterWithdrawalQuantity { get; set; }
		
		[Column("TypeOfMeterUseToMeasure", Order = 8)]
        [Display(Name = "Type of Meter Use to Measure Groundwater Withdrawal")]
		[MaxLength(250)]
        public string TypeOfMeterUseToMeasure { get; set; }
		
		[Column("CommandAreaOfWell", Order = 9)]
        [Display(Name = "Command Area of Well (Ha) (for irrigation purpose)")]
        public double? CommandAreaOfWell { get; set; }
		
		[Column("AquiferType", Order = 10)]
        [Display(Name = "Aquifer Type")]
		[MaxLength(100)]
        public string AquiferType { get; set; }
		
		[Column("Lithology", Order = 11)]
        [Display(Name = "Lithology")]
		[MaxLength(100)]
        public string Lithology { get; set; }
		
		[Column("ScreenDepth", Order = 12)]
        [Display(Name = "Screen Depth")]
		[MaxLength(100)]
        public string ScreenDepth { get; set; }
		
		[Column("AquiferThickness", Order = 13)]
        [Display(Name = "Aquifer Thickness")]
		[MaxLength(100)]
        public string AquiferThickness { get; set; }
		
		[Column("AreaOfInfluence", Order = 14)]
        [Display(Name = "Area of Influence")]
		[MaxLength(100)]
        public string AreaOfInfluence { get; set; }
		
		[Column("AquiferStsExistingExtract", Order = 15)]
        [Display(Name = "Status of Aquifer for Existing Extraction")]
		[MaxLength(250)]
        public string AquiferStsExistExtract { get; set; }
		
		[Column("AquiferCategoryId", Order = 16)]
        [Display(Name = "Categorization of Aquifer")]
        public int? AquiferCategoryId { get; set; }
		
		[Column("WaterWithdrawSafeLimitYesNoId", Order = 17)]
        [Display(Name = "Categorization of Aquifer")]
        public int? WaterWithdrawSafeLimitYesNoId { get; set; }
				
		[Column("RechargeTime", Order = 18)]
        [Display(Name = "Recharge Time")]
		[MaxLength(100)]
        public string RechargeTime { get; set; }
		
		[Column("DischargePointLocation", Order = 19)]
        [Display(Name = "Location")]
		[MaxLength(250)]
        public string DischargePointLocation { get; set; }
		
		[Column("DischargePointLatitude", Order = 20)]
        [Display(Name = "Latitude")]
        public double? DischargePointLatitude { get; set; }
		
		[Column("DischargePointLongitude", Order = 21)]
        [Display(Name = "Longitude")]
        public double? DischargePointLongitude { get; set; }
		
		[Column("NearestWellLocation", Order = 22)]
        [Display(Name = "Nearest Well Location")]
		[MaxLength(250)]
        public string NearestWellLocation { get; set; }
		
		[Column("DistanceFromProposedWell", Order = 23)]
        [Display(Name = "Distance (m) from Proposed Extraction Well")]
		[MaxLength(250)]
        public string DistanceFromProposedWell { get; set; }
		
		[Column("WaterLevel", Order = 24)]
        [Display(Name = "Water level (m)")]
        public int? WaterLevel { get; set; }
		
		[Column("Discharge", Order = 24)]
        [Display(Name = "Discharge (m³/s)")]
        public double? Discharge { get; set; }
		
		[Column("FutureGroundWaterAvailability", Order = 25)]
        [Display(Name = "Future Ground Water Availability at Withdrawal Point")]
		[MaxLength(250)]
        public string FutureGroundWaterAvailability { get; set; }
		
		[Column("BeneficiaryAreaProposedWell", Order = 26)]
        [Display(Name = "Beneficiary Area for the Proposed Tube Well")]
		[MaxLength(250)]
        public string BeneficiaryAreaProposedWell { get; set; }
		
		[Column("ProbableImpactOnExisting", Order = 27)]
        [Display(Name = "Probable Impact on Existing Tube Wells for Proposed Well")]
		[MaxLength(250)]
        public string ProbableImpactOnExisting { get; set; }
		
		[Column("ProbableImpactOnSurroundingEnv", Order = 28)]
        [Display(Name = "Probable Impact on Surrounding Environment, Ground Water Availability and Quality")]
		[MaxLength(250)]
        public string ProbableImpactOnSurroundingEnv { get; set; }
		
		[Column("StepsTakenForGrndWtrRecharge", Order = 29)]
        [Display(Name = "Steps Taken for Potential Ground Water Recharge")]
		[MaxLength(250)]
        public string StepsTakenForGrndWtrRecharge { get; set; }
    }
}
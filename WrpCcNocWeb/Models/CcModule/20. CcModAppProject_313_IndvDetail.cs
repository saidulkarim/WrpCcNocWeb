using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_313_IndvDetail
    {
		[Key]
		[Column("Project313IndvId", Order = 0)]
		public long Project313IndvId { get; set; }

		[Required]
		[Column("ProjectId", Order = 1)]
		[Display(Name = "Project Id")]
		public long ProjectId { get; set; }
		[ForeignKey("ProjectId")]
		public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }               

		[Column("ConnectivityAmidWaterland", Order = 2)]
		[Display(Name = "Connectivity amid khals, river and wetland")]
		[MaxLength(50)]
		public string ConnectivityAmidWaterland { get; set; }

		[Column("CatchmentArea", Order = 3)]
		[Display(Name = "Catchment Area (ha)")]
		public double? CatchmentArea { get; set; }

		[Column("WaterLevelDryMax", Order = 4)]
		[Display(Name = "Water Level Dry Max")]
		public double? WaterLevelDryMax { get; set; }

		[Column("WaterLevelDryMin", Order = 5)]
		[Display(Name = "Water Level Dry Min")]
		public double? WaterLevelDryMin { get; set; }

		[Column("WaterLevelWetMax", Order = 6)]
		[Display(Name = "Water Level Wet Max")]
		public double? WaterLevelWetMax { get; set; }

		[Column("WaterLevelWetMin", Order = 7)]
		[Display(Name = "Water Level WetMin")]
		public double? WaterLevelWetMin { get; set; }

		[Column("DischargeDryMax", Order = 8)]
		[Display(Name = "Discharge Dry Max")]
		public double? DischargeDryMax { get; set; }

		[Column("DischargeDryMin", Order = 9)]
		[Display(Name = "Discharge Dry Min")]
		public double? DischargeDryMin { get; set; }

		[Column("DischargeWetMax", Order = 10)]
		[Display(Name = "Discharge Wet Max")]
		public double? DischargeWetMax { get; set; }

		[Column("DischargeWetMin", Order = 11)]
		[Display(Name = "Discharge Wet Min")]
		public double? DischargeWetMin { get; set; }

		[Column("BankErosionLength", Order = 12)]
		[Display(Name = "Length (m)")]        
		public double? BankErosionLength { get; set; }

		[Column("BankErosionArea", Order = 13)]
		[Display(Name = "Area (ha)")]        
		public double? BankErosionArea { get; set; }

		[Column("BankErosionLocation", Order = 14)]
		[Display(Name = "Location")]
		[MaxLength(150)]
		public string BankErosionLocation { get; set; }

		[Column("BankStabilityTypeId", Order = 15)]
		[Display(Name = "Bank Stability")]
		public int? BankStabilityTypeId { get; set; }
		[ForeignKey("BankStabilityTypeId")]
		public virtual LookUpCcModBankStability LookUpCcModBankStability { get; set; }

		[Column("AccretionLength", Order = 16)]
		[Display(Name = "Length (m)")]        
		public double? AccretionLength { get; set; }

		[Column("AccretionArea", Order = 17)]
		[Display(Name = "Area (ha)")]        
		public double? AccretionArea { get; set; }

		[Column("AccretionLocation", Order = 18)]
		[Display(Name = "Location")]
		[MaxLength(150)]
		public string AccretionLocation { get; set; }

		[Column("SedimentationId", Order = 19)]
		[Display(Name = "Sedimentation")]
		public int? SedimentationId { get; set; }
		[ForeignKey("SedimentationId")]
		public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfRiverOrKhal { get; set; }

		[Column("SedimentationRate", Order = 20)]
		[Display(Name = "Sedimentation Rate")]
		public double? SedimentationRate { get; set; }

		[Column("KhalTypeId", Order = 21)]
		[Display(Name = "Khal Type")]
		public int? KhalTypeId { get; set; }
		[ForeignKey("KhalTypeId")]
		public virtual LookUpCcModKhalType LookUpCcModKhalType { get; set; }

		[Column("DrainageConditionId", Order = 22)]
		[Display(Name = "Drainage Condition")]
		public int? DrainageConditionId { get; set; }
		[ForeignKey("DrainageConditionId")]
		public virtual LookUpCcModDrainageCondition LookUpCcModDrainageCondition { get; set; }

		[Column("HighLandPercent", Order = 23)]
		[Display(Name = "High Land F0 (0 - 30 cm)")]
		public double? HighLandPercent { get; set; }

		[Column("MediumHighLandPercent", Order = 24)]
		[Display(Name = "Medium High Land F1 (30 - 90 cm)")]
		public double? MediumHighLandPercent { get; set; }

		[Column("MediumLowLandPercent", Order = 25)]
		[Display(Name = "Medium Low Land F2 (90 - 180 cm)")]
		public double? MediumLowLandPercent { get; set; }

		[Column("LowLandPercent", Order = 26)]
		[Display(Name = "Low Land F3 (> 180 - 360 cm)")]
		public double? LowLandPercent { get; set; }

		[Column("VeryLowLandPercent", Order = 27)]
		[Display(Name = "Very Low Land F4 (> 360 cm)")]
		public double? VeryLowLandPercent { get; set; }

		[Column("CultivableCrops", Order = 28)]
		[Display(Name = "Cultivable Crops")]
		[MaxLength(50)]
		public string CultivableCrops { get; set; }

		[Column("CropProduction", Order = 29)]
		[Display(Name = "Crop Production")]
		public int? CropProduction { get; set; }

		[Column("FishProduction", Order = 30)]
		[Display(Name = "Fish Production")]
		public int? FishProduction { get; set; }

		[Column("FishDiversity", Order = 31)]
		[Display(Name = "Fish Diversity")]
		[MaxLength(50)]
		public string FishDiversity { get; set; }

		[Column("FishMigration", Order = 32)]
		[Display(Name = "Fish Migration")]
		[MaxLength(50)]
		public string FishMigration { get; set; }

		[Column("FloraAndFauna", Order = 33)]
		[Display(Name = "Flora And Fauna")]
		[MaxLength(50)]
		public string FloraAndFauna { get; set; }

		[Column("LandLessPeoplePercentage", Order = 34)]
		[Display(Name = "Land Less People Percentage")]
		public double? LandLessPeoplePercentage { get; set; }

		[Column("SmallFarmerPercentage", Order = 35)]
		[Display(Name = "Small Farmer Percentage")]
		public double? SmallFarmerPercentage { get; set; }

		[Column("AvgMonthlyIncome", Order = 36)]
		[Display(Name = "Avg Monthly Income")]
		public int? AvgMonthlyIncome { get; set; }

		[Column("DuplicatYesNoId", Order = 37)]
		[Display(Name = "Was there any Duplication")]
		public int? DuplicatYesNoId { get; set; }
		[ForeignKey("DuplicatYesNoId")]
		public virtual LookUpCcModYesNo LookUpYesNoDuplication_313 { get; set; }

		[Column("DuplicationApplicantComments", Order = 38)]
		[Display(Name = "Duplication Applicant Comments")]
		[MaxLength(150)]
		public string DuplicationApplicantComments { get; set; }

		[Column("DuplicationAuthorityComments", Order = 39)]
		[Display(Name = "Duplication Authority Comments")]
		[MaxLength(150)]
		public string DuplicationAuthorityComments { get; set; }	   	  
    }
}
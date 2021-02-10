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

		[Column("BankErosionRate", Order = 15)]
		[Display(Name = "Bank Erosion Rate")]
		public double? BankErosionRate { get; set; }

		[Column("BankStabilityTypeId", Order = 16)]
		[Display(Name = "Bank Stability")]
		public int? BankStabilityTypeId { get; set; }
		[ForeignKey("BankStabilityTypeId")]
		public virtual LookUpCcModBankStability LookUpCcModBankStability { get; set; }

		[Column("CharAccretionLength", Order = 17)]
		[Display(Name = "Length (m)")]        
		public double? CharAccretionLength { get; set; }

		[Column("CharAccretionArea", Order = 18)]
		[Display(Name = "Area (ha)")]        
		public double? CharAccretionArea { get; set; }

		[Column("CharAccretionLocation", Order = 19)]
		[Display(Name = "Location")]
		[MaxLength(150)]
		public string CharAccretionLocation { get; set; }

		[Column("SedimentationId", Order = 20)]
		[Display(Name = "Sedimentation")]
		public int? SedimentationId { get; set; }
		[ForeignKey("SedimentationId")]
		public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfRiverOrKhal { get; set; }

		[Column("SedimentationRate", Order = 21)]
		[Display(Name = "Sedimentation Rate")]
		public double? SedimentationRate { get; set; }

		[Column("KhalTypeId", Order = 22)]
		[Display(Name = "Khal Type")]
		public int? KhalTypeId { get; set; }
		[ForeignKey("KhalTypeId")]
		public virtual LookUpCcModKhalType LookUpCcModKhalType { get; set; }

		[Column("DrainageConditionId", Order = 23)]
		[Display(Name = "Drainage Condition")]
		public int? DrainageConditionId { get; set; }
		[ForeignKey("DrainageConditionId")]
		public virtual LookUpCcModDrainageCondition LookUpCcModDrainageCondition { get; set; }

		[Column("HighLandPercent", Order = 24)]
		[Display(Name = "High Land F0 (0 - 30 cm)")]
		public double? HighLandPercent { get; set; }

		[Column("MediumHighLandPercent", Order = 25)]
		[Display(Name = "Medium High Land F1 (30 - 90 cm)")]
		public double? MediumHighLandPercent { get; set; }

		[Column("MediumLowLandPercent", Order = 26)]
		[Display(Name = "Medium Low Land F2 (90 - 180 cm)")]
		public double? MediumLowLandPercent { get; set; }

		[Column("LowLandPercent", Order = 27)]
		[Display(Name = "Low Land F3 (> 180 - 360 cm)")]
		public double? LowLandPercent { get; set; }

		[Column("VeryLowLandPercent", Order = 28)]
		[Display(Name = "Very Low Land F4 (> 360 cm)")]
		public double? VeryLowLandPercent { get; set; }

		[Column("CultivableCrops", Order = 29)]
		[Display(Name = "Cultivable Crops")]
		[MaxLength(250)]
		public string CultivableCrops { get; set; }

		[Column("CropProduction", Order = 30)]
		[Display(Name = "Crop Production")]
		public double? CropProduction { get; set; }

		[Column("FishProduction", Order = 31)]
		[Display(Name = "Fish Production")]
		public double? FishProduction { get; set; }

		[Column("FishDiversity", Order = 32)]
		[Display(Name = "Fish Diversity")]
		[MaxLength(50)]
		public string FishDiversity { get; set; }

		[Column("FishMigration", Order = 33)]
		[Display(Name = "Fish Migration")]
		[MaxLength(50)]
		public string FishMigration { get; set; }

		[Column("FloraAndFauna", Order = 34)]
		[Display(Name = "Flora And Fauna")]
		[MaxLength(50)]
		public string FloraAndFauna { get; set; }

		//[Column("LandLessPeoplePercentage", Order = 34)]
		//[Display(Name = "Land Less People Percentage")]
		//public double? LandLessPeoplePercentage { get; set; }

		//[Column("SmallFarmerPercentage", Order = 35)]
		//[Display(Name = "Small Farmer Percentage")]
		//public double? SmallFarmerPercentage { get; set; }

		//[Column("AvgMonthlyIncome", Order = 36)]
		//[Display(Name = "Avg Monthly Income")]
		//public int? AvgMonthlyIncome { get; set; }

		[Column("DuplicatYesNoId", Order = 38)]
		[Display(Name = "Was there any Duplication")]
		public int? DuplicatYesNoId { get; set; }
		[ForeignKey("DuplicatYesNoId")]
		public virtual LookUpCcModYesNo LookUpYesNoDuplication_313 { get; set; }

		[Column("DuplicationApplicantComments", Order = 39)]
		[Display(Name = "Duplication Applicant Comments")]
		[MaxLength(150)]
		public string DuplicationApplicantComments { get; set; }

		[Column("DuplicationAuthorityComments", Order = 40)]
		[Display(Name = "Duplication Authority Comments")]
		[MaxLength(150)]
		public string DuplicationAuthorityComments { get; set; }

		[Column("UseOfToolsYesNoId", Order = 41)]
		[Display(Name = "Was there any Use Of Tools")]
		public int? UseOfToolsYesNoId { get; set; }
		[ForeignKey("UseOfToolsYesNoId")]
		public virtual LookUpCcModYesNo LookUpYesNoUseOfTool_41 { get; set; }

		[Column("ToolsApplicantComments", Order = 42)]
		[Display(Name = "Tools Applicant Comments")]
		[MaxLength(150)]
		public string ToolsApplicantComments { get; set; }

		[Column("ToolsAuthorityComments", Order = 43)]
		[Display(Name = "Tools Authority Comments")]
		[MaxLength(150)]
		public string ToolsAuthorityComments { get; set; }

		[Column("CropProductionAmount", Order = 44)]
		[Display(Name = "Crop Production Amount (Taka)")]
		public double? CropProductionAmount { get; set; }

		[Column("TotalFishProduction", Order = 45)]
		[Display(Name = "Total Fish Production (Ton)")]
		public double? TotalFishProduction { get; set; }

		[Column("UseOfAppropToolsDescription", Order = 46)]
		[Display(Name = "Short Description, If Yes")]
		[MaxLength(500)]
		public string UseOfAppropToolsDescription { get; set; }
	}
}
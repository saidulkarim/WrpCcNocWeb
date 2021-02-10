using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_38_IndvDetail
    {
        [Key]
        [Column("Project38IndvId", Order = 0)]
        public long Project38IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Id")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }               
       
        [Column("NameOfRiverWaterCourse", Order = 2)]
        [Display(Name = "Name of River/ Water Course")]
        [MaxLength(250)]
        public string NameOfRiverWaterCourse { get; set; }
        
		[Column("RiverNatureId", Order = 3)]
        [Display(Name = "River Nature")]
        public int? RiverNatureId { get; set; }
        [ForeignKey("RiverNatureId")]
        public virtual LookUpCcModRiverNature LookUpCcModRiverNature { get; set; }
		
		[Column("BankErosionLength", Order = 4)]
        [Display(Name = "Length (m)")]        
        public double? BankErosionLength { get; set; }
		
		[Column("BankErosionArea", Order = 5)]
        [Display(Name = "Area (ha)")]        
        public double? BankErosionArea { get; set; }
		
		[Column("BankErosionLocation", Order = 6)]
        [Display(Name = "Location")]
        [MaxLength(150)]
        public string BankErosionLocation { get; set; }
		
		[Column("BankErosionRate", Order = 7)]
        [Display(Name = "Erosion Rate (m/year)")]        
        public double? BankErosionRate { get; set; }
		
		[Column("SetBackDistanceImpStruct", Order = 8)]
        [Display(Name = "Set Back Distance from Important Structure/ Road/ Building/Local Habitation")]
        [MaxLength(250)]
        public string SetBackDistanceImpStruct { get; set; }
		
		[Column("SoilTypeRiverBank", Order = 9)]
        [Display(Name = "Soil Type of River Bank")]
        [MaxLength(250)]
        public string SoilTypeRiverBank { get; set; }
		
		[Column("SedimentationId", Order = 10)]
        [Display(Name = "Sedimentation")]
        public int? SedimentationId { get; set; }
        [ForeignKey("SedimentationId")]
        public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfRiverOrKhal { get; set; }
		
		[Column("CharArea", Order = 11)]
        [Display(Name = "Area (ha)")]        
        public double? CharArea { get; set; }
		
		[Column("CharLocation", Order = 12)]
        [Display(Name = "Location")]
        [MaxLength(150)]
        public string CharLocation { get; set; }
		
		[Column("IrrigatedCropArea", Order = 13)]
        [Display(Name = "Irrigated Crop Area (ha)")]        
        public double? IrrigatedCropArea { get; set; }
		
		[Column("CropProduction", Order = 14)]
        [Display(Name = "Crop Production (Ton)")]        
        public double? CropProduction { get; set; }
		
		[Column("FloraAndFauna", Order = 15)]
        [Display(Name = "Flora And Fauna")]
        [MaxLength(250)]
        public string FloraAndFauna { get; set; }
				
		[Column("SecLandLessPeople", Order = 16)]
        [Display(Name = "% of Land Less People")]        
        public double? SecLandLessPeople { get; set; }
		
		[Column("SecSmallFarmer", Order = 17)]
        [Display(Name = "% of Small Farmer")]        
        public double? SecSmallFarmer { get; set; }
		
		[Column("SecAvgMonthlyIncome", Order = 18)]
        [Display(Name = "Average Monthly Income")]        
        public double? SecAvgMonthlyIncome { get; set; }
		
		[Column("RiverDepth", Order = 19)]
        [Display(Name = "River Depth (m)")]        
        public double? RiverDepth  { get; set; }
		
		[Column("RiverWidth", Order = 20)]
        [Display(Name = "River Width (m)")]        
        public double? RiverWidth  { get; set; }
		
		[Column("SedimentationRate", Order = 21)]
        [Display(Name = "Sedimentation Rate")]
        public double? SedimentationRate { get; set; }
		
		[Column("WaterLevelDryMax", Order = 22)]
        [Display(Name = "Water Level Dry Max")]
        public double? WaterLevelDryMax { get; set; }

        [Column("WaterLevelDryMin", Order = 23)]
        [Display(Name = "Water Level Dry Min")]
        public double? WaterLevelDryMin { get; set; }

        [Column("WaterLevelWetMax", Order = 24)]
        [Display(Name = "Water Level Wet Max")]
        public double? WaterLevelWetMax { get; set; }

        [Column("WaterLevelWetMin", Order = 25)]
        [Display(Name = "Water Level WetMin")]
        public double? WaterLevelWetMin { get; set; }

        [Column("DischargeDryMax", Order = 26)]
        [Display(Name = "Discharge Dry Max")]
        public double? DischargeDryMax { get; set; }

        [Column("DischargeDryMin", Order = 27)]
        [Display(Name = "Discharge Dry Min")]
        public double? DischargeDryMin { get; set; }

        [Column("DischargeWetMax", Order = 28)]
        [Display(Name = "Discharge Wet Max")]
        public double? DischargeWetMax { get; set; }

        [Column("DischargeWetMin", Order = 29)]
        [Display(Name = "Discharge Wet Min")]
        public double? DischargeWetMin { get; set; }
		
		[Column("BankLineShiftingId", Order = 30)]
        [Display(Name = "Bank Line Shifting")]
        public int? BankLineShiftingId { get; set; }
        [ForeignKey("BankLineShiftingId")]
        public virtual LookUpCcModBankLineShifting LookUpCcModBankLineShifting { get; set; }
		
		[Column("BankStabilityTypeId", Order = 31)]
        [Display(Name = "Bank Stability")]
        public int? BankStabilityTypeId { get; set; }
        [ForeignKey("BankStabilityTypeId")]
        public virtual LookUpCcModBankStability LookUpCcModBankStability { get; set; }
		
		[Column("UseOfToolsYesNoId", Order = 32)]
        [Display(Name = "Was there any Use Of Tools")]
        public int? UseOfToolsYesNoId { get; set; }
        [ForeignKey("UseOfToolsYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoUseOfTool_38 { get; set; }

        [Column("ToolsApplicantComments", Order = 33)]
        [Display(Name = "Tools Applicant Comments")]
        [MaxLength(150)]
        public string ToolsApplicantComments { get; set; }

        [Column("ToolsAuthorityComments", Order = 34)]
        [Display(Name = "Tools Authority Comments")]
        [MaxLength(150)]
        public string ToolsAuthorityComments { get; set; }
    }
}
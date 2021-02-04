using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_33_IndvDetail
    {
        [Key]
        [Column("Project33IndvId", Order = 0)]
        public long Project33IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Id")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Column("NameOfTheWaterBody", Order = 2)]
        [Display(Name = "Name Of The Water Body")]
        [MaxLength(250)]
        public string NameOfTheWaterBody { get; set; }

        [Column("WaterBodyTypeId", Order = 3)]
        [Display(Name = "Water Body Type Id")]
        public int? WaterBodyTypeId { get; set; }
        [ForeignKey("WaterBodyTypeId")]
        public virtual LookUpCcModTypeOfWaterBody LookUpCcModTypeOfWaterBody { get; set; }

        [Column("Offtake", Order = 4)]
        [Display(Name = "Offtake")]
        [MaxLength(250)]
        public string Offtake { get; set; }

        [Column("Outfall", Order = 5)]
        [Display(Name = "Outfall")]
        [MaxLength(250)]
        public string Outfall { get; set; }

        [Column("Connectivity", Order = 6)]
        [Display(Name = "Connectivity")]
        [MaxLength(250)]
        public string Connectivity { get; set; }

        [Column("ExistingUseOfWaterFromSource", Order = 7)]
        [Display(Name = "Existing Use Of Water From Source")]
        [MaxLength(250)]
        public string ExistingUseOfWaterFromSource { get; set; }

        [Column("WaterLevelDryMax", Order = 8)]
        [Display(Name = "Water Level Dry Max")]
        public double? WaterLevelDryMax { get; set; }

        [Column("WaterLevelDryMin", Order = 9)]
        [Display(Name = "Water Level Dry Min")]
        public double? WaterLevelDryMin { get; set; }

        [Column("WaterLevelWetMax", Order = 10)]
        [Display(Name = "Water Level Wet Max")]
        public double? WaterLevelWetMax { get; set; }

        [Column("WaterLevelWetMin", Order = 11)]
        [Display(Name = "Water Level WetMin")]
        public double? WaterLevelWetMin { get; set; }

        [Column("DischargeDryMax", Order = 12)]
        [Display(Name = "Discharge Dry Max")]
        public double? DischargeDryMax { get; set; }

        [Column("DischargeDryMin", Order = 13)]
        [Display(Name = "Discharge Dry Min")]
        public double? DischargeDryMin { get; set; }

        [Column("DischargeWetMax", Order = 14)]
        [Display(Name = "Discharge Wet Max")]
        public double? DischargeWetMax { get; set; }

        [Column("DischargeWetMin", Order = 15)]
        [Display(Name = "Discharge Wet Min")]
        public double? DischargeWetMin { get; set; }

        [Column("CrossSectionDepth", Order = 16)]
        [Display(Name = "Cross Section Depth")]
        public double? CrossSectionDepth { get; set; }

        [Column("CrossSectionWidth", Order = 17)]
        [Display(Name = "Cross Section Width")]
        public double? CrossSectionWidth { get; set; }

        [Column("SedimentationId", Order = 18)]
        [Display(Name = "Sedimentation Id")]
        public int? SedimentationId { get; set; }
        [ForeignKey("SedimentationId")]
        public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfRiverOrKhal { get; set; }

        [Column("SedimentationRate", Order = 19)]
        [Display(Name = "Sedimentation Rate")]
        public double? SedimentationRate { get; set; }

        [Column("HighLandPercent", Order = 20)]
        [Display(Name = "High Land F0 (0-30 cm)")]
        public double? HighLandPercent { get; set; }

        [Column("MediumHighLandPercent", Order = 21)]
        [Display(Name = "Medium High Land F1 (30-90 cm)")]
        public double? MediumHighLandPercent { get; set; }

        [Column("MediumLowLandPercent", Order = 22)]
        [Display(Name = "Medium Low Land F2 (90-180 cm)")]
        public double? MediumLowLandPercent { get; set; }

        [Column("LowLandPercent", Order = 23)]
        [Display(Name = "Low Land F3 (> 180-360 cm)")]
        public double? LowLandPercent { get; set; }

        [Column("VeryLowLandPercent", Order = 24)]
        [Display(Name = "Very Low Land F4 (>360 cm)")]
        public double? VeryLowLandPercent { get; set; }

        [Column("CultivableCrops", Order = 25)]
        [Display(Name = "Cultivable Crops")]
        [MaxLength(50)]
        public string CultivableCrops { get; set; }

        [Column("CropProduction", Order = 26)]
        [Display(Name = "Crop Production")]
        public double? CropProduction { get; set; }

        [Column("FishProduction", Order = 27)]
        [Display(Name = "Fish Production")]
        public double? FishProduction { get; set; }

        [Column("FishDiversity", Order = 28)]
        [Display(Name = "Fish Diversity")]
        [MaxLength(50)]
        public string FishDiversity { get; set; }

        [Column("FishMigration", Order = 29)]
        [Display(Name = "Fish Migration")]
        [MaxLength(50)]
        public string FishMigration { get; set; }

        [Column("FloraAndFauna", Order = 30)]
        [Display(Name = "Flora And Fauna")]
        [MaxLength(250)]
        public string FloraAndFauna { get; set; }

        [Column("WaterWithdrawQuantityPerDay", Order = 31)]
        [Display(Name = "Water Withdraw Quantity Per Day")]
        public double? WaterWithdrawQuantityPerDay { get; set; }

        [Column("UseOfFlowMeterMeasrYNId", Order = 32)]
        [Display(Name = "Use of Flow Meter to Measure Water Withdrawal")]
        public int? UseOfFlowMeterMeasrYNId { get; set; }
        [ForeignKey("UseOfFlowMeterMeasrYNId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoUseOfFlowMeter_33 { get; set; }

        [Column("NoOfPump", Order = 33)]
        [Display(Name = "No. of Pump")]
        public int? NoOfPump { get; set; }

        [Column("PumpCapacity", Order = 34)]
        [Display(Name = "Pump Capacity")]
        public double? PumpCapacity { get; set; }

        [Column("PipeDiameter", Order = 35)]
        [Display(Name = "Pipe Diameter")]
        public double? PipeDiameter { get; set; }

        [Column("DivertedWaterRtnSrcYNId", Order = 36)]
        [Display(Name = "Diverted Water Return to the Source")]
        public int? DivertedWaterRtnSrcYNId { get; set; }
        [ForeignKey("DivertedWaterRtnSrcYNId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoDivertWtrRtnSrc_33 { get; set; }

        [Column("WaterSalinity", Order = 37)]
        [Display(Name = "Salinity (ppm)")]
        [MaxLength(50)]
        public string WaterSalinity { get; set; }

        [Column("WaterDO", Order = 38)]
        [Display(Name = "DO (mg/l)")]
        [MaxLength(50)]
        public string WaterDO { get; set; }

        [Column("WaterTDS", Order = 39)]
        [Display(Name = "TDS")]
        [MaxLength(50)]
        public string WaterTDS { get; set; }

        [Column("WaterPhLevel", Order = 40)]
        [Display(Name = "pH")]
        [MaxLength(50)]
        public string WaterPhLevel { get; set; }

        [Column("UseOfToolsYesNoId", Order = 41)]
        [Display(Name = "Was there any Use Of Tools")]
        public int? UseOfToolsYesNoId { get; set; }
        [ForeignKey("UseOfToolsYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoUseOfTool_33 { get; set; }

        [Column("ToolsApplicantComments", Order = 42)]
        [Display(Name = "Tools Applicant Comments")]
        [MaxLength(150)]
        public string ToolsApplicantComments { get; set; }

        [Column("ToolsAuthorityComments", Order = 43)]
        [Display(Name = "Tools Authority Comments")]
        [MaxLength(150)]
        public string ToolsAuthorityComments { get; set; }

        [Column("DuplicatYesNoId", Order = 44)]
        [Display(Name = "Was there any Duplication")]
        public int? DuplicatYesNoId { get; set; }
        [ForeignKey("DuplicatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDuplication_33 { get; set; }

        [Column("DuplicationApplicantComments", Order = 45)]
        [Display(Name = "Duplication Applicant Comments")]
        [MaxLength(150)]
        public string DuplicationApplicantComments { get; set; }

        [Column("DuplicationAuthorityComments", Order = 46)]
        [Display(Name = "Duplication Authority Comments")]
        [MaxLength(150)]
        public string DuplicationAuthorityComments { get; set; }

        [Column("SurfaceWaterDemandPerDay", Order = 47)]
        [Display(Name = "Surface Water Demand Per Day")]
        public double? SurfaceWaterDemandPerDay { get; set; }

        [Column("TotalFishProduction", Order = 48)]
        [Display(Name = "Total Fish Production (Ton)")]
        public double? TotalFishProduction { get; set; }

        [Column("UseOfAppropToolsDescription", Order = 49)]
        [Display(Name = "Short Description, If Yes")]
        [MaxLength(500)]
        public string UseOfAppropToolsDescription { get; set; }

        [Column("CropProductionAmount", Order = 50)]
        [Display(Name = "Crop Production Amount (Taka)")]
        public double? CropProductionAmount { get; set; }

        //04 02 2021
        [Column("OMCostBearYesNoId", Order = 51)]
        [Display(Name = "O&M Cost Beared by Beneficiary Group?")]
        public int? OMCostBearYesNoId { get; set; }
        [ForeignKey("OMCostBearYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoOMCostBear_34 { get; set; }

        [Column("OMCostBearAppComments", Order = 52)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string OMCostBearAppComments { get; set; }

        [Column("OMCostBearAuthComments", Order = 53)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string OMCostBearAuthComments { get; set; }

        [Column("OMCostBearElecBill", Order = 54)]
        [Display(Name = "Electricity Bill")]
        [MaxLength(100)]
        public string OMCostBearElecBill { get; set; }

        [Column("OMBearCostPumpOptCost", Order = 55)]
        [Display(Name = "Pump Operation Cost")]
        [MaxLength(100)]
        public string OMBearCostPumpOptCost { get; set; }

        [Column("OMCostBearCanalRepair", Order = 56)]
        [Display(Name = "Canal Repair & Maintenance")]
        [MaxLength(100)]
        public string OMCostBearCanalRepair { get; set; }

        [Column("OMCostBearOther", Order = 57)]
        [Display(Name = "Others")]
        [MaxLength(100)]
        public string OMCostBearOther { get; set; }
    }
}

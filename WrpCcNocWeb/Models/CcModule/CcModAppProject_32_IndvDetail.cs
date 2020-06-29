using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_32_IndvDetail
    {
        [Key]
        [Column("Project32IndvId", Order = 0)]
        public long Project32IndvId { get; set; }

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

        [Column("FishProduction", Order = 20)]
        [Display(Name = "Fish Production")]
        public int? FishProduction { get; set; }

        [Column("FishDiversity", Order = 21)]
        [Display(Name = "Fish Diversity")]
        [MaxLength(50)]
        public string FishDiversity { get; set; }

        [Column("FishMigration", Order = 22)]
        [Display(Name = "Fish Migration")]
        [MaxLength(50)]
        public string FishMigration { get; set; }

        [Column("FloraAndFauna", Order = 23)]
        [Display(Name = "Flora And Fauna")]
        [MaxLength(250)]
        public string FloraAndFauna { get; set; }            
        
        [Column("WaterWithdrawQuantityPerDay", Order = 25)]
        [Display(Name = "Water Withdraw Quantity Per Day")]        
        public double? WaterWithdrawQuantityPerDay { get; set; }

        [Column("UseOfFlowMeterMeasrYNId", Order = 26)]
        [Display(Name = "Use of Flow Meter to Measure Water Withdrawal")]
        public int? UseOfFlowMeterMeasrYNId { get; set; }
        [ForeignKey("UseOfFlowMeterMeasrYNId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoUseOfFlowMeter_32 { get; set; }
        
        [Column("NoOfPump", Order = 27)]
        [Display(Name = "No. of Pump")]        
        public int? NoOfPump { get; set; }
        
        [Column("PumpCapacity", Order = 28)]
        [Display(Name = "Pump Capacity")]        
        public double? PumpCapacity { get; set; }

        [Column("PipeDiameter", Order = 29)]
        [Display(Name = "Pipe Diameter")]       
        public double? PipeDiameter { get; set; }

        [Column("DivertedWaterRtnSrcYNId", Order = 30)]
        [Display(Name = "Diverted Water Return to the Source")]
        public int? DivertedWaterRtnSrcYNId { get; set; }
        [ForeignKey("DivertedWaterRtnSrcYNId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoDivertWtrRtnSrc_32 { get; set; }
        
        [Column("WaterSalinity", Order = 31)]
        [Display(Name = "Salinity (ppm)")]
        [MaxLength(50)]
        public string WaterSalinity { get; set; }

        [Column("WaterDO", Order = 32)]
        [Display(Name = "DO (mg/l)")]
        [MaxLength(50)]
        public string WaterDO { get; set; }

        [Column("WaterTDS", Order = 33)]
        [Display(Name = "TDS")]
        [MaxLength(50)]
        public string WaterTDS { get; set; }

        [Column("WaterPhLevel", Order = 34)]
        [Display(Name = "pH")]
        [MaxLength(50)]
        public string WaterPhLevel { get; set; }                    

        [Column("UseOfToolsYesNoId", Order = 35)]
        [Display(Name = "Was there any Use Of Tools")]
        public int? UseOfToolsYesNoId { get; set; }
        [ForeignKey("UseOfToolsYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoUseOfTool_32 { get; set; }

        [Column("ToolsApplicantComments", Order = 36)]
        [Display(Name = "Tools Applicant Comments")]
        [MaxLength(150)]
        public string ToolsApplicantComments { get; set; }

        [Column("ToolsAuthorityComments", Order = 37)]
        [Display(Name = "Tools Authority Comments")]
        [MaxLength(150)]
        public string ToolsAuthorityComments { get; set; }

        [Column("DuplicatYesNoId", Order = 38)]
        [Display(Name = "Was there any Duplication")]
        public int? DuplicatYesNoId { get; set; }
        [ForeignKey("DuplicatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDuplication_32 { get; set; }

        [Column("DuplicationApplicantComments", Order = 39)]
        [Display(Name = "Duplication Applicant Comments")]
        [MaxLength(150)]
        public string DuplicationApplicantComments { get; set; }

        [Column("DuplicationAuthorityComments", Order = 40)]
        [Display(Name = "Duplication Authority Comments")]
        [MaxLength(150)]
        public string DuplicationAuthorityComments { get; set; }
    }
}

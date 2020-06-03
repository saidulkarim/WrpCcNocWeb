﻿using System;
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
        
        [Column("ProposedWaterUseId", Order = 2)]
        [Display(Name = "Proposed Water Use Id")]
        public int? ProposedWaterUseId { get; set; }
        [ForeignKey("ProposedWaterUseId")]
        public virtual LookUpCcModProposedWaterUse LookUpCcModProposedWaterUse { get; set; }
       
        [Column("NameOfTheWaterBody", Order = 3)]
        [Display(Name = "Name Of The Water Body")]
        [MaxLength(250)]
        public string NameOfTheWaterBody { get; set; }

        [Column("RiverNatureId", Order = 4)]
        [Display(Name = "River Nature Id")]
        public int? RiverNatureId { get; set; }
        [ForeignKey("RiverNatureId")]
        public virtual LookUpCcModRiverNature LookUpCcModRiverNature { get; set; }
        
        [Column("WaterBodyTypeId", Order = 5)]
        [Display(Name = "Water Body Type Id")]
        public int? WaterBodyTypeId { get; set; }
        [ForeignKey("WaterBodyTypeId")]
        public virtual LookUpCcModTypeOfWaterBody LookUpCcModTypeOfWaterBody { get; set; }

        [Column("Offtake", Order = 6)]
        [Display(Name = "Offtake")]
        [MaxLength(250)]
        public string Offtake { get; set; }

        [Column("Outfall", Order = 7)]
        [Display(Name = "Outfall")]
        [MaxLength(250)]
        public string Outfall { get; set; }

        [Column("Connectivity", Order = 8)]
        [Display(Name = "Connectivity")]
        [MaxLength(250)]
        public string Connectivity { get; set; }

        [Column("ExistingUseOfWaterFromSource", Order = 9)]
        [Display(Name = "Existing Use Of Water From Source")]
        [MaxLength(250)]
        public string ExistingUseOfWaterFromSource { get; set; }

        [Column("WaterLevelDryMax", Order = 10)]
        [Display(Name = "Water Level Dry Max")]
        public double? WaterLevelDryMax { get; set; }

        [Column("WaterLevelDryMin", Order = 11)]
        [Display(Name = "Water Level Dry Min")]
        public double? WaterLevelDryMin { get; set; }

        [Column("WaterLevelWetMax", Order = 12)]
        [Display(Name = "Water Level Wet Max")]
        public double? WaterLevelWetMax { get; set; }

        [Column("WaterLevelWetMin", Order = 13)]
        [Display(Name = "Water Level WetMin")]
        public double? WaterLevelWetMin { get; set; }

        [Column("DischargeDryMax", Order = 14)]
        [Display(Name = "Discharge Dry Max")]
        public double? DischargeDryMax { get; set; }

        [Column("DischargeDryMin", Order = 15)]
        [Display(Name = "Discharge Dry Min")]
        public double? DischargeDryMin { get; set; }

        [Column("DischargeWetMax", Order = 16)]
        [Display(Name = "Discharge Wet Max")]
        public double? DischargeWetMax { get; set; }

        [Column("DischargeWetMin", Order = 17)]
        [Display(Name = "Discharge Wet Min")]
        public double? DischargeWetMin { get; set; }

        [Column("CrossSectionDepth", Order = 18)]
        [Display(Name = "Cross Section Depth")]
        public double? CrossSectionDepth { get; set; }

        [Column("CrossSectionWidth", Order = 19)]
        [Display(Name = "Cross Section Width")]
        public double? CrossSectionWidth { get; set; }

        [Column("SedimentationId", Order = 20)]
        [Display(Name = "Sedimentation Id")]
        public int? SedimentationId { get; set; }
        [ForeignKey("SedimentationId")]
        public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfRiverOrKhal { get; set; }
        
        [Column("FishProduction", Order = 21)]
        [Display(Name = "Fish Production")]
        public int? FishProduction { get; set; }

        [Column("FishDiversity", Order = 22)]
        [Display(Name = "Fish Diversity")]
        [MaxLength(50)]
        public string FishDiversity { get; set; }

        [Column("FishMigration", Order = 23)]
        [Display(Name = "Fish Migration")]
        [MaxLength(50)]
        public string FishMigration { get; set; }

        [Column("FloraAndFauna", Order = 24)]
        [Display(Name = "Flora And Fauna")]
        [MaxLength(250)]
        public string FloraAndFauna { get; set; }

        [Column("WaterDiversionSourceId", Order = 25)]
        [Display(Name = "Water Diversion Source Id")]
        public int? WaterDiversionSourceId { get; set; }
        [ForeignKey("WaterDiversionSourceId")]
        public virtual LookUpCcModWtrDiversionSource LookUpCcModWtrDiversionSource { get; set; }
        
        [Column("WaterWithdrawQuantityPerDay", Order = 26)]
        [Display(Name = "Water Withdraw Quantity Per Day")]        
        public double? WaterWithdrawQuantityPerDay { get; set; }

        [Column("UseOfFlowMeterMeasrYNId", Order = 27)]
        [Display(Name = "Use of Flow Meter to Measure Water Withdrawal")]
        public int? UseOfFlowMeterMeasrYNId { get; set; }
        [ForeignKey("UseOfFlowMeterMeasrYNId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoUseOfFlowMeter { get; set; }
        
        [Column("NoOfPump", Order = 28)]
        [Display(Name = "No. of Pump")]        
        public int? NoOfPump { get; set; }
        
        [Column("PumpCapacity", Order = 29)]
        [Display(Name = "Pump Capacity")]        
        public double? PumpCapacity { get; set; }

        [Column("PipeDiameter", Order = 30)]
        [Display(Name = "Pipe Diameter")]       
        public double? PipeDiameter { get; set; }

        [Column("DivertedWaterRtnSrcYNId", Order = 31)]
        [Display(Name = "Diverted Water Return to the Source")]
        public int? DivertedWaterRtnSrcYNId { get; set; }
        [ForeignKey("DivertedWaterRtnSrcYNId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoDivertWtrRtnSrc { get; set; }
        
        [Column("WaterSalinity", Order = 32)]
        [Display(Name = "Salinity (ppm)")]
        [MaxLength(50)]
        public string WaterSalinity { get; set; }

        [Column("WaterDO", Order = 33)]
        [Display(Name = "DO (mg/l)")]
        [MaxLength(50)]
        public string WaterDO { get; set; }

        [Column("WaterTDS", Order = 34)]
        [Display(Name = "TDS")]
        [MaxLength(50)]
        public string WaterTDS { get; set; }

        [Column("WaterPhLevel", Order = 35)]
        [Display(Name = "pH")]
        [MaxLength(50)]
        public string WaterPhLevel { get; set; }             

        [Column("GroundWaterQualityId", Order = 36)]
        [Display(Name = "Ground Water Quality Id")]
        public int? GroundWaterQualityId { get; set; }
        [ForeignKey("GroundWaterQualityId")]
        public virtual LookUpCcModGroundWaterQuality LookUpCcModGroundWaterQuality { get; set; }

        [Column("UseOfToolsYesNoId", Order = 37)]
        [Display(Name = "Was there any Use Of Tools")]
        public int? UseOfToolsYesNoId { get; set; }
        [ForeignKey("UseOfToolsYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoUseOfTool_32 { get; set; }

        [Column("ToolsApplicantComments", Order = 38)]
        [Display(Name = "Tools Applicant Comments")]
        [MaxLength(150)]
        public string ToolsApplicantComments { get; set; }

        [Column("ToolsAuthorityComments", Order = 39)]
        [Display(Name = "Tools Authority Comments")]
        [MaxLength(150)]
        public string ToolsAuthorityComments { get; set; }

        [Column("DuplicatYesNoId", Order = 40)]
        [Display(Name = "Was there any Duplication")]
        public int? DuplicatYesNoId { get; set; }
        [ForeignKey("DuplicatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDuplication_32 { get; set; }

        [Column("DuplicationApplicantComments", Order = 41)]
        [Display(Name = "Duplication Applicant Comments")]
        [MaxLength(150)]
        public string DuplicationApplicantComments { get; set; }

        [Column("DuplicationAuthorityComments", Order = 42)]
        [Display(Name = "Duplication Authority Comments")]
        [MaxLength(150)]
        public string DuplicationAuthorityComments { get; set; }
    }
}
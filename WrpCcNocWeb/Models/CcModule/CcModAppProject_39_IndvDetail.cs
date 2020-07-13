using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_39_IndvDetail
    {
        [Key]
        [Column("Project39IndvId", Order = 0)]
        public long Project39IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Id")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }               
       
        [Column("NameOfDredgedRiver", Order = 2)]
        [Display(Name = "Name of the Dredged River")]
        [MaxLength(250)]
        public string NameOfDredgedRiver { get; set; }
        
		[Column("RiverNatureId", Order = 3)]
        [Display(Name = "River Nature")]
        public int? RiverNatureId { get; set; }
        [ForeignKey("RiverNatureId")]
        public virtual LookUpCcModRiverNature LookUpCcModRiverNature { get; set; }
		
		[Column("RiverSystem", Order = 4)]
        [Display(Name = "River System (inter linked river with dredged river)")]
        [MaxLength(250)]
        public string RiverSystem { get; set; }
		
		[Column("CatchmentArea", Order = 5)]
        [Display(Name = "Catchment Area (ha)")]        
        public double? CatchmentArea { get; set; }
		
		[Column("DrainageConditionId", Order = 6)]
        [Display(Name = "Drainage Condition")]
        public int? DrainageConditionId { get; set; }
        [ForeignKey("DrainageConditionId")]
        public virtual LookUpCcModDrainageCondition LookUpCcModDrainageCondition { get; set; }
		
		[Column("LengthDredgingWork", Order = 7)]
        [Display(Name = "Length of Dredging Work (km)")]        
        public double? LengthDredgingWork { get; set; }
		
		[Column("FishHabitatArea", Order = 8)]
        [Display(Name = "Area (ha)")]        
        public double? FishHabitatArea { get; set; }
		
		[Column("FishHabitatProduction", Order = 9)]
        [Display(Name = "Production (Ton)")]        
        public double? FishHabitatProduction { get; set; }
		
		[Column("RiverDepth", Order = 10)]
        [Display(Name = "River Depth (m)")]        
        public double? RiverDepth  { get; set; }
		
		[Column("RiverWidth", Order = 11)]
        [Display(Name = "River Width (m)")]        
        public double? RiverWidth  { get; set; }
		
		[Column("SedimentationId", Order = 12)]
        [Display(Name = "Sedimentation")]
        public int? SedimentationId { get; set; }
        [ForeignKey("SedimentationId")]
        public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfRiverOrKhal { get; set; }
		
		[Column("SedimentationRate", Order = 13)]
        [Display(Name = "Sedimentation Rate")]
        public double? SedimentationRate { get; set; }
		
		[Column("BankStabilityTypeId", Order = 14)]
        [Display(Name = "Bank Stability")]
        public int? BankStabilityTypeId { get; set; }
        [ForeignKey("BankStabilityTypeId")]
        public virtual LookUpCcModBankStability LookUpCcModBankStability { get; set; }
		
		[Column("BankErosionLength", Order = 15)]
        [Display(Name = "Length (m)")]        
        public double? BankErosionLength { get; set; }
		
		[Column("BankErosionArea", Order = 16)]
        [Display(Name = "Area (ha)")]        
        public double? BankErosionArea { get; set; }
		
		[Column("BankErosionLocation", Order = 17)]
        [Display(Name = "Location")]
        [MaxLength(150)]
        public string BankErosionLocation { get; set; }
		
		[Column("AccretionLength", Order = 18)]
        [Display(Name = "Length (m)")]        
        public double? AccretionLength { get; set; }
		
		[Column("AccretionArea", Order = 19)]
        [Display(Name = "Area (ha)")]        
        public double? AccretionArea { get; set; }
		
		[Column("AccretionLocation", Order = 20)]
        [Display(Name = "Location")]
        [MaxLength(150)]
        public string AccretionLocation { get; set; }
		
		[Column("WaterLevelDryMax", Order = 21)]
        [Display(Name = "Water Level Dry Max")]
        public double? WaterLevelDryMax { get; set; }

        [Column("WaterLevelDryMin", Order = 22)]
        [Display(Name = "Water Level Dry Min")]
        public double? WaterLevelDryMin { get; set; }

        [Column("WaterLevelWetMax", Order = 23)]
        [Display(Name = "Water Level Wet Max")]
        public double? WaterLevelWetMax { get; set; }

        [Column("WaterLevelWetMin", Order = 24)]
        [Display(Name = "Water Level WetMin")]
        public double? WaterLevelWetMin { get; set; }

        [Column("DischargeDryMax", Order = 25)]
        [Display(Name = "Discharge Dry Max")]
        public double? DischargeDryMax { get; set; }

        [Column("DischargeDryMin", Order = 26)]
        [Display(Name = "Discharge Dry Min")]
        public double? DischargeDryMin { get; set; }

        [Column("DischargeWetMax", Order = 27)]
        [Display(Name = "Discharge Wet Max")]
        public double? DischargeWetMax { get; set; }

        [Column("DischargeWetMin", Order = 28)]
        [Display(Name = "Discharge Wet Min")]
        public double? DischargeWetMin { get; set; }			
		
		[Column("UseOfToolsYesNoId", Order = 29)]
        [Display(Name = "Was there any Use Of Tools")]
        public int? UseOfToolsYesNoId { get; set; }
        [ForeignKey("UseOfToolsYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoUseOfTool_39 { get; set; }

        [Column("ToolsApplicantComments", Order = 30)]
        [Display(Name = "Tools Applicant Comments")]
        [MaxLength(150)]
        public string ToolsApplicantComments { get; set; }

        [Column("ToolsAuthorityComments", Order = 31)]
        [Display(Name = "Tools Authority Comments")]
        [MaxLength(150)]
        public string ToolsAuthorityComments { get; set; }				
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_310_IndvDetail
    {
        [Key]
        [Column("Project310IndvId", Order = 0)]
        public long Project310IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Id")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }               
       
        [Column("NameExcavatedKhal", Order = 2)]
        [Display(Name = "Name of the Excavated Khals")]
        [MaxLength(250)]
        public string NameExcavatedKhal { get; set; }
        
		[Column("RiverNatureId", Order = 3)]
        [Display(Name = "Khal Nature")]
        public int? RiverNatureId { get; set; }
        [ForeignKey("RiverNatureId")]
        public virtual LookUpCcModRiverNature LookUpCcModKhalNature_310 { get; set; }
		
		[Column("CatchmentArea", Order = 4)]
        [Display(Name = "Catchment Area (ha)")]        
        public double? CatchmentArea { get; set; }
		
		[Column("WaterLevelDryMax", Order = 5)]
        [Display(Name = "Water Level Dry Max")]
        public double? WaterLevelDryMax { get; set; }

        [Column("WaterLevelDryMin", Order = 6)]
        [Display(Name = "Water Level Dry Min")]
        public double? WaterLevelDryMin { get; set; }

        [Column("WaterLevelWetMax", Order = 7)]
        [Display(Name = "Water Level Wet Max")]
        public double? WaterLevelWetMax { get; set; }

        [Column("WaterLevelWetMin", Order = 8)]
        [Display(Name = "Water Level WetMin")]
        public double? WaterLevelWetMin { get; set; }

        [Column("DischargeDryMax", Order = 9)]
        [Display(Name = "Discharge Dry Max")]
        public double? DischargeDryMax { get; set; }

        [Column("DischargeDryMin", Order = 10)]
        [Display(Name = "Discharge Dry Min")]
        public double? DischargeDryMin { get; set; }

        [Column("DischargeWetMax", Order = 11)]
        [Display(Name = "Discharge Wet Max")]
        public double? DischargeWetMax { get; set; }

        [Column("DischargeWetMin", Order = 12)]
        [Display(Name = "Discharge Wet Min")]
        public double? DischargeWetMin { get; set; }
		
		[Column("SedimentationId", Order = 13)]
        [Display(Name = "Sedimentation Of Khal")]
        public int? SedimentationId { get; set; }
        [ForeignKey("SedimentationId")]
        public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfKhal_310 { get; set; }
		
		[Column("SedimentationRate", Order = 14)]
        [Display(Name = "Sedimentation Rate of Khal")]
        public double? SedimentationRate { get; set; }
		
		[Column("LengthExcavationWork", Order = 15)]
        [Display(Name = "Length of Excavation Work")]
        public double? LengthExcavationWork { get; set; }
		
		[Column("FishHabitatArea", Order = 16)]
        [Display(Name = "Area (ha)")]        
        public double? FishHabitatArea { get; set; }
		
		[Column("FishHabitatProduction", Order = 17)]
        [Display(Name = "Production (Ton)")]        
        public double? FishHabitatProduction { get; set; }
				
		[Column("ExcavatedMaterialQuality", Order = 18)]
        [Display(Name = "Excavated Material Quality")]
        [MaxLength(250)]
        public string ExcavatedMaterialQuality { get; set; }
		
		[Column("SpoilManagementPlan", Order = 19)]
        [Display(Name = "Spoil Management Plan")]
        [MaxLength(250)]
        public string SpoilManagementPlan { get; set; }		
		
		[Column("DrainageConditionId", Order = 20)]
        [Display(Name = "Drainage Condition")]
        public int? DrainageConditionId { get; set; }
        [ForeignKey("DrainageConditionId")]
        public virtual LookUpCcModDrainageCondition LookUpCcModDrainageCond_39 { get; set; }		
		
		[Column("UseOfToolsYesNoId", Order = 21)]
        [Display(Name = "Was there any Use Of Tools")]
        public int? UseOfToolsYesNoId { get; set; }
        [ForeignKey("UseOfToolsYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoUseOfTool_310 { get; set; }

        [Column("ToolsApplicantComments", Order = 22)]
        [Display(Name = "Tools Applicant Comments")]
        [MaxLength(150)]
        public string ToolsApplicantComments { get; set; }

        [Column("ToolsAuthorityComments", Order = 23)]
        [Display(Name = "Tools Authority Comments")]
        [MaxLength(150)]
        public string ToolsAuthorityComments { get; set; }		

		[Column("DuplicatYesNoId", Order = 24)]
        [Display(Name = "Was there any Duplication")]
        public int? DuplicatYesNoId { get; set; }
        [ForeignKey("DuplicatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDuplication_310 { get; set; }

        [Column("DuplicationApplicantComments", Order = 25)]
        [Display(Name = "Duplication Applicant Comments")]
        [MaxLength(150)]
        public string DuplicationApplicantComments { get; set; }

        [Column("DuplicationAuthorityComments", Order = 26)]
        [Display(Name = "Duplication Authority Comments")]
        [MaxLength(150)]
        public string DuplicationAuthorityComments { get; set; }		
    }
}
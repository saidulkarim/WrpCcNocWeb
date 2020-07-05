using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_35_IndvDetail
    {
        [Key]
        [Column("Project35IndvId", Order = 0)]
        public long Project35IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }
        
		[Column("WaterBodyTypeId", Order = 2)]
        [Display(Name = "Type of Water Bodies")]
        public int? WaterBodyTypeId { get; set; }
        [ForeignKey("WaterBodyTypeId")]
        public virtual LookUpCcModTypeOfWaterBody LookUpCcModTypeOfWaterBody { get; set; }	
		
		[Column("Offtake", Order = 3)]
        [Display(Name = "Offtake")]
        [MaxLength(250)]
        public string Offtake { get; set; }

        [Column("Outfall", Order = 4)]
        [Display(Name = "Outfall")]
        [MaxLength(250)]
        public string Outfall { get; set; }

        [Column("Connectivity", Order = 5)]
        [Display(Name = "Connectivity")]
        [MaxLength(250)]
        public string Connectivity { get; set; }
		
		[Column("WaterLevelDryMax", Order = 6)]
        [Display(Name = "Water Level Dry Max")]
        public double? WaterLevelDryMax { get; set; }

        [Column("WaterLevelDryMin", Order = 7)]
        [Display(Name = "Water Level Dry Min")]
        public double? WaterLevelDryMin { get; set; }

        [Column("WaterLevelWetMax", Order = 8)]
        [Display(Name = "Water Level Wet Max")]
        public double? WaterLevelWetMax { get; set; }

        [Column("WaterLevelWetMin", Order = 9)]
        [Display(Name = "Water Level WetMin")]
        public double? WaterLevelWetMin { get; set; }

        [Column("DischargeDryMax", Order = 10)]
        [Display(Name = "Discharge Dry Max")]
        public double? DischargeDryMax { get; set; }

        [Column("DischargeDryMin", Order = 11)]
        [Display(Name = "Discharge Dry Min")]
        public double? DischargeDryMin { get; set; }

        [Column("DischargeWetMax", Order = 12)]
        [Display(Name = "Discharge Wet Max")]
        public double? DischargeWetMax { get; set; }

        [Column("DischargeWetMin", Order = 13)]
        [Display(Name = "Discharge Wet Min")]
        public double? DischargeWetMin { get; set; }
		
		[Column("CrossSectionDepth", Order = 14)]
        [Display(Name = "Cross Section Depth")]
        public double? CrossSectionDepth { get; set; }

        [Column("CrossSectionWidth", Order = 15)]
        [Display(Name = "Cross Section Width")]
        public double? CrossSectionWidth { get; set; }

        [Column("SedimentationId", Order = 16)]
        [Display(Name = "Sedimentation Id")]
        public int? SedimentationId { get; set; }
        [ForeignKey("SedimentationId")]
        public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfRiverOrKhal { get; set; }

        [Column("SedimentationRate", Order = 17)]
        [Display(Name = "Sedimentation Rate")]
        public double? SedimentationRate { get; set; }
		
		[Column("DrainageConditionId", Order = 18)]
        [Display(Name = "Drainage Condition")]
        public int? DrainageConditionId { get; set; }
        [ForeignKey("DrainageConditionId")]
        public virtual LookUpCcModDrainageCondition LookUpCcModDrainageCondition { get; set; }
		
		[Column("HighLandPercent", Order = 19)]
        [Display(Name = "High Land F0 (0 - 30 cm)")]
        public double? HighLandPercent { get; set; }

        [Column("MediumHighLandPercent", Order = 20)]
        [Display(Name = "Medium High Land F1 (30 - 90 cm)")]
        public double? MediumHighLandPercent { get; set; }

        [Column("MediumLowLandPercent", Order = 21)]
        [Display(Name = "Medium Low Land F2 (90 - 180 cm)")]
        public double? MediumLowLandPercent { get; set; }

        [Column("LowLandPercent", Order = 22)]
        [Display(Name = "Low Land F3 (> 180 - 360 cm)")]
        public double? LowLandPercent { get; set; }

        [Column("VeryLowLandPercent", Order = 23)]
        [Display(Name = "Very Low Land F4 (> 360 cm)")]
        public double? VeryLowLandPercent { get; set; }
                
        [Column("CropProduction", Order = 24)]
        [Display(Name = "Crop Production")]
        public double? CropProduction { get; set; }

        [Column("FishProduction", Order = 25)]
        [Display(Name = "Fish Production")]
        public double? FishProduction { get; set; }

        [Column("FishDiversity", Order = 26)]
        [Display(Name = "Fish Diversity")]
        [MaxLength(50)]
        public string FishDiversity { get; set; }

        [Column("FishMigration", Order = 27)]
        [Display(Name = "Fish Migration")]
        [MaxLength(50)]
        public string FishMigration { get; set; }

        [Column("FloraAndFauna", Order = 28)]
        [Display(Name = "Flora And Fauna")]
        [MaxLength(50)]
        public string FloraAndFauna { get; set; }
			

        [Column("DuplicatYesNoId", Order = 29)]
        [Display(Name = "Was there any Duplication")]
        public int? DuplicatYesNoId { get; set; }
        [ForeignKey("DuplicatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDuplication_35 { get; set; }

        [Column("DuplicationApplicantComments", Order = 30)]
        [Display(Name = "Duplication Applicant Comments")]
        [MaxLength(150)]
        public string DuplicationApplicantComments { get; set; }

        [Column("DuplicationAuthorityComments", Order = 31)]
        [Display(Name = "Duplication Authority Comments")]
        [MaxLength(150)]
        public string DuplicationAuthorityComments { get; set; }
    }
}
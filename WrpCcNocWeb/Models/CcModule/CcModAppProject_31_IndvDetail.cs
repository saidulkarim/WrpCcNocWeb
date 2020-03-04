using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_31_IndvDetail
    {
        [Key]
        [Column("Project31IndvId", Order = 0)]
        public long Project31IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
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

        [Column("HighestFloodLevel", Order = 4)]
        [Display(Name = "Highest Flood Level (m PWD)")]
        public double? HighestFloodLevel { get; set; }

        [Column("MaximumDischarge", Order = 5)]
        [Display(Name = "Maximum Discharge")]
        public double? MaximumDischarge { get; set; }

        [Column("DrainageConditionId", Order = 6)]
        [Display(Name = "Drainage Condition")]
        public int? DrainageConditionId { get; set; }
        [ForeignKey("DrainageConditionId")]
        public virtual LookUpCcModDrainageCondition LookUpCcModDrainageCondition { get; set; }

        [Column("WaterSalinity", Order = 7)]
        [Display(Name = "Salinity (ppm)")]
        [MaxLength(50)]
        public string WaterSalinity { get; set; }

        [Column("WaterDO", Order = 8)]
        [Display(Name = "DO (mg/l)")]
        [MaxLength(50)]
        public string WaterDO { get; set; }

        [Column("WaterTDS", Order = 9)]
        [Display(Name = "TDS")]
        [MaxLength(50)]
        public string WaterTDS { get; set; }

        [Column("WaterPhLevel", Order = 10)]
        [Display(Name = "pH")]
        [MaxLength(50)]
        public string WaterPhLevel { get; set; }

        [Column("HighLandPercent", Order = 11)]
        [Display(Name = "High Land F0 (0 - 30 cm)")]
        public double? HighLandPercent { get; set; }

        [Column("MediumHighLandPercent", Order = 12)]
        [Display(Name = "Medium High Land F1 (30 - 90 cm)")]
        public double? MediumHighLandPercent { get; set; }

        [Column("MediumLowLandPercent", Order = 13)]
        [Display(Name = "Medium Low Land F2 (90 - 180 cm)")]
        public double? MediumLowLandPercent { get; set; }

        [Column("LowLandPercent", Order = 14)]
        [Display(Name = "Low Land F3 (> 180 - 360 cm)")]
        public double? LowLandPercent { get; set; }

        [Column("VeryLowLandPercent", Order = 15)]
        [Display(Name = "Very Low Land F4 (> 360 cm)")]
        public double? VeryLowLandPercent { get; set; }

        [Column("CultivableCrops", Order = 16)]
        [Display(Name = "Cultivable Crops")]
        [MaxLength(50)]
        public string CultivableCrops { get; set; }

        [Column("CropProduction", Order = 17)]
        [Display(Name = "Crop Production")]
        public int? CropProduction { get; set; }

        [Column("FishProduction", Order = 18)]
        [Display(Name = "Fish Production")]
        public int? FishProduction { get; set; }

        [Column("FishDiversity", Order = 19)]
        [Display(Name = "Fish Diversity")]
        [MaxLength(50)]
        public string FishDiversity { get; set; }

        [Column("FishMigration", Order = 20)]
        [Display(Name = "Fish Migration")]
        [MaxLength(50)]
        public string FishMigration { get; set; }

        [Column("FloraAndFauna", Order = 21)]
        [Display(Name = "Flora And Fauna")]
        [MaxLength(50)]
        public string FloraAndFauna { get; set; }

        [Column("LandLessPeoplePercentage", Order = 22)]
        [Display(Name = "Land Less People Percentage")]
        public double? LandLessPeoplePercentage { get; set; }

        [Column("SmallFarmerPercentage", Order = 23)]
        [Display(Name = "Small Farmer Percentage")]
        public double? SmallFarmerPercentage { get; set; }

        [Column("AvgMonthlyIncome", Order = 24)]
        [Display(Name = "Avg Monthly Income")]
        public int? AvgMonthlyIncome { get; set; }

        [Column("UseOfToolsYesNoId", Order = 25)]
        [Display(Name = "Was there any Use Of Tools")]
        public int? UseOfToolsYesNoId { get; set; }
        [ForeignKey("UseOfToolsYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoUseOfTool { get; set; }

        [Column("ToolsApplicantComments", Order = 26)]
        [Display(Name = "Tools Applicant Comments")]
        [MaxLength(150)]
        public string ToolsApplicantComments { get; set; }

        [Column("ToolsAuthorityComments", Order = 27)]
        [Display(Name = "Tools Authority Comments")]
        [MaxLength(150)]
        public string ToolsAuthorityComments { get; set; }

        [Column("DuplicatYesNoId", Order = 28)]
        [Display(Name = "Was there any Duplication")]
        public int? DuplicatYesNoId { get; set; }
        [ForeignKey("DuplicatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDuplication { get; set; }

        [Column("DuplicationApplicantComments", Order = 29)]
        [Display(Name = "Duplication Applicant Comments")]
        [MaxLength(150)]
        public string DuplicationApplicantComments { get; set; }

        [Column("DuplicationAuthorityComments", Order = 30)]
        [Display(Name = "Duplication Authority Comments")]
        [MaxLength(150)]
        public string DuplicationAuthorityComments { get; set; }
    }
}

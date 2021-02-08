using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_34_IndvDetail
    {
        [Key]
        [Column("Project34IndvId", Order = 0)]
        public long Project34IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Column("NameOfHydraulicStructure", Order = 2)]
        [Display(Name = "NameOf Hydraulic Structure")]
        [MaxLength(250)]
        public string NameOfHydraulicStructure { get; set; }

        [Column("TypeOfStructure", Order = 3)]
        [Display(Name = "Type of Structure")]
        [MaxLength(250)]
        public string TypeOfStructure { get; set; }

        [Column("TypeOfTrafficNavigation", Order = 4)]
        [Display(Name = "Type of traffic and navigation")]
        [MaxLength(250)]
        public string TypeOfTrafficNavigation { get; set; }

        [Column("RiverNatureId", Order = 6)]
        [Display(Name = "River Nature")]
        public int? RiverNatureId { get; set; }
        [ForeignKey("RiverNatureId")]
        public virtual LookUpCcModRiverNature LookUpCcModRiverNature { get; set; }

        [Column("CommandArea", Order = 7)]
        [Display(Name = "Command Area (ha)")]
        [MaxLength(250)]
        public string CommandArea { get; set; }

        [Column("CrossSectionDepth", Order = 8)]
        [Display(Name = "Cross Section Depth")]
        public double? CrossSectionDepth { get; set; }

        [Column("CrossSectionWidth", Order = 9)]
        [Display(Name = "Cross Section Width")]
        public double? CrossSectionWidth { get; set; }

        [Column("WaterLevelMax", Order = 10)]
        [Display(Name = "Maximum")]
        public double? WaterLevelMax { get; set; }

        [Column("WaterLevelMin", Order = 11)]
        [Display(Name = "Minimum")]
        public double? WaterLevelMin { get; set; }

        [Column("DischargeMax", Order = 12)]
        [Display(Name = "Maximum")]
        public double? DischargeMax { get; set; }

        [Column("DischargeMin", Order = 13)]
        [Display(Name = "Minimum")]
        public double? DischargeMin { get; set; }

        [Column("BankLineShiftingId", Order = 15)]
        [Display(Name = "Bank Line Shifting")]
        public int? BankLineShiftingId { get; set; }
        [ForeignKey("BankLineShiftingId")]
        public virtual LookUpCcModBankLineShifting LookUpCcModBankLineShifting { get; set; }

        [Column("BankStabilityTypeId", Order = 15)]
        [Display(Name = "Bank Stability")]
        public int? BankStabilityTypeId { get; set; }
        [ForeignKey("BankStabilityTypeId")]
        public virtual LookUpCcModBankStability LookUpCcModBankStability { get; set; }

        [Column("BankSoilCondition", Order = 16)]
        [Display(Name = "Bank Soil Condition")]
        [MaxLength(250)]
        public string BankSoilCondition { get; set; }

        [Column("BankErosionLength", Order = 17)]
        [Display(Name = "Length (m)")]
        public double? BankErosionLength { get; set; }

        [Column("BankErosionArea", Order = 18)]
        [Display(Name = "Area (ha)")]
        public double? BankErosionArea { get; set; }

        [Column("BankErosionLocation", Order = 19)]
        [Display(Name = "Location")]
        [MaxLength(150)]
        public string BankErosionLocation { get; set; }

        [Column("BankErosionRate", Order = 20)]
        [Display(Name = "Erosion Rate (m/year)")]
        public double? BankErosionRate { get; set; }

        [Column("RiverBedErosion", Order = 21)]
        [Display(Name = "River Bed Erosion")]
        [MaxLength(150)]
        public string RiverBedErosion { get; set; }

        [Column("SedimentationId", Order = 22)]
        [Display(Name = "Sedimentation")]
        public int? SedimentationId { get; set; }
        [ForeignKey("SedimentationId")]
        public virtual LookUpCcModSediOfRiverOrKhal LookUpCcModSediOfRiverOrKhal { get; set; }

        [Column("SedimentationRate", Order = 23)]
        [Display(Name = "Sedimentation Rate")]
        public double? SedimentationRate { get; set; }

        [Column("CharAccretionLength", Order = 24)]
        [Display(Name = "Length (m)")]
        public double? CharAccretionLength { get; set; }

        [Column("CharAccretionArea", Order = 25)]
        [Display(Name = "Area (ha)")]
        public double? CharAccretionArea { get; set; }

        [Column("CharAccretionLocation", Order = 26)]
        [Display(Name = "Location")]
        [MaxLength(150)]
        public string CharAccretionLocation { get; set; }

        [Column("RiverTrainingWorksLoc", Order = 27)]
        [Display(Name = "Location")]
        [MaxLength(150)]
        public string RiverTrainingWorksLoc { get; set; }

        [Column("DuplicatYesNoId", Order = 28)]
        [Display(Name = "Was there any Duplication")]
        public int? DuplicatYesNoId { get; set; }
        [ForeignKey("DuplicatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDuplication_34 { get; set; }

        [Column("DuplicationApplicantComments", Order = 29)]
        [Display(Name = "Duplication Applicant Comments")]
        [MaxLength(150)]
        public string DuplicationApplicantComments { get; set; }

        [Column("DuplicationAuthorityComments", Order = 30)]
        [Display(Name = "Duplication Authority Comments")]
        [MaxLength(150)]
        public string DuplicationAuthorityComments { get; set; }

        [Column("RiverTrainingWorksQuantity", Order = 31)]
        [Display(Name = "Quantity")]
        [MaxLength(150)]
        public string RiverTrainingWorksQuantity { get; set; }
    }
}
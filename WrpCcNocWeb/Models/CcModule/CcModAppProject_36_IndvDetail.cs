using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_36_IndvDetail
    {
        [Key]
        [Column("Project36IndvId", Order = 0)]
        public long Project36IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
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
		
		[Column("DrainageConditionId", Order = 5)]
        [Display(Name = "Drainage Condition")]
        public int? DrainageConditionId { get; set; }
        [ForeignKey("DrainageConditionId")]
        public virtual LookUpCcModDrainageCondition LookUpCcModDrainageCondition { get; set; }
		
		[Column("HighLandPercent", Order = 6)]
        [Display(Name = "High Land F0 (0 - 30 cm)")]
        public double? HighLandPercent { get; set; }

        [Column("MediumHighLandPercent", Order = 7)]
        [Display(Name = "Medium High Land F1 (30 - 90 cm)")]
        public double? MediumHighLandPercent { get; set; }

        [Column("MediumLowLandPercent", Order = 8)]
        [Display(Name = "Medium Low Land F2 (90 - 180 cm)")]
        public double? MediumLowLandPercent { get; set; }

        [Column("LowLandPercent", Order = 9)]
        [Display(Name = "Low Land F3 (> 180 - 360 cm)")]
        public double? LowLandPercent { get; set; }

        [Column("VeryLowLandPercent", Order = 10)]
        [Display(Name = "Very Low Land F4 (> 360 cm)")]
        public double? VeryLowLandPercent { get; set; }
		
		[Column("FishProduction", Order = 11)]
        [Display(Name = "Fish Production")]
        public int? FishProduction { get; set; }

        [Column("FishDiversity", Order = 12)]
        [Display(Name = "Fish Diversity")]
        [MaxLength(50)]
        public string FishDiversity { get; set; }

        [Column("FishMigration", Order = 13)]
        [Display(Name = "Fish Migration")]
        [MaxLength(50)]
        public string FishMigration { get; set; }
				
		[Column("LandUseMapYesNoId", Order = 14)]
        [Display(Name = "Land Use Map (Rajuk Approval and others)")]
        public int? LandUseMapYesNoId { get; set; }
        [ForeignKey("LandUseMapYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoLandUseMap_36 { get; set; }

        [Column("LandUseMapApplicantComments", Order = 15)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string LandUseMapApplicantComments { get; set; }

        [Column("LandUseMapAuthorityComments", Order = 16)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string LandUseMapAuthorityComments { get; set; }
		
		[Column("LandUseDesignYesNoId", Order = 17)]
        [Display(Name = "Land Use Design/ Planning")]
        public int? LandUseDesignYesNoId { get; set; }
        [ForeignKey("LandUseDesignYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoLandUseDesign_36 { get; set; }

        [Column("LandUseDesignApplicantComments", Order = 18)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string LandUseDesignApplicantComments { get; set; }

        [Column("LandUseDesignAuthorityComments", Order = 19)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string LandUseDesignAuthorityComments { get; set; }
		
		[Column("ImpactFloodPlainAreaYesNoId", Order = 20)]
        [Display(Name = "Impact on Floodplain Area")]
        public int? ImpactFloodPlainAreaYesNoId { get; set; }
        [ForeignKey("ImpactFloodPlainAreaYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoImpctFldPlnAra_36 { get; set; }

        [Column("ImpctFldPlnAraApplicntComments", Order = 21)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string ImpctFldPlnAraApplicntComments { get; set; }

        [Column("ImpctFldPlnAraAuthortyComments", Order = 22)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string ImpctFldPlnAraAuthortyComments { get; set; }			

        [Column("DuplicatYesNoId", Order = 23)]
        [Display(Name = "Was there any Duplication")]
        public int? DuplicatYesNoId { get; set; }
        [ForeignKey("DuplicatYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDuplication_36 { get; set; }

        [Column("DuplicationApplicantComments", Order = 24)]
        [Display(Name = "Duplication Applicant Comments")]
        [MaxLength(150)]
        public string DuplicationApplicantComments { get; set; }

        [Column("DuplicationAuthorityComments", Order = 25)]
        [Display(Name = "Duplication Authority Comments")]
        [MaxLength(150)]
        public string DuplicationAuthorityComments { get; set; }
    }
}
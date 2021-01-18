using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModSubDesignSubmitDetail
    {
        [Key]
        [Column("SubDesignSubmittedId", Order = 0)]
        public long SubDesignSubmittedId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        //Embankment Plantation Design
        [Column("EmbankmentPlantationYesNoId", Order = 2)]
        [Display(Name = "Embankment Plantation Design")]
        public int EmbankmentPlantationYesNoId { get; set; }
        [ForeignKey("EmbankmentPlantationYesNoId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoEmbank { get; set; }

        [Column("EmbankmentPlantationAppCmt", Order = 3)]
        [MaxLength(150)]
        [Display(Name = "Applicant Comments")]
        public string EmbankmentPlantationAppCmt { get; set; }

        [Column("EmbankmentPlantationAuthCmt", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "Authority Comments")]
        public string EmbankmentPlantationAuthCmt { get; set; }

        //Polder
        [Column("PolderDrainageSysYesNoId", Order = 5)]
        [Display(Name = "Whether drainage system inside the polder area is considered with high importance or not?")]
        public int PolderDrainageSysYesNoId { get; set; }
        [ForeignKey("PolderDrainageSysYesNoId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoPolder { get; set; }

        [Column("PolderDrainageSysAppCmt", Order = 6)]
        [MaxLength(150)]
        [Display(Name = "Applicant Comments")]
        public string PolderDrainageSysAppCmt { get; set; }

        [Column("PolderDrainageSysAuthCmt", Order = 7)]
        [MaxLength(150)]
        [Display(Name = "Authority Comments")]
        public string PolderDrainageSysAuthCmt { get; set; }

        //Shelter of Flood Affected People 
        [Column("ShelterDesignYesNoId", Order = 8)]
        [Display(Name = "Whether any bay included in the design for shelter of flood affected people during flood?")]
        public int ShelterDesignYesNoId { get; set; }
        [ForeignKey("ShelterDesignYesNoId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoShelter { get; set; }

        [Column("ShelterDesignAppCmt", Order = 9)]
        [MaxLength(150)]
        [Display(Name = "Applicant Comments")]
        public string ShelterDesignAppCmt { get; set; }

        [Column("ShelterDesignAuthCmt", Order = 10)]
        [MaxLength(150)]
        [Display(Name = "Authority Comments")]
        public string ShelterDesignAuthCmt { get; set; }

        //Shelter of Flood Affected People 
        [Column("SufficientDisShrimpGherYesNoId", Order = 11)]
        [Display(Name = "During embankment design, whether sufficient distance (minimum 300 feet) is kept from Shrimp gher and provision for water intrusion in Shrimp gher through drainage canal? ")]
        public int SufficientDisShrimpGherYesNoId { get; set; }
        [ForeignKey("SufficientDisShrimpGherYesNoId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoShrimpGher { get; set; }

        [Column("SufficientDisShrimpGherAppCmt", Order = 12)]
        [MaxLength(150)]
        [Display(Name = "Applicant Comments")]
        public string SufficientDisShrimpGherAppCmt { get; set; }

        [Column("SufficientDisShrimpGherAuthCmt", Order = 13)]
        [MaxLength(150)]
        [Display(Name = "Authority Comments")]
        public string SufficientDisShrimpGherAuthCmt { get; set; }
    }
}
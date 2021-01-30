using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpAgency
    {
        [Key]
        [Column("AgencyId", Order = 0)]
        public int AgencyId { get; set; }

        [Required]
        [Column("MinistryId", Order = 1)]
        [Display(Name = "Ministry")]
        public int MinistryId { get; set; }
        [ForeignKey("MinistryId")]
        public virtual LookUpMinistry LookUpMinistry { get; set; }

        [Required]
        [Column("AgencyName", Order = 2)]
        [MaxLength(250)]
        [Display(Name = "Agency")]
        public string AgencyName { get; set; }

        [Column("AgencyShortName", Order = 3)]
        [MaxLength(50)]
        [Display(Name = "Agency Short Name")]
        public string AgencyShortName { get; set; }

        [Column("AgencyNameBn", Order = 4)]
        [MaxLength(250)]
        [Display(Name = "Agency (Bangla)")]
        public string AgencyNameBn { get; set; }

        [Column("AgencyShortNameBn", Order = 5)]
        [MaxLength(50)]
        [Display(Name = "Agency Short Name (Bangla)")]
        public string AgencyShortNameBn { get; set; }

        [Column("WebsiteLink", Order = 6)]
        [MaxLength(100)]
        [Display(Name = "Website Link")]
        public string WebsiteLink { get; set; }
    }
}
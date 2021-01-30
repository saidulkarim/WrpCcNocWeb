using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdpSector
    {
        [Key]
        [Column("AdpSectorId", Order = 0)]
        public int AdpSectorId { get; set; }

        [Required]
        [Column("AdpSectorName", Order = 1)]
        [MaxLength(250)]
        [Display(Name = "ADP Sector")]
        public string AdpSectorName { get; set; }

        [Column("AdpSectorShortName", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "ADP Sector Short Name")]
        public string AdpSectorShortName { get; set; }

        [Column("AdpSectorNameBn", Order = 3)]
        [MaxLength(250)]
        [Display(Name = "ADP Sector (Bangla)")]
        public string AdpSectorNameBn { get; set; }

        [Column("AdpSectorShortNameBn", Order = 4)]
        [MaxLength(50)]
        [Display(Name = "ADP Sector Short Name (Bangla)")]
        public string AdpSectorShortNameBn { get; set; }

        [Column("WebsiteLink", Order = 5)]
        [MaxLength(100)]
        [Display(Name = "Website Link")]
        public string WebsiteLink { get; set; }
    }
}
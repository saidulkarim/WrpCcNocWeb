using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpMinistry
    {
        [Key]
        [Column("MinistryId", Order = 0)]
        public int MinistryId { get; set; }

        [Required]
        [Column("MinistryName", Order = 1)]
        [MaxLength(250)]
        [Display(Name = "Ministry")]
        public string MinistryName { get; set; }

        [Column("MinistryShortName", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Ministry Short Name")]
        public string MinistryShortName { get; set; }

        [Column("MinistryNameBn", Order = 3)]
        [MaxLength(250)]
        [Display(Name = "Ministry (Bangla)")]
        public string MinistryNameBn { get; set; }

        [Column("MinistryShortNameBn", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "Ministry Short Name (Bangla)")]
        public string MinistryShortNameBn { get; set; }

        [Column("WebsiteLink", Order = 5)]
        [MaxLength(100)]
        [Display(Name = "Website Link")]
        public string WebsiteLink { get; set; }
    }
}
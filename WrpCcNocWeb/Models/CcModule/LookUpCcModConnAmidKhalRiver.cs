using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModConnAmidKhalRiver
    {
        [Key]
        [Column("ConnAmidKhalRiverId", Order = 0)]
        public int ConnAmidKhalRiverId { get; set; }

        [Required]
        [Column("OptionTitle", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Title")]
        public string OptionTitle { get; set; }


        [Column("OptionTitleBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Title (Bangla)")]
        public string OptionTitleBn { get; set; }
    }
}
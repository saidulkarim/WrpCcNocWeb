using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.AdminModule
{
    public class LookUpAdminModLanguage
    {
        [Key]
        [Column("LanguageId", Order = 0)]
        public int LanguageId { get; set; }

        [Required]
        [Column("Language", Order = 1)]
        [MaxLength(20)]
        [Display(Name = "Language")]
        public string Language { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminModMenu
    {
        [Key]        
        [Column("MenuId", Order = 0)]
        public int MenuId { get; set; }
               
        [Required]       
        [Column("MenuTitle", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Menu Title")]
        public string MenuTitle { get; set; }

        [Column("MenuTitleBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Menu Title")]
        public string MenuTitleBn { get; set; }
    }
}
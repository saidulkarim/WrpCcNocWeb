using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminModSubMenu
    {
        [Key]
        [Column("SubMenuId", Order = 0)]
        public int SubMenuId { get; set; }

        [Required]
        [Column("MenuId", Order = 1)]
        [Display(Name = "Menu")]
        public int MenuId { get; set; }
        [ForeignKey("MenuId")]
        public virtual LookUpAdminModMenu LookUpAdminModMenu { get; set; }

        [Required]
        [Column("SubMenuTitle", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Sub Menu Title")]
        public string SubMenuTitle { get; set; }

        [Column("SubMenuTitleBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Sub Menu Title")]
        public string SubMenuTitleBn { get; set; }

        [Column("Controller", Order = 3)]
        [MaxLength(100)]
        [Display(Name = "Controller")]
        public string Controller { get; set; }

        [Column("Action", Order = 4)]
        [MaxLength(100)]
        [Display(Name = "Action")]
        public string Action { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModNavigationClass //Navigation Class
    {
        [Key]
        [Column("NavigationClassId", Order = 0)]
        public int NavigationClassId { get; set; }
        
        [Required]
        [Column("NavigationClassName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Navigation Class Name")]
        public string NavigationClassName { get; set; }

        [Required]
        [Column("NavigationClassNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Navigation Class Name")]
        public string NavigationClassNameBn { get; set; }
    }
}
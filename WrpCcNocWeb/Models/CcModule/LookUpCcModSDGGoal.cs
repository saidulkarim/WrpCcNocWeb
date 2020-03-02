using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModSDGGoal
    {
        [Key]
        [Column("SDGGoalId", Order = 0)]        
        public int SDGGoalId { get; set; }


        [Required]
        [Column("SDGGoalNumber", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "SDG Goal Number")]
        public string SDGGoalNumber { get; set; }
        
        
        [Column("SDGDocLink", Order = 2)]
        [MaxLength(250)]
        [Display(Name = "SDG Documents Link")]
        public string SDGDocLink { get; set; }
    }
}

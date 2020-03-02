using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModDrainageCondition
    {
        [Key]
        [Column("DrainageConditionId", Order = 0)]        
        public int DrainageConditionId { get; set; }


        [Required]
        [Column("DrainageCondition", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Drainage Condition")]
        public string DrainageCondition { get; set; }
    }
}

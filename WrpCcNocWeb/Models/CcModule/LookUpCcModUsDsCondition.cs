using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModUsDsCondition //US DS Condition 
    {
        [Key]
        [Column("UsDsConditionId", Order = 0)]
        public int UsDsConditionId { get; set; }
        
        [Required]
        [Column("ParameterName", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Parameter Name")]
        public string ParameterName { get; set; }

        [Required]
        [Column("ParameterNameBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Parameter Name")]
        public string ParameterNameBn { get; set; }
    }
}
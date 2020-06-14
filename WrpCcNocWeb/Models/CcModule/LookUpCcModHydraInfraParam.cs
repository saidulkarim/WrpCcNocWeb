using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModHydraInfraParam //hydraulic infrastructure Param
    {
        [Key]
        [Column("HydraInfraParamId", Order = 0)]
        public long HydraInfraParamId { get; set; }
        
        [Required]
        [Column("ParameterName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Parameter Name")]
        public string ParameterName { get; set; }
                
        [Column("ParameterNameBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Parameter Name")]
        public string ParameterNameBn { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModGrndWtrWthdrwParam  //ground water withdrawal 
    {
        [Key]
        [Column("GrndWtrWithdrawalParamId", Order = 0)]
        public int GrndWtrWithdrawalParamId { get; set; }
        
        [Required]
        [Column("ParameterName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Parameter")]
        public string ParameterName { get; set; }
                
        [Column("ParameterNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Parameter")]
        public string ParameterNameBn { get; set; }
    }
}
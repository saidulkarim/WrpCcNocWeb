using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModDredgingPlanParam //Planning of Dredging
    {
        [Key]
        [Column("DredgingPlanParamId", Order = 0)]
        public int DredgingPlanParamId { get; set; }

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
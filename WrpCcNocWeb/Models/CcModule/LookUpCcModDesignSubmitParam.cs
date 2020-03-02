using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModDesignSubmitParam
    {
        [Key]
        [Column("DesignSubmittedParameterId", Order = 0)]        
        public int DesignSubmittedParameterId { get; set; }


        [Required]
        [Column("ParameterName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Parameter Name")]
        public string ParameterName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModSIAParameter
    {
        [Key]
        [Column("SIAParameterId", Order = 0)]        
        public int SIAParameterId { get; set; }


        [Required]
        [Column("SIAParameterName", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "SIA Parameter Name")]
        public string SIAParameterName { get; set; }


        [Column("SIAParameterNameBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "SIA Parameter Name")]
        public string SIAParameterNameBn { get; set; }
    }
}

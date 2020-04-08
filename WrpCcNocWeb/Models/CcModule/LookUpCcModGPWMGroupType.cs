using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModGPWMGroupType
    {
        [Key]
        [Column("GPWMGroupTypeId", Order = 0)]       
        public int GPWMGroupTypeId { get; set; }


        [Required]
        [Column("GPWMGroupTypeName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "GPWM Group Type Name")]
        public string GPWMGroupTypeName { get; set; }


        [Column("GPWMGroupTypeNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "GPWM Group Type Name")]
        public string GPWMGroupTypeNameBn { get; set; }
    }
}

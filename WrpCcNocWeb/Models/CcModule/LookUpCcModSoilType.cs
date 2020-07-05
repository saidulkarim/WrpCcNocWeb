using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModSoilType //Soil Type
    {
        [Key]
        [Column("SoilTypeId", Order = 0)]
        public int SoilTypeId { get; set; }
        
        [Required]
        [Column("SoilTypeName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Soil Type Name")]
        public string SoilTypeName { get; set; }

        [Required]
        [Column("SoilTypeNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Soil Type Name")]
        public string SoilTypeNameBn { get; set; }
    }
}
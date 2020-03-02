using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModLandType
    {
        [Key]
        [Column("LandTypeId", Order = 0)]        
        public int LandTypeId { get; set; }


        [Required]
        [Column("LandTypeName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Land Type Name")]
        public string LandTypeName { get; set; }
    }
}

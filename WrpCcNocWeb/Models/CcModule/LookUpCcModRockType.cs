using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModRockType
    {
        [Key]
        [Column("RockTypeID", Order = 0)]        
        public int RockTypeID { get; set; }


        [Required]
        [Column("RockTypeName", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Rock Type Name")]
        public string RockTypeName { get; set; }


        [Column("RockTypeNameBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Rock Type Name")]
        public string RockTypeNameBn { get; set; }
    }
}

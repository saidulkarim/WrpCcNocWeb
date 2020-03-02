using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModTypeOfFlood
    {
        [Key]
        [Column("FloodTypeId", Order = 0)]        
        public int FloodTypeId { get; set; }


        [Required]
        [Column("FloodTypeName", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Flood Type Name")]
        public string FloodTypeName { get; set; }
    }
}

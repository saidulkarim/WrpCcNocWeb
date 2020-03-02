using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModWaterUse
    {

        [Key]
        [Column("WaterUseId", Order = 0)]        
        public int WaterUseId { get; set; }


        [Required]
        [Column("WaterUseName", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Water Use Name")]
        public string WaterUseName { get; set; }
    }
}

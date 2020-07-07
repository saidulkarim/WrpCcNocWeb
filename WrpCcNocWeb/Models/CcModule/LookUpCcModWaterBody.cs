using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModWaterBody
    {
        [Key]
        [Column("WaterBodyId", Order = 0)]
        public int WaterBodyId { get; set; }

        [Required]
        [Column("NameOfWaterBody", Order = 1)]
        [Display(Name = "Name Of The Water Body")]
        [MaxLength(100)]
        public string NameOfWaterBody { get; set; }

        [Required]
        [Column("NameOfWaterBodyBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Name Of The Water Body")]
        public string NameOfWaterBodyBn { get; set; }
    }
}

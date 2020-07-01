using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModWaterUseSector //Water use Sector
    {
        [Key]
        [Column("WaterUseSectorId", Order = 0)]
        public int WaterUseSectorId { get; set; }
        
        [Required]
        [Column("WaterUseSectorName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Water Use Sector")]
        public string WaterUseSectorName { get; set; }
                
        [Column("WaterUseSectorNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Water Use Sector")]
        public string WaterUseSectorNameBn { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModWaterUseConsumption //Water use Sector
    {
        [Key]
        [Column("WaterUseConsumptionId", Order = 0)]
        public int WaterUseConsumptionId { get; set; }
        
        [Required]
        [Column("ConsumptionName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Consumption Name")]
        public string ConsumptionName { get; set; }

        [Required]
        [Column("ConsumptionNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Consumption Name")]
        public string ConsumptionNameBn { get; set; }
    }
}
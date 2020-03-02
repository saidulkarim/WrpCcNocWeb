using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModHydroRegion
    {
        [Key]
        [Column("HydroRegionId", Order = 0)]       
        public int HydroRegionId { get; set; }


        [Required]
        [Column("HydroRegionName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Hydrological Region")]
        public string HydroRegionName { get; set; }
    }
}
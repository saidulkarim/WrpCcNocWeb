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
        [Column("HydroRegionShortName", Order = 1)]
        [MaxLength(20)]
        [Display(Name = "Hydrological Region")]
        public string HydroRegionShortName { get; set; }
        

        [Column("HydroRegionFullName", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Hydrological Region")]
        public string HydroRegionFullName { get; set; }


        [Column("HydroRegionShortNameBn", Order = 3)]
        [MaxLength(20)]
        [Display(Name = "Hydrological Region")]
        public string HydroRegionShortNameBn { get; set; }


        [Column("HydroRegionFullNameBn", Order = 4)]
        [MaxLength(50)]
        [Display(Name = "Hydrological Region")]
        public string HydroRegionFullNameBn { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjHydroRegionDetail
    {
        [Key]
        [Column("PrjHydroRegionDetailId", Order = 0)]
        public long PrjHydroRegionDetailId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Column("HydroRegionId", Order = 2)]
        [Display(Name = "Hydrological Region")]
        public int HydroRegionId { get; set; }
        [ForeignKey("HydroRegionId")]
        public virtual LookUpCcModHydroRegion LookUpCcModHydroRegion { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModBDP2100HotSpotDetail
    {
        [Key]
        [Column("HotSpotDetailId", Order = 0)]
        public long HotSpotDetailId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("DeltaPlanHotSpotId", Order = 2)]
        [Display(Name = "Delta Plan HotSpot")]
        public int DeltaPlanHotSpotId { get; set; }
        [ForeignKey("DeltaPlanHotSpotId")]
        public virtual LookUpCcModDeltPlan2100HotSpot LookUpCcModDeltPlan2100HotSpot { get; set; }
    }
}

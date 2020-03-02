using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjTypesOfFloodDetail
    {
        [Key]
        [Column("FloodTypeDetailId", Order = 0)]
        public long FloodTypeDetailId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Column("FloodTypeId", Order = 2)]
        [Display(Name = "Flood Type")]
        public int FloodTypeId { get; set; }
        [ForeignKey("FloodTypeId")]
        public virtual LookUpCcModTypeOfFlood LookUpCcModTypeOfFlood { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModRiverTypeDetail
    {
        [Key]
        [Column("RiverTypeDetailId", Order = 0)]
        public long RiverTypeDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("RiverTypeId", Order = 2)]
        [Display(Name = "River Type")]
        public int RiverTypeId { get; set; }
        [ForeignKey("RiverTypeId")]
        public virtual LookUpCcModRiverType LookUpCcModRiverType { get; set; }
    }
}
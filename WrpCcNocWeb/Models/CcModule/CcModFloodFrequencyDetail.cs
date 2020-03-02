using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModFloodFrequencyDetail
    {
        [Key]
        [Column("FloodFrequencyDetailId", Order = 0)]
        public long FloodFrequencyDetailId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("FloodFrequencyId", Order = 2)]
        [Display(Name = "Flood Frequency")]
        public int FloodFrequencyId { get; set; }
        [ForeignKey("FloodFrequencyId")]
        public virtual LookUpCcModFloodFrequency LookUpCcModFloodFrequency { get; set; }
    }
}

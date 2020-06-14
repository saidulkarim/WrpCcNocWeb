using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModConservLocationDetail //Conservation location
    {
        [Key]
        [Column("ConservLocationDetailId", Order = 0)]
        public long ConservLocationDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("ConservLocationId", Order = 2)]
        [Display(Name = "Conservation Location")]
        public int ConservLocationId { get; set; }
        [ForeignKey("ConservLocationId")]
        public virtual LookUpCcModConservLocation LookUpCcModConservLocation { get; set; }
    }
}
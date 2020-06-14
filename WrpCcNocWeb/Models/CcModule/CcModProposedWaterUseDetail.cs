using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModProposedWaterUseDetail
    {
        [Key]
        [Column("ProposedWaterUseDetailId", Order = 0)]
        public long ProposedWaterUseDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("ProposedWaterUseId", Order = 2)]
        [Display(Name = "Proposed Water Use")]
        public int ProposedWaterUseId { get; set; }
        [ForeignKey("ProposedWaterUseId")]
        public virtual LookUpCcModProposedWaterUse LookUpCcModProposedWaterUse { get; set; }
    }
}
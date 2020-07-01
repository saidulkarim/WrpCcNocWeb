using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModPurposeOfWaterUseDetail
    {
        [Key]
        [Column("PurposeOfWaterUseDetailId", Order = 0)]
        public long PurposeOfWaterUseDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("PurposeWaterUseId", Order = 2)]
        [Display(Name = "Purpose Water Use")]
        public int PurposeWaterUseId { get; set; }
        [ForeignKey("PurposeWaterUseId")]
        public virtual LookUpCcModPurposeOfWaterUse LookUpCcModPurposeOfWaterUse { get; set; }		
    }
}
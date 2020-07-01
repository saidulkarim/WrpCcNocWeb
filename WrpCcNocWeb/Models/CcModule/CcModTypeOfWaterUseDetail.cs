using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModTypeOfWaterUseDetail
    {
        [Key]
        [Column("TypeOfWaterUseDetailId", Order = 0)]
        public long TypeOfWaterUseDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("TypeOfWaterUseId", Order = 2)]
        [Display(Name = "Type of Water Use")]
        public int TypeOfWaterUseId { get; set; }
        [ForeignKey("TypeOfWaterUseId")]
        public virtual LookUpCcModTypeOfWaterUse LookUpCcModTypeOfWaterUse { get; set; }
    }
}
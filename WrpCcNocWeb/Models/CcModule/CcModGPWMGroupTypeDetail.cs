using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModGPWMGroupTypeDetail
    {
        [Key]
        [Column("GPWMGroupTypeDetailId", Order = 0)]
        public long GPWMGroupTypeDetailId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("GPWMGroupTypeId", Order = 2)]
        [Display(Name = "GPWM Group Type")]
        public int GPWMGroupTypeId { get; set; }
        [ForeignKey("GPWMGroupTypeId")]
        public virtual LookUpCcModGPWMGroupType LookUpCcModGPWMGroupType { get; set; }
    }
}

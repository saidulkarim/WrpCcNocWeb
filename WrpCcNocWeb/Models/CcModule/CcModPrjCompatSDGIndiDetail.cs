using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WrpCcNocWeb.Models
{
    public class CcModPrjCompatSDGIndiDetail
    {
        [Key]
        [Column("SDGIndicatorDetailId", Order = 0)]
        public long SDGIndicatorDetailId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Column("SDGIndicatorId", Order = 2)]
        [Display(Name = "SDG Indicator")]
        public int SDGIndicatorId { get; set; }
        [ForeignKey("SDGIndicatorId")]
        public virtual LookUpCcModSDGIndicator LookUpCcModSDGIndicator { get; set; }
    }
}

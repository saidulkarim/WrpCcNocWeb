using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WrpCcNocWeb.Models
{
    public class CcModPrjCompatSDGDetail
    {
        [Key]
        [Column("SDGCompabilityId", Order = 0)]
        public long SDGCompabilityId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Column("SDGGoalId", Order = 2)]
        [Display(Name = "SDG Goal")]
        public int SDGGoalId { get; set; }
        [ForeignKey("SDGGoalId")]
        public virtual LookUpCcModSDGGoal LookUpCcModSDGGoal { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModBDP2100GoalDetail
    {
        [Key]
        [Column("DeltaGoalDetailId", Order = 0)]
        public long DeltaGoalDetailId { get; set; }
        
        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("DeltPlan2100GoalId", Order = 2)]
        [Display(Name = "Delt Plan 2100 Goal")]
        public int DeltPlan2100GoalId { get; set; }
        [ForeignKey("DeltPlan2100GoalId")]
        public virtual LookUpCcModDeltPlan2100Goal LookUpCcModDeltPlan2100Goal { get; set; }
    }
}

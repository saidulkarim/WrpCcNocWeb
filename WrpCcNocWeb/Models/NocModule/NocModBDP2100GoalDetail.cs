using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModBDP2100GoalDetail
    {
        [Key]
        [Column("DeltaGoalDetailId", Order = 0)]
        public long DeltaGoalDetailId { get; set; }
        
        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "NocApplicationId")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual NocModAppCommonDetail NocModAppCommonDetail { get; set; }

        [Required]
        [Column("DeltPlan2100GoalId", Order = 2)]
        [Display(Name = "Delt Plan 2100 Goal")]
        public int DeltPlan2100GoalId { get; set; }
        [ForeignKey("DeltPlan2100GoalId")]
        public virtual LookUpCcModDeltPlan2100Goal LookUpCcModDeltPlan2100Goal { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModDeltPlan2100Goal
    {
        [Key]
        [Column("DeltPlan2100GoalId", Order = 0)]        
        public int DeltPlan2100GoalId { get; set; }

        [Required]
        [Column("DeltPlan2100Goal", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Delta Plan 2100 Goal")]
        public string DeltPlan2100Goal { get; set; }

        [Required]
        [Column("DeltPlan2100GoaDocLink", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Delta Plan 2100 Goal Doc Link")]
        public string DeltPlan2100GoaDocLink { get; set; }

        [Column("DeltPlan2100GoalBn", Order = 3)]
        [MaxLength(100)]
        [Display(Name = "Delta Plan 2100 Goal")]
        public string DeltPlan2100GoalBn { get; set; }
    }
}

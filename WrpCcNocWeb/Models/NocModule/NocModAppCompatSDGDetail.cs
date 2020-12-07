using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModAppCompatSDGDetail
    {
        [Key]
        [Column("SDGCompabilityId", Order = 0)]
        public long SDGCompabilityId { get; set; }

        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "NocApplicationId")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Column("SDGGoalId", Order = 2)]
        [Display(Name = "SDG Goal")]
        public int SDGGoalId { get; set; }
        [ForeignKey("SDGGoalId")]
        public virtual LookUpCcModSDGGoal LookUpCcModSDGGoal { get; set; }
    }
}
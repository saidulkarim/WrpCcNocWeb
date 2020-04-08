using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModDeltPlan2100HotSpot
    {
        [Key]
        [Column("DeltaPlanHotSpotId", Order = 0)]        
        public int DeltaPlanHotSpotId { get; set; }
        
        [Required]
        [Column("PlanName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Delta Plan Name")]
        public string PlanName { get; set; }

        [Column("PlanNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Delta Plan Name")]
        public string PlanNameBn { get; set; }
    }
}

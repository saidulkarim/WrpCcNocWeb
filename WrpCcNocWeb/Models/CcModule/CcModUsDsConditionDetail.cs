using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModUsDsConditionDetail //US and DS condition for proposed intervention 
    {
        [Key]
        [Column("UsDsConditionDetailId", Order = 0)]
        public long UsDsConditionDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("UsDsConditionId", Order = 2)]
        [Display(Name = "US DS Condition Parameter")]
        public int UsDsConditionId { get; set; }
        [ForeignKey("UsDsConditionId")]
        public virtual LookUpCcModUsDsCondition LookUpCcModUsDsCondition { get; set; }
				
        [Column("UsParameterValue", Order = 3)]
        [MaxLength(150)]
        [Display(Name = "US Parameter Value")]
        public string UsParameterValue { get; set; }       
    }
}
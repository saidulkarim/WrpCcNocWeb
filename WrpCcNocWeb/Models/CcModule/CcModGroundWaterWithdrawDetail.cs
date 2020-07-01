using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModGroundWaterWithdrawDetail
    {
        [Key]
        [Column("GroundWaterWithdrawDetailId", Order = 0)]
        public long GroundWaterWithdrawDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("GrndWtrWithdrawalParamId", Order = 2)]
        [Display(Name = "Parameter")]
        public int GrndWtrWithdrawalParamId { get; set; }
        [ForeignKey("GrndWtrWithdrawalParamId")]
        public virtual LookUpCcModGrndWtrWthdrwParam LookUpCcModGrndWtrWthdrwParam { get; set; }
		
		[Column("ExistingInfrastructure", Order = 3)]
        [Display(Name = "Existing Infrastructure")]
        [MaxLength(150)]
        public string ExistingInfrastructure { get; set; }
		
		[Column("ProposedInfrastructure", Order = 4)]
        [Display(Name = "Proposed Infrastructure")]
        [MaxLength(150)]
        public string ProposedInfrastructure { get; set; }        
    }
}
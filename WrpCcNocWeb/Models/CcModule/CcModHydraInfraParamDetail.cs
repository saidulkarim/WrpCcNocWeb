using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModHydraInfraParamDetail //Design parameters of hydraulic infrastructure
    {
        [Key]
        [Column("HydraInfraParamDetailId", Order = 0)]
        public long HydraInfraParamDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("HydraInfraParamId", Order = 2)]
        [Display(Name = "Parameter Name")]
        public int HydraInfraParamId { get; set; }
        [ForeignKey("HydraInfraParamId")]
        public virtual LookUpCcModHydraInfraParam LookUpCcModHydraInfraParam { get; set; }
		
        [Required]
        [Column("Description", Order = 3)]
        [MaxLength(250)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
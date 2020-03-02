using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjEIADetail
    {
        [Key]
        [Column("EIAId", Order = 0)]
        public long EIAId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Column("EIAParameterId", Order = 2)]
        [Display(Name = "EIA Parameter")]
        public int EIAParameterId { get; set; }
        [ForeignKey("EIAParameterId")]
        public virtual LookUpCcModEIAParameter LookUpCcModEIAParameter { get; set; }


        [Column("PreProjectSituation", Order = 3)]
        [MaxLength(150)]
        [Display(Name = "Pre-Project Situation")]
        public string PreProjectSituation { get; set; }


        [Column("PostProjectSituation", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "Post-Project Situation")]
        public string PostProjectSituation { get; set; }


        [Column("PositiveNegativeImpact", Order = 5)]
        [MaxLength(150)]
        [Display(Name = "Positive Negative Impact")]
        public string PositiveNegativeImpact { get; set; }


        [Column("MitigationPlan", Order = 6)]
        [MaxLength(150)]
        [Display(Name = "Mitigation Plan")]
        public string MitigationPlan { get; set; }
    }
}

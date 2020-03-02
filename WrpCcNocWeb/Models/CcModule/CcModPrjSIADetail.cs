using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjSIADetail
    {
        [Key]
        [Column("SIAId", Order = 0)]
        public long SIAId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("SIAParameterId", Order = 2)]
        [Display(Name = "SIA Parameter")]
        public int SIAParameterId { get; set; }
        [ForeignKey("SIAParameterId")]
        public virtual LookUpCcModSIAParameter LookUpCcModSIAParameter { get; set; }


        [Column("PreProjectSituation", Order = 3)]
        [MaxLength(150)]
        [Display(Name = "PreProject Situation")]
        public string PreProjectSituation { get; set; }


        [Column("PostProjectSituation", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "PostProject Situation")]
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModTypeOfDredgingPlanDetail
    {
        [Key]
        [Column("DredgingPlanDetailId", Order = 0)]
        public long DredgingPlanDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("DredgingPlanParamId", Order = 2)]
        [Display(Name = "Parameter")]
        public int DredgingPlanParamId { get; set; }
        [ForeignKey("DredgingPlanParamId")]
        public virtual LookUpCcModDredgingPlanParam LookUpCcModDredgingPlanParam { get; set; }

        [Column("ApplicantComment", Order = 3)]
        [MaxLength(100)]
        [Display(Name = "Applicant Comment")]
        public string ApplicantComment { get; set; }

        [Column("AuthorityComment", Order = 4)]
        [MaxLength(100)]
        [Display(Name = "Authority Comment")]
        public string AuthorityComment { get; set; }
    }
}
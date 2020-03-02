using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModDesignSubmitDetail
    {
        [Key]
        [Column("DesignSubmittedId", Order = 0)]
        public long DesignSubmittedId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("DesignSubmittedParameterId", Order = 2)]
        [Display(Name = "Design Submitted Parameter")]
        public int DesignSubmittedParameterId { get; set; }
        [ForeignKey("DesignSubmittedParameterId")]
        public virtual LookUpCcModDesignSubmitParam LookUpCcModDesignSubmitParam { get; set; }


        [Required]
        [Column("YesNoId", Order = 3)]
        [Display(Name = "Was there Design Submitted with this Parameter?")]
        public int YesNoId { get; set; }
        [ForeignKey("YesNoId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoDesign { get; set; }


        [Column("DesignSubmitApplicantCmt", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "Applicant Comments")]        
        public string DesignSubmitApplicantCmt { get; set; }


        [Column("DesignSubmitAuthorityCmt", Order = 5)]
        [MaxLength(150)]
        [Display(Name = "Authority Comments")]
        public string DesignSubmitAuthorityCmt { get; set; }
    }
}

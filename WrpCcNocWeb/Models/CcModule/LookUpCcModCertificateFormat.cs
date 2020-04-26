using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.CcModule
{
    public class LookUpCcModCertificateFormat
    {
        [Key]
        [Column("CertificateFormatId", Order = 0)]
        public int CertificateFormatId { get; set; }

        [Required]
        [Column("RuleAsPerGazette", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Rule As Per Gazette")]
        public string RuleAsPerGazette { get; set; }

        [Column("ProjectTypeId", Order = 2)]
        [Display(Name = "Project Type")]
        public long? ProjectTypeId { get; set; }
        [ForeignKey("ProjectTypeId")]
        public virtual LookUpCcModProjectType LookUpCcModProjectType { get; set; }
                
        [Required]
        [Column("CertificateBody", Order = 3)]
        [MaxLength(200)]        
        public string CertificateBody { get; set; }

        [Required]
        [Column("TermsConditions", Order = 4)]
        [MaxLength(550)]
        public string TermsConditions { get; set; }
    }
}

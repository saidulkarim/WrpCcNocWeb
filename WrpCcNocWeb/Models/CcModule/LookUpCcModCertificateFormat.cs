using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModCertificateFormat
    {
        [Key]
        [Column("CertificateFormatId", Order = 0)]
        public int CertificateFormatId { get; set; }

        [Required]
        [Column("ProjectTypeId", Order = 1)]
        [Display(Name = "Project Type")]
        public int ProjectTypeId { get; set; }
        [ForeignKey("ProjectTypeId")]
        public virtual LookUpCcModProjectType LookUpCcModProjectType { get; set; }

        [Required]
        [Column("RuleAsPerGazette", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Rule As Per Gazette")]
        public string RuleAsPerGazette { get; set; }

        [Required]
        [Column("RuleAsPerGazetteBn", Order = 3)]
        [MaxLength(100)]
        [Display(Name = "Rule As Per Gazette")]
        public string RuleAsPerGazetteBn { get; set; }

        [Required]
        [Column("CertificateBody", Order = 4)]
        [MaxLength(500)]        
        public string CertificateBody { get; set; }

        [Required]
        [Column("CertificateBodyBn", Order = 5)]
        [MaxLength(500)]
        public string CertificateBodyBn { get; set; }

        [Required]
        [Column("TermsConditions", Order = 6)]
        [MaxLength(1000)]
        public string TermsConditions { get; set; }

        [Required]
        [Column("TermsConditionsBn", Order = 7)]
        [MaxLength(1000)]
        public string TermsConditionsBn { get; set; }
    }
}

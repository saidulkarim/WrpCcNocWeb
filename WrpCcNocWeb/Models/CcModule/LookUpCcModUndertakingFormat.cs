using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModUndertakingFormat
    {
        [Key]
        [Column("UndertakingFormatId", Order = 0)]
        public int UndertakingFormatId { get; set; }

        [Required]
        [Column("ProjectTypeId", Order = 1)]
        [Display(Name = "Project Type")]
        public int ProjectTypeId { get; set; }
        [ForeignKey("ProjectTypeId")]
        public virtual LookUpCcModProjectType LookUpCcModProjectType { get; set; }

        [Required]
        [Column("FormNo", Order = 2)]
        [MaxLength(20)]
        [Display(Name = "Form No")]
        public string FormNo { get; set; }

        [Required]
        [Column("FormNoBn", Order = 3)]
        [MaxLength(20)]
        [Display(Name = "Form No (Bangla)")]
        public string FormNoBn { get; set; }

        [Required]
        [Column("RuleAsPerGazette", Order = 4)]
        [MaxLength(100)]
        [Display(Name = "Rule As Per Gazette")]
        public string RuleAsPerGazette { get; set; }

        [Required]
        [Column("RuleAsPerGazetteBn", Order = 5)]
        [MaxLength(100)]
        [Display(Name = "Rule As Per Gazette")]
        public string RuleAsPerGazetteBn { get; set; }

        [Required]
        [Column("CertificateBody", Order = 6)]
        [MaxLength(1000)]
        public string CertificateBody { get; set; }

        [Required]
        [Column("CertificateBodyBn", Order = 7)]
        [MaxLength(1000)]
        public string CertificateBodyBn { get; set; }

        [Required]
        [Column("TermsConditions", Order = 8)]
        [MaxLength(2000)]
        public string TermsConditions { get; set; }

        [Required]
        [Column("TermsConditionsBn", Order = 9)]
        [MaxLength(2000)]
        public string TermsConditionsBn { get; set; }

        [Required]
        [Column("CertificateFooter", Order = 10)]
        [MaxLength(1000)]
        public string CertificateFooter { get; set; }

        [Required]
        [Column("CertificateFooterBn", Order = 11)]
        [MaxLength(1000)]
        public string CertificateFooterBn { get; set; }
    }
}
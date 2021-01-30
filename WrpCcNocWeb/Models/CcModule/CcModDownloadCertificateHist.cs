using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModDownloadCertificateHist
    {
        [Key]
        [Column("CcModDownCertificateHistId", Order = 0)]
        public long CcModDownCertificateHistId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Column("DownloadDateTime", Order = 2)]
        [Display(Name = "Download Date")]
        public DateTime? DownloadDateTime { get; set; }
    }
}
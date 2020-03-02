using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModAnalyzeOptionsDetail
    {
        [Key]
        [Column("AnalyzeOptionsId", Order = 0)]
        public long AnalyzeOptionsId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("OptionNumber", Order = 2)]
        [MaxLength(20)]
        [Display(Name = "Option Number")]
        public string OptionNumber { get; set; }


        [Column("AnalyzeDescription", Order = 3)]
        [MaxLength(200)]
        [Display(Name = "Analyze Description")]
        public string AnalyzeDescription { get; set; }


        [Column("AnalyzeRemarks", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "Analyze Remarks")]
        public string AnalyzeRemarks { get; set; }
    }
}

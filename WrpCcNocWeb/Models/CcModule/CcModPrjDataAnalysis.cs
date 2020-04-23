using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.CcModule
{
    public class CcModPrjDataAnalysis
    {
        [Key]
        [Column("ProjectDataAnalysisId", Order = 0)]
        public long ProjectDataAnalysisId { get; set; }
        
        [Required]
        [Column("UserId", Order = 1)]
        [Display(Name = "Name of User")]
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AdminModUsersDetail AdminModUsersDetail { get; set; }
        
        [Required]
        [Column("ProjectId", Order = 2)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }
        
        [Column("LabelNameOfControl", Order = 3)]        
        [MaxLength(100)]
        public string FormLabelName { get; set; }                     

        [Column("Comments", Order = 4)]
        [Display(Name = "Comments")]
        [MaxLength(250)]
        public string Comments { get; set; }

        [Column("DateOfAnalysis", Order = 5)]
        [Display(Name = "Date Of Analysis")]
        public DateTime? DateOfAnalysis { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProjDataAnalysis
    {
        [Key]
        [Column("AppProjDataAnalysisId", Order = 0)]
        public long AppProjDataAnalysisId { get; set; }

        [Required]
        [Column("UserId", Order = 1)]
        [Display(Name = "Name of User")]
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AdminModUsersDetail AdminModUsersDetail { get; set; }

        [Required]
        [Column("ProjectTypeId", Order = 2)]
        [Display(Name = "Project Type")]
        public int ProjectTypeId { get; set; }
        [ForeignKey("ProjectTypeId")]
        public virtual LookUpCcModProjectType LookUpCcModProjectType { get; set; }

        [Required]
        [Column("ProjectId", Order = 3)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("LabelNameOfControl", Order = 4)]
        [MaxLength(100)]
        public string LabelNameOfControl { get; set; }

        [Column("Comments", Order = 5)]
        [Display(Name = "Comments")]
        [MaxLength(350)]
        public string Comments { get; set; }

        [Column("DateOfAnalysis", Order = 6)]
        [Display(Name = "Date of Analysis")]
        public DateTime? DateOfAnalysis { get; set; }
    }
}
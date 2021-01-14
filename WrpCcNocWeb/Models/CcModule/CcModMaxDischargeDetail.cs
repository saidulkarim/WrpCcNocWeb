using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModMaxDischargeDetail
    {
        [Key]
        [Column("MaxDischargeDetailId", Order = 0)]
        public long MaxDischargeDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("DischargeYear", Order = 2)]
        [Display(Name = "Year")]
        public int DischargeYear { get; set; }

        [Column("DischargeAmount", Order = 3)]
        [Display(Name = "Discharge Amount (m3/s)")]
        public double? DischargeAmount { get; set; }
    }
}
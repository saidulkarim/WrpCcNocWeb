using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModWaterUseDetail
    {
        [Key]
        [Column("WaterUseDetailId", Order = 0)]
        public long WaterUseDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("WaterUseId", Order = 2)]
        [Display(Name = "Water Use")]
        public int WaterUseId { get; set; }
        [ForeignKey("WaterUseId")]
        public virtual LookUpCcModWaterUse LookUpCcModWaterUse { get; set; }
		
		[Column("ExistingDemand", Order = 3)]
        [Display(Name = "Existing Demand (m3/day)")]       
        public double? ExistingDemand { get; set; }
		
		[Column("ProposedDemand", Order = 4)]
        [Display(Name = "Proposed Demand (m3/day)")]       
        public double? ProposedDemand { get; set; }
		
		[Column("TotalDemand", Order = 5)]
        [Display(Name = "Total Demand (m3/day)")]       
        public double? TotalDemand { get; set; }
		
		[Column("YearConductingPeriod", Order = 6)]
        [Display(Name = "Year of Conducting Period")]       
        public double? YearConductingPeriod { get; set; }
		
		[Column("AnnualDemand", Order = 7)]
        [Display(Name = "Annual Demand (m3/year)")]       
        public double? AnnualDemand { get; set; }
    }
}
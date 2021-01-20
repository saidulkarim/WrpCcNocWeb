using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjEcoFinAnalysisDetail
    {
        [Key]
        [Column("EconomicalAndFinancialId", Order = 0)]
        public long EconomicalAndFinancialId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("EcoAndFinancialParamId", Order = 2)]
        [Display(Name = "Eco And Financial Parameter")]
        public int EcoAndFinancialParamId { get; set; }
        [ForeignKey("EcoAndFinancialParamId")]
        public virtual LookUpCcModEcoAndFinancial LookUpEcoAndFinancial { get; set; }

        [Column("EcoAndFinancialParamUnitValue", Order = 3)]
        [MaxLength(50)]
        [Display(Name = "Value (%)")]
        public string EcoAndFinancialParamUnitValue { get; set; }                        
    }
}
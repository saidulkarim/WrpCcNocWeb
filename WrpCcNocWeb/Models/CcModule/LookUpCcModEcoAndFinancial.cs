using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModEcoAndFinancial
    {
        [Key]
        [Column("EcoAndFinancialParamId", Order = 0)]        
        public int EcoAndFinancialParamId { get; set; }


        [Required]
        [Column("EcoAndFinancialParamName", Order = 1)]
        [Display(Name = "Eco And Financial Param Name")]
        public string EcoAndFinancialParamName { get; set; }


        [Column("EcoAndFinancialUnit", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Eco And Financial Unit")]
        public string EcoAndFinancialUnit { get; set; }


        [Column("EcoAndFinancialParamNameBn", Order = 3)]
        [Display(Name = "Eco And Financial Param Name")]
        public string EcoAndFinancialParamNameBn { get; set; }


        [Column("EcoAndFinancialUnitBn", Order = 4)]
        [MaxLength(50)]
        [Display(Name = "Eco And Financial Unit")]
        public string EcoAndFinancialUnitBn { get; set; }

    }
}

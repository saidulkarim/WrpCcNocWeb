using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.AdminModule;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models.NocModule.Lookup
{
    public class LookUpNocWithdrawalQuantity
    {
        [Key]
        [Column("WithdrawalQuantityId", Order = 0)]
        public int WithdrawalQuantityId { get; set; }

        [Column("QuantityName", Order = 1)]
        [Display(Name = "Quantity")]
		[MaxLength(100)]
        public string QuantityName { get; set; }
		
		[Column("QuantityNameBn", Order = 2)]
        [Display(Name = "Quantity (Bangla)")]
		[MaxLength(150)]
        public string QuantityNameBn { get; set; }       
    }
}
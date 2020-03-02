using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModBankStability
    {
        [Key]
        [Column("BankStabilityTypeId", Order = 0)]        
        public int BankStabilityTypeId { get; set; }


        [Required]
        [Column("BankStabilityType", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Bank Stability Type")]
        public string BankStabilityType { get; set; }
    }
}

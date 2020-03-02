using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModBankDocType
    {

        [Key]
        [Column("BankDocTypeId", Order = 0)]        
        public int BankDocTypeId { get; set; }


        [Required]
        [Column("BankDocType", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Bank Document Type")]
        public string BankDocType { get; set; }
    }
}

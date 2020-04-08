using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModBankLineShifting
    {
        [Key]
        [Column("BankLineShiftingId", Order = 0)]        
        public int BankLineShiftingId { get; set; }


        [Required]
        [Column("BankLineTitle", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Bank Line Title")]
        public string BankLineTitle { get; set; }


        [Column("BankLineTitleBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Bank Line Title")]
        public string BankLineTitleBn { get; set; }
    }
}

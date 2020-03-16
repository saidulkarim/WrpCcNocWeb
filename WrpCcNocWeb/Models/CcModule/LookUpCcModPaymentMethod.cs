using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModPaymentMethod
    {

        [Key]
        [Column("PaymentMethodId", Order = 0)]        
        public int PaymentMethodId { get; set; }


        [Required]
        [Column("PaymentMethod", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }
    }
}

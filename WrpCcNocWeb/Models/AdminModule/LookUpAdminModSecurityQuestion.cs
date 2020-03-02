using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminModSecurityQuestion
    {
        [Key]
        [Column("SecurityQuestionId", Order = 0)]
        public int SecurityQuestionId { get; set; }
        
        
        [Required]
        [Column("SecurityQuestion", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Security Question")]
        public string SecurityQuestion { get; set; }
    }
}
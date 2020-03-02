using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminModErrorSlvStatus
    {
        [Key]        
        [Column("ErrorSolvedStatusId", Order = 0)]
        public int ErrorSolvedStatusId { get; set; }
        
        [Required]        
        [Column("ErrorSolvedStatus", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Status")]
        public string ErrorSolvedStatus { get; set; }
    }
}

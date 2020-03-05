using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.CcModule
{
    public class LookUpCcModApplicationState
    {
        [Key]
        [Column("ApplicationStateId", Order = 0)]
        public int ApplicationStateId { get; set; }


        [Required]
        [Column("ApplicationState", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Status of Application")]
        public string ApplicationState { get; set; }


        [Column("InstructionForUser", Order = 2)]
        [MaxLength(550)]
        [Display(Name = "Instruction")]
        public string InstructionForUser { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModTypeProposedWell //Type of proposed well
    {
        [Key]
        [Column("TypeProposedWellId", Order = 0)]
        public int TypeProposedWellId { get; set; }
        
        [Required]
        [Column("ProposedWellName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Proposed Well Name")]
        public string ProposedWellName { get; set; }
                
        [Column("ProposedWellNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Proposed Well Name")]
        public string ProposedWellNameBn { get; set; }
    }
}
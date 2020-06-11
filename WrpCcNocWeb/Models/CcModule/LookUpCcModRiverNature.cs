using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModRiverNature
    {
        [Key]
        [Column("RiverNatureId", Order = 0)]        
        public int RiverNatureId { get; set; }


        [Required]
        [Column("RiverNatureTitle", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "River Nature")]
        public string RiverNatureTitle { get; set; }


        [Column("RiverNatureTitleBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "River Nature")]
        public string RiverNatureTitleBn { get; set; }
    }
}

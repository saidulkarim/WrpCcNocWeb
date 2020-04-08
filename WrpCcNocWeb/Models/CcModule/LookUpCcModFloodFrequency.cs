using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModFloodFrequency
    {
        [Key]
        [Column("FloodFrequencyId", Order = 0)]        
        public int FloodFrequencyId { get; set; }


        [Required]
        [Column("FloodFrequency", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Flood Frequency")]
        public string FloodFrequency { get; set; }


        [Column("FloodFrequencyBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Flood Frequency")]
        public string FloodFrequencyBn { get; set; }
    }
}

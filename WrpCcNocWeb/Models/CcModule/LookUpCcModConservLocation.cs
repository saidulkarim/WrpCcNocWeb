using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModConservLocation
    {
        [Key]
        [Column("ConservLocationId", Order = 0)]        
        public int ConservLocationId { get; set; }


        [Required]
        [Column("ConservLocation", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Conserv Location")]
        public string ConservLocation { get; set; }


        [Column("ConservLocationBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Conserv Location")]
        public string ConservLocationBn { get; set; }
    }
}

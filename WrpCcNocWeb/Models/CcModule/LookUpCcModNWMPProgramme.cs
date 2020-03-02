using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModNWMPProgramme
    {
        [Key]
        [Column("NWMPProgrammeId", Order = 0)]
        public int NWMPProgrammeId { get; set; }


        [Required]
        [Column("NWMPProgrammeTitle", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "NWMP Programme Title")]
        public string NWMPProgrammeTitle { get; set; }


        [Required]
        [Column("NWMPLink", Order = 2)]
        [MaxLength(250)]
        [Display(Name = "NWMP Link")]
        public string NWMPLink { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModSediOfRiverOrKhal
    {
        [Key]
        [Column("SedimentationId", Order = 0)]       
        public int SedimentationId { get; set; }


        [Required]
        [Column("SedimentationName", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Sedimentation Name")]
        public string SedimentationName { get; set; }


        [Column("SedimentationNameBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Sedimentation Name")]
        public string SedimentationNameBn { get; set; }
    }
}

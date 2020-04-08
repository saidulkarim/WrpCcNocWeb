using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModSurfWaterQuality
    {
        [Key]
        [Column("SurfaceWaterQualityId", Order = 0)]        
        public int SurfaceWaterQualityId { get; set; }


        [Required]
        [Column("ParameterName", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Parameter Name")]
        public string ParameterName { get; set; }


        [Column("ParameterNameBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Parameter Name")]
        public string ParameterNameBn { get; set; }
    }
}

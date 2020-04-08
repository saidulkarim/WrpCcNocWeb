using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModNameOfWaterBody
    {
        [Key]
        [Column("WaterBodyId", Order = 0)]        
        public int WaterBodyId { get; set; }


        [Required]
        [Column("WaterBodyName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Water Body Name")]
        public string WaterBodyName { get; set; }


        [Column("WaterBodyNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Water Body Name")]
        public string WaterBodyNameBn { get; set; }
    }
}

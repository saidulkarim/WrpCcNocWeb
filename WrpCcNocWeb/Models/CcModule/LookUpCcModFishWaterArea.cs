using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModFishWaterArea //Fish Water Area 
    {
        [Key]
        [Column("FishWaterAreaId", Order = 0)]
        public int FishWaterAreaId { get; set; }
        
        [Required]
        [Column("AreaName", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Name")]
        public string AreaName { get; set; }

        [Required]
        [Column("AreaNameBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Name")]
        public string AreaNameBn { get; set; }
    }
}
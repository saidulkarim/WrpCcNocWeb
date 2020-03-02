using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModPurposeOfWaterUse
    {
        [Key]
        [Column("PurposeWaterUseId", Order = 0)]
        public int PurposeWaterUseId { get; set; }


        [Required]
        [Column("WaterUsePurpose", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Water Use Purpose")]
        public string WaterUsePurpose { get; set; }
    }
}
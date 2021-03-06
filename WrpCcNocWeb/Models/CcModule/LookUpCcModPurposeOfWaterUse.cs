﻿using System;
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
        [Column("PurposeOfWaterUseId", Order = 0)]
        public int PurposeOfWaterUseId { get; set; }


        [Required]
        [Column("WaterUsePurpose", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Water Use Purpose")]
        public string WaterUsePurpose { get; set; }


        [Column("WaterUsePurposeBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Water Use Purpose")]
        public string WaterUsePurposeBn { get; set; }
    }
}
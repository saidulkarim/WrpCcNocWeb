﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModSDGIndicator
    {
        [Key]
        [Column("SDGIndicatorId", Order = 0)]        
        public int SDGIndicatorId { get; set; }


        [Required]
        [Column("SDGIndicatorName", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "SDG Indicator Name")]
        public string SDGIndicatorName { get; set; }
    }
}
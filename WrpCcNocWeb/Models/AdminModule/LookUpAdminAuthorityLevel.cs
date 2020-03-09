﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.AdminModule
{
    public class LookUpAdminAuthorityLevel
    {
        [Key]
        [Column("AuthorityLevelId", Order = 0)]
        public int AuthorityLevelId { get; set; }

        [Required]
        [Column("AuthorityLevel", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Authority Level")]
        public string AuthorityLevel { get; set; }
    }
}

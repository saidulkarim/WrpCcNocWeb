using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.CcModule
{
    public class LookUpCcModMonth
    {
        [Key]
        [Column("MonthId", Order = 0)]
        public int MonthId { get; set; }

        [Required]
        [Column("MonthShortName", Order = 1)]
        [MaxLength(10)]
        [Display(Name = "Month Name")]
        public string MonthShortName { get; set; }

        [Required]
        [Column("MonthName", Order = 2)]
        [MaxLength(20)]
        [Display(Name = "Month Name")]
        public string MonthName { get; set; }
        
        [Required]
        [Column("MonthShortNameBn", Order = 3)]
        [MaxLength(10)]
        [Display(Name = "Month Name")]
        public string MonthShortNameBn { get; set; }

        [Required]
        [Column("MonthNameBn", Order = 4)]
        [MaxLength(20)]
        [Display(Name = "Month Name")]
        public string MonthNameBn { get; set; }
    }
}
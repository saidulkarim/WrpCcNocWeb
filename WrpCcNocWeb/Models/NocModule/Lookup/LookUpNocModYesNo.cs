using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.NocModule.Lookup
{
    public class LookUpNocModYesNo
    {
        [Key]
        [Column("YesNoId", Order = 0)]        
        public int YesNoId { get; set; }

        [Required]
        [Column("YesNoTitle", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Title")]
        public string YesNoTitle { get; set; }

        [Column("YesNoTitleBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Title")]
        public string YesNoTitleBn { get; set; }
    }
}
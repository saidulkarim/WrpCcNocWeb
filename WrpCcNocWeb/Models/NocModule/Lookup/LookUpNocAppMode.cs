using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.AdminModule;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models.NocModule.Lookup
{
    public class LookUpNocAppMode
    {
        [Key]
        [Column("ModeId", Order = 0)]
        public int ModeId { get; set; }

        [Column("ModeName", Order = 1)]
        [Display(Name = "Mode Name")]
		[MaxLength(100)]
        public string ModeName { get; set; }
		
		[Column("ModeNameBn", Order = 2)]
        [Display(Name = "ModeName (Bangla)")]
		[MaxLength(150)]
        public string ModeNameBn { get; set; }       
    }
}
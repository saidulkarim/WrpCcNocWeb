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
    public class LookUpNocWellType
    {
        [Key]
        [Column("WellTypeId", Order = 0)]
        public int WellTypeId { get; set; }

        [Column("WellType", Order = 1)]
        [Display(Name = "Well Type")]
		[MaxLength(100)]
        public string WellType { get; set; }
		
		[Column("WellTypeBn", Order = 2)]
        [Display(Name = "Well Type (Bangla)")]
		[MaxLength(150)]
        public string WellTypeBn { get; set; }       
    }
}
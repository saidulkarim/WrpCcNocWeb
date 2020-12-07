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
    public class LookUpNocModHydroSystem
    {
        [Key]
        [Column("HydroSystemId", Order = 0)]
        public int HydroSystemId { get; set; }

        [Column("HydroSystemName", Order = 1)]
        [Display(Name = "Hydro System")]
		[MaxLength(100)]
        public string HydroSystemName { get; set; }
		
		[Column("HydroSystemNameBn", Order = 2)]
        [Display(Name = "Hydro System (Bangla)")]
		[MaxLength(150)]
        public string HydroSystemNameBn { get; set; }
    }
}
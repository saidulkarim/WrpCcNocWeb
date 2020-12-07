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
    public class LookUpNocAppObjective
    {
        [Key]
        [Column("ObjectiveId", Order = 0)]
        public int ObjectiveId { get; set; }

        [Column("ObjectiveName", Order = 1)]
        [Display(Name = "Objective")]
		[MaxLength(100)]
        public string ObjectiveName { get; set; }
		
		[Column("ObjectiveNameBn", Order = 2)]
        [Display(Name = "Objective (Bangla)")]
		[MaxLength(150)]
        public string ObjectiveNameBn { get; set; }       
    }
}
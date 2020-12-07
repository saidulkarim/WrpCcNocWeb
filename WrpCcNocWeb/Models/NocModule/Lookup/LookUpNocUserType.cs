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
    public class LookUpNocUserType
    {
        [Key]
        [Column("TubeWellUserTypeId", Order = 0)]
        public int TubeWellUserTypeId { get; set; }

        [Column("TubeWellUserType", Order = 1)]
        [Display(Name = "User Type")]
		[MaxLength(100)]
        public string TubeWellUserType { get; set; }
		
		[Column("TubeWellUserTypeBn", Order = 2)]
        [Display(Name = "User Type (Bangla)")]
		[MaxLength(150)]
        public string TubeWellUserTypeBn { get; set; }       
    }
}
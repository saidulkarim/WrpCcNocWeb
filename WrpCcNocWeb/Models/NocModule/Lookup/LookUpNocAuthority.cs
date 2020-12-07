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
    public class LookUpNocAuthority
    {
        [Key]
        [Column("AuthorityId", Order = 0)]
        public int AuthorityId { get; set; }

        [Column("AuthorityName", Order = 1)]
        [Display(Name = "Authority")]
		[MaxLength(100)]
        public string AuthorityName { get; set; }
		
		[Column("AuthorityNameBn", Order = 2)]
        [Display(Name = "Authority (Bangla)")]
		[MaxLength(150)]
        public string AuthorityNameBn { get; set; }       
    }
}
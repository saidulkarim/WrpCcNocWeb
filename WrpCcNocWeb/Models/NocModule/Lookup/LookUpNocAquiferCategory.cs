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
    public class LookUpNocAquiferCategory
    {
        [Key]
        [Column("AquiferCategoryId", Order = 0)]
        public int AquiferCategoryId { get; set; }

        [Column("AquiferCategory", Order = 1)]
        [Display(Name = "Aquifer Category")]
		[MaxLength(100)]
        public string AquiferCategory { get; set; }
		
		[Column("AquiferCategoryBn", Order = 2)]
        [Display(Name = "Aquifer Category (Bangla)")]
		[MaxLength(150)]
        public string AquiferCategoryBn { get; set; }       
    }
}
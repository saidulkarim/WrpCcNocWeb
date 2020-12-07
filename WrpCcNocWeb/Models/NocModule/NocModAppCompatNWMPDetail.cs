using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModAppCompatNWMPDetail
    {
        [Key]
        [Column("PrjCompatNWMPId", Order = 0)]
        public long PrjCompatNWMPId { get; set; }

        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "NoC Application")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual NocModAppCommonDetail NocModAppCommonDetail { get; set; }

        [Required]
        [Column("NWMPProgrammeId", Order = 2)]
        [Display(Name = "NWMP Programme")]
        public int NWMPProgrammeId { get; set; }
        [ForeignKey("NWMPProgrammeId")]
        public virtual LookUpCcModNWMPProgramme LookUpCcModNWMPProgramme { get; set; }
    }
}
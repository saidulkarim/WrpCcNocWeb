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
    public class LookUpNocModGrndWtrQtyParams
    {
        [Key]
        [Column("GroundWaterQualityId", Order = 0)]
        public int GroundWaterQualityId { get; set; }

        [Column("ParameterName", Order = 1)]
        [Display(Name = "Parameter")]
		[MaxLength(100)]
        public string ParameterName { get; set; }
		
		[Column("ParameterNameBn", Order = 2)]
        [Display(Name = "Parameter (Bangla)")]
		[MaxLength(150)]
        public string ParameterNameBn { get; set; }       
    }
}
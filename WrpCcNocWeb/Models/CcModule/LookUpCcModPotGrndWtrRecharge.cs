using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModPotGrndWtrRecharge
    {
        [Key]
        [Column("PotGrndWtrRechargeId", Order = 0)]
        [DataType(DataType.Text)]
        public int PotGrndWtrRechargeId { get; set; }


        [Required]
        [Column("IndicatorName", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Indicator Name")]
        public string IndicatorName { get; set; }
    }
}

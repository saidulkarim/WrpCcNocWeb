using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModProposedWaterUse
    {
        [Key]
        [Column("ProposedWaterUseId", Order = 0)]        
        public int ProposedWaterUseId { get; set; }


        [Required]
        [Column("WaterUseTypeName", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Water Use Type Name")]
        public string WaterUseTypeName { get; set; }


        [Column("WaterUseTypeNameBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Water Use Type Name")]
        public string WaterUseTypeNameBn { get; set; }
    }
}

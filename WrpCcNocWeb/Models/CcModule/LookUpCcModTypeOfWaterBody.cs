using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModTypeOfWaterBody
    {
        [Key]
        [Column("WaterBodyTypeId", Order = 0)]        
        public int WaterBodyTypeId { get; set; }


        [Required]
        [Column("WaterBodyType", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Water Body Type")]
        public string WaterBodyType { get; set; }


        [Column("WaterBodyTypeBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Water Body Type")]
        public string WaterBodyTypeBn { get; set; }
    }
}

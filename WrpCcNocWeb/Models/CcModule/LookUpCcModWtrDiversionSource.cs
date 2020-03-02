using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModWtrDiversionSource
    {
        [Key]
        [Column("WaterDiversionSourceId", Order = 0)]       
        public int WaterDiversionSourceId { get; set; }


        [Required]
        [Column("SourceName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Source Name")]
        public string SourceName { get; set; }
    }
}

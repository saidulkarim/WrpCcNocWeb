using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModRiverType
    {
        [Key]
        [Column("RiverTypeId", Order = 0)]        
        public int RiverTypeId { get; set; }


        [Required]
        [Column("RiverTypeName", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "River Type Name")]
        public string RiverTypeName { get; set; }


        [Column("RiverTypeNameBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "River Type Name")]
        public string RiverTypeNameBn { get; set; }
    }
}

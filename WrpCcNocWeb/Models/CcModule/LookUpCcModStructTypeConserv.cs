using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModStructTypeConserv
    {
        [Key]
        [Column("TypeOfStructureId", Order = 0)]        
        public int TypeOfStructureId { get; set; }


        [Required]
        [Column("StructureName", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Structure Name")]
        public string StructureName { get; set; }


        [Column("StructureNameBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Structure Name")]
        public string StructureNameBn { get; set; }
    }
}

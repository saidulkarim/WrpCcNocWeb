using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModStructTypeConservDetail //Type of structure for conservation 
    {
        [Key]
        [Column("StructTypeConservDetailId", Order = 0)]
        public long StructTypeConservDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("TypeOfStructureId", Order = 2)]
        [Display(Name = "Type of Structure for Conservation")]
        public int TypeOfStructureId { get; set; }
        [ForeignKey("TypeOfStructureId")]
        public virtual LookUpCcModStructTypeConserv LookUpCcModStructTypeConserv { get; set; }
    }
}
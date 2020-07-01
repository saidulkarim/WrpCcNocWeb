using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModSoilTypeDetail
    {
        [Key]
        [Column("SoilTypeDetailId", Order = 0)]
        public long SoilTypeDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("SoilTypeId", Order = 2)]
        [Display(Name = "Soil Type")]
        public int SoilTypeId { get; set; }
        [ForeignKey("SoilTypeId")]
        public virtual LookUpCcModSoilType LookUpCcModSoilType { get; set; }		
    }
}
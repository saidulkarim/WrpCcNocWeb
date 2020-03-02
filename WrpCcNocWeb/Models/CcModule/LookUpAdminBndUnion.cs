using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminBndUnion
    {
        [Key]
        [Column("UnionGeoCode", Order = 0)]
        [MaxLength(8)]
        public string UnionGeoCode { get; set; }
        
        [Required]
        [Column("UpazilaGeoCode", Order = 1)]
        [Display(Name = "Upazila")]
        [MaxLength(6)]
        public string UpazilaGeoCode { get; set; }
        [ForeignKey("UpazilaGeoCode")]
        public virtual LookUpAdminBndUpazila LookUpAdminBndUpazila { get; set; }

        [Required]
        [Column("UnionName", Order = 2)]
        [Display(Name = "Union Name")]
        [MaxLength(50)]
        public string UnionName { get; set; }
    }
}

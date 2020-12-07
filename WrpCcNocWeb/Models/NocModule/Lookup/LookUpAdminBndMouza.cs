using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.NocModule.Lookup
{
    public class LookUpAdminBndMouza
    {
        [Key]
        [Column("MouzaGeoCode", Order = 0)]
        [MaxLength(12)]
        public string MouzaGeoCode { get; set; }

        [Required]
        [Column("UnionGeoCode", Order = 1)]
        [Display(Name = "Union")]
        [MaxLength(8)]
        public string UnionGeoCode { get; set; }
        [ForeignKey("UnionGeoCode")]
        public virtual LookUpAdminBndUnion LookUpAdminBndUnion { get; set; }

        [Required]
        [Column("MouzaName", Order = 2)]
        [Display(Name = "Mouza Name")]
        [MaxLength(50)]
        public string MouzaName { get; set; }

        [Required]
        [Column("MouzaNameBn", Order = 3)]
        [Display(Name = "Mouza Name (Bangla)")]
        [MaxLength(150)]
        public string MouzaNameBn { get; set; }
    }
}

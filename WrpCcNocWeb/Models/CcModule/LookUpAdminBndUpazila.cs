using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminBndUpazila
    {
        [Key]
        [Column("UpazilaGeoCode", Order = 0)]
        [MaxLength(6)]
        public string UpazilaGeoCode { get; set; }
        
        [Required]
        [Column("DistrictGeoCode", Order = 1)]
        [MaxLength(4)]
        [Display(Name = "District")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual LookUpAdminBndDistrict LookUpAdminBndDistrict { get; set; }

        [Required]
        [Column("UpazilaName", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Upazila Name")]
        public string UpazilaName { get; set; }

        [Required]
        [Column("UpazilaNameBn", Order = 3)]
        [MaxLength(100)]
        [Display(Name = "উপজেলা")]
        public string UpazilaNameBn { get; set; }
    }
}

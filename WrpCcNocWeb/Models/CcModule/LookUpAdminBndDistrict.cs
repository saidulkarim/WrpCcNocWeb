using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminBndDistrict
    {
        [Key]
        [Column("DistrictGeoCode", Order = 0)]
        [MaxLength(4)]
        public string DistrictGeoCode { get; set; }
        
        [Required]
        [Column("DistrictName", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjLocationDetail
    {
        [Key]
        [Column("LocationId", Order = 0)]
        public long LocationId { get; set; }
        
        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("DistrictGeoCode", Order = 2)]
        [MaxLength(4)]
        [Display(Name = "District")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual LookUpAdminBndDistrict LookUpAdminBndDistrict { get; set; }
        
        [Column("UpazilaGeoCode", Order = 3)]
        [MaxLength(6)]
        [Display(Name = "Upazila")]
        public string UpazilaGeoCode { get; set; }
        [ForeignKey("UpazilaGeoCode")]
        public virtual LookUpAdminBndUpazila LookUpAdminBndUpazila { get; set; }
        
        [Column("UnionGeoCode", Order = 4)]
        [MaxLength(8)]
        [Display(Name = "Union")]
        public string UnionGeoCode { get; set; }
        [ForeignKey("UnionGeoCode")]
        public virtual LookUpAdminBndUnion LookUpAdminBndUnion { get; set; }

        [Column("Latitude", Order = 5)]
        [MaxLength(50)]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Column("Longitude", Order = 6)]
        [MaxLength(8)]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }
    }
}
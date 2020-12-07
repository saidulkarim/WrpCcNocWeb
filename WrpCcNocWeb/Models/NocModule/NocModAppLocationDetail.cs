using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModAppLocationDetail
    {
        [Key]
        [Column("LocationId", Order = 0)]
        public long LocationId { get; set; }
        
        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual NocModAppCommonDetail NocModAppCommonDetail { get; set; }

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
		
		[Column("MouzaGeoCode", Order = 5)]
        [MaxLength(12)]
        [Display(Name = "Mouza")]
        public string MouzaGeoCode { get; set; }
        [ForeignKey("MouzaGeoCode")]
        public virtual LookUpAdminBndMouza LookUpAdminBndMouza { get; set; }
		
		[Column("Village", Order = 6)]
        [MaxLength(150)]
        [Display(Name = "Village")]
        public string Village { get; set; }

        [Column("Latitude", Order = 7)]
        [MaxLength(12)]
        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Column("Longitude", Order = 8)]
        [MaxLength(12)]
        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        [Column("LocationImage", Order = 9)]
        [MaxLength(50)]
        [Display(Name = "Location Photo")]
        public string LocationImage { get; set; }

        [Column("MapFileName", Order = 10)]
        [MaxLength(50)]
        [Display(Name = "Upload Project Map")]
        public string MapFileName { get; set; }
    }
}
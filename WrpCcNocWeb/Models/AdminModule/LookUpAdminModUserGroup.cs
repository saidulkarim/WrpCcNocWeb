using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.AdminModule;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminModUserGroup
    {
        [Key]
        [Column("UserGroupId", Order = 0)]
        public long UserGroupId { get; set; }

        [Required]
        [Column("UserGroupName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Name of Group")]
        public string UserGroupName { get; set; }

        [Column("AuthorityLevelId", Order = 2)]
        [Display(Name = "Authority Level")]
        public int? AuthorityLevelId { get; set; }
        [ForeignKey("AuthorityLevelId")]
        public virtual LookUpAdminModAuthorityLevel LookUpAdminModAuthorityLevel { get; set; }

        [Column("DistrictGeoCode", Order = 3)]
        [MaxLength(4)]
        [Display(Name = "District")]
        public string DistrictGeoCode { get; set; }
        [ForeignKey("DistrictGeoCode")]
        public virtual LookUpAdminBndDistrict LookUpAdminBndDistrict { get; set; }

        [Column("UpazilaGeoCode", Order = 4)]
        [Display(Name = "Upazila")]
        [MaxLength(6)]
        public string UpazilaGeoCode { get; set; }
        [ForeignKey("UpazilaGeoCode")]
        public virtual LookUpAdminBndUpazila LookUpAdminBndUpazila { get; set; }

        [Column("UnionGeoCode", Order = 5)]
        [Display(Name = "Union")]
        [MaxLength(6)]
        public string UnionGeoCode { get; set; }
        [ForeignKey("UnionGeoCode")]
        public virtual LookUpAdminBndUnion LookUpAdminBndUnion { get; set; }

        [Column("CanViewOneAsList", Order = 6)]
        public int? CanViewOneList { get; set; }

        [Column("CanViewMultipleAsList", Order = 7)]
        public int? CanViewMultipleList { get; set; }

        [Column("CanViewAsDetails", Order = 8)]
        public int? CanViewAsDetails { get; set; }

        [Column("CanInsertOne", Order = 9)]
        public int? CanInsertOne { get; set; }

        [Column("CanInsertMultiple", Order = 10)]
        public int? CanInsertMultiple { get; set; }

        [Column("CanUpdateOne", Order = 11)]
        public int? CanUpdateOne { get; set; }

        [Column("CanUpdateMultiple", Order = 12)]
        public int? CanUpdateMultiple { get; set; }

        [Column("CanDeleteOne", Order = 13)]
        public int? CanDeleteOne { get; set; }

        [Column("CanDeleteMultiple", Order = 14)]
        public int? CanDeleteMultiple { get; set; }
    }
}
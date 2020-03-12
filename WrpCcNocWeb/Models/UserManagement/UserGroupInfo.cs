using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.UserManagement
{
    public class UserGroupInfo
    {
        public long UserGroupId { get; set; }

        public string UserGroupName { get; set; }   
        
        public int? AuthorityLevelId { get; set; }

        public string AuthorityLevel { get; set; }

        public string DistrictGeoCode { get; set; }

        public string DistrictName { get; set; }

        public string UpazilaGeoCode { get; set; }

        public string UpazilaName { get; set; }

        public string UnionGeoCode { get; set; }

        public string UnionName { get; set; }

        public int? CanViewOneList { get; set; }
                
        public int? CanViewMultipleList { get; set; }

        public int? CanViewAsDetails { get; set; }

        public int? CanInsertOne { get; set; }

        public int? CanInsertMultiple { get; set; }

        public int? CanUpdateOne { get; set; }

        public int? CanUpdateMultiple { get; set; }

        public int? CanDeleteOne { get; set; }
        
        public int? CanDeleteMultiple { get; set; }


        List<LookUpAdminModUserGroup> lookUpAdminModUserGroups { get; set; }
        List<LookUpAdminModAuthorityLevel> lookUpAdminModAuthorityLevels { get; set; }
        List<LookUpAdminBndDistrict> lookUpAdminBndDistricts { get; set; }
        List<LookUpAdminBndUpazila> lookUpAdminBndUpazilas { get; set; }
        List<LookUpAdminBndUnion> lookUpAdminBndUnions { get; set; }        
    }
}
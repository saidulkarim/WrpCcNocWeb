using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.UserManagement
{
    [Serializable]
    public class UserInfo
    {
        public long UserID { get; set; }
        public long UserRegistrationID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserDesignation { get; set; }
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public int? UserActivationStatus { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? IsDeleted { get; set; }
    }

    [Serializable]
    public class UpdateUserInfo
    {
        public long UserID { get; set; }
        //public long UserRegistrationID { get; set; }
        //public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserFullNameBn { get; set; }
        public string UserFatherName { get; set; }
        public string UserNID { get; set; }
        public string UserPassportNo { get; set; }
        public string UserProfession { get; set; }
        public string UserDesignation { get; set; }
        public string UserAddress { get; set; }
        public string UserAddressBn { get; set; }
        //public string UserEmail { get; set; }
        public string UserAlternateEmail { get; set; }        
        //public string UserMobile { get; set; }
        public string UserAlternateMobile { get; set; }
                
        //public int SecurityQuestionId { get; set; }
        //public string SecurityQuestionAnswer { get; set; }
    }

    [Serializable]
    public class UserLevelInfo
    {
        public long UserID { get; set; }
        public long UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public int? AuthorityLevelId { get; set; }
        public int? ApplicationStateId { get; set; }
        public string DistrictGeoCode { get; set; }
        public string UpazilaGeoCode { get; set; }
        public string UnionGeoCode { get; set; }
    }
}
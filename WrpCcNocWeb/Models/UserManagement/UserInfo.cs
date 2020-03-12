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
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }       
        public string UserAddress { get; set; }        
        public string UserDesignation { get; set; }
        public int? UserActivationStatus { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
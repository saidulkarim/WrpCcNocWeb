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
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
    }
}

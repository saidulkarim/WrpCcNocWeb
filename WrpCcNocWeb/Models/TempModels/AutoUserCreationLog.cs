using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.TempModels
{
    public class AutoUserCreationLog
    {
        public long ProjectId { get; set; }
        public int? IWRMCMemberId { get; set; }
        public int? MemberTypeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MemberEmail { get; set; }
        public string MemberMobile { get; set; }
    }
}

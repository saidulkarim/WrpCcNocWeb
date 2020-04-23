using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.TempModels
{
    public class ApplicantInfo
    {
        public long ApplicantUserId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantNameBn { get; set; }
        public string ApplicantAddress { get; set; }
        public string ApplicantAddressBn { get; set; }
        public string ApplicantMobile { get; set; }
        public string ApplicantMobileBn { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantGroupName { get; set; }
    }
}

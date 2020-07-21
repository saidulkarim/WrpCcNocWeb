using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.TempModels
{
    public class CertificateVerificationInfo
    {
        public string TrackingNumber { get; set; }
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }

        public string AppliedDate { get; set; }
        public string ProjectType { get; set; }
        public string ProjectTitle { get; set; }
        public string ApplicationState { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalStage { get; set; }
        public string RejectReason { get; set; }
    }
}

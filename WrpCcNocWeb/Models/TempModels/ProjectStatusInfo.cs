using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.TempModels
{
    public class ProjectStatusInfo
    {
        public long ProjectId { get; set; }
        public long? AppSubmissionId { get; set; }
        public int ApplicationStateId { get; set; }
        public string ApplicationState { get; set; }
        public int? ApprovalStatusId { get; set; }
        public string ApprovalStatus { get; set; }
    }
}

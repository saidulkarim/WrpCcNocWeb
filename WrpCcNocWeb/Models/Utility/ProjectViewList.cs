using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.Utility
{
    public class ProjectViewList
    {
        public long ProjectId { get; set; }
        public int ProjectTypeId { get; set; }
        public string ProjectType { get; set; }
        public string ProjectName { get; set; }
        public double ProjectEstimatedCost { get; set; }
        public int? ApplicationStateId { get; set; }
        public string ApplicationState { get; set; }
        public int? ApprovalStatusId { get; set; }
        public string ApprovalStatus { get; set; }
        public int? IsCompleted { get; set; }
    }
}
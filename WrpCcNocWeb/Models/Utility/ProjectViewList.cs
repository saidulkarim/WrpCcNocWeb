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
        public string AppSubmissionDate { get; set; }
        public int? ApplicationStateId { get; set; }
        public string ApplicationState { get; set; }
        public int? ApprovalStatusId { get; set; }
        public string ApprovalStatus { get; set; }
        public int? IsCompletedId { get; set; }
        public string ReasonOfRejection { get; set; }
        public DateTime? AppApprovalDate { get; set; }
        public int? UndertakingSubmitYesNoId { get; set; }
    }

    public class ProjectQueryViewList
    {
        public long ProjectQueryId { get; set; }
        public long? ProjectId { get; set; }
        public long SenderUserId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderFullName { get; set; }
        public string SenderDesignation { get; set; }
        public string QuerySubject { get; set; }
        public string QueryBody { get; set; }
        public long ReceiverUserId { get; set; }
        public string ReceiverUserName { get; set; }
        public string ReceiverFullName { get; set; }
        public string ReceiverDesignation { get; set; }
        public int? QueryStateId { get; set; }
        public string QueryState { get; set; }
        public string SentOn { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.ReportModels
{
    public class CcRegistryBook
    {
        //public int Serial { get; set; }
        public long? AppSubmissionId { get; set; } //app tracking code
        public string ApplicantNameAddress { get; set; }
        public string ProjectType { get; set; }
        public string ProjectObjective { get; set; }
        public string ProjectDistrict { get; set; }
        public string ProjectUpazila { get; set; }
        public string MethodOfUsing { get; set; }
        public string IssuingDate { get; set; }
        public string ExpiringDate { get; set; }
        public string ClearanceTerms { get; set; }
    }
}
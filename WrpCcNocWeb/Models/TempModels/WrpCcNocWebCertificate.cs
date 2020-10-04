using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.TempModels
{
    public class WrpCcNocWebCertificate
    {
        public string ApplicantName { get; set; }
        public string ApplicantNameBn { get; set; }
        public string ApplicantAddress { get; set; }
        public string ApplicantAddressBn { get; set; }
        public string ApplicantMobile { get; set; }
        public string ApplicantMobileBn { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantGroupName { get; set; }
        public int LanguageId { get; set; }
        public long? ClearanceNo { get; set; }
        public string ClearanceNoBn { get; set; }
        public string ClearanceDate { get; set; }
        public string ClearanceDateBn { get; set; }
        public LookUpCcModProjectType ProjectType { get; set; }
        public string FormNo { get; set; }
        public string FormNoBn { get; set; }
        public LookUpCcModCertificateFormat CertificateFormat { get; set; }

        public string HigherAuthSignature { get; set; }
        public string HigherAuthSeal { get; set; }
    }

    public class WrpCcNocUndertaking
    {
        public long ProjectId { get; set; }
        public int ProjectTypeId { get; set; }
        public int LanguageId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantNameBn { get; set; }
        public string ApplicantAddress { get; set; }
        public string ApplicantAddressBn { get; set; }
        public string LocationName { get; set; }
        public string LocationNameBn { get; set; }
        public string UndertakingDate { get; set; } //dd MMM, yyyy
        public string UndertakingDateBn { get; set; } //dd MMM, yyyy
        public string UndertakingTime { get; set; } // HH:mm:ss
        public string UndertakingTimeBn { get; set; } // HH:mm:ss
        public LookUpCcModUndertakingFormat UndertakingFormat { get; set; }        
        public string ApplicantSignatureFile { get; set; }

        public int? IsUndertakingSubmitted { get; set; }
        public DateTime? UndertakingSubmitDate { get; set; }
    }
}

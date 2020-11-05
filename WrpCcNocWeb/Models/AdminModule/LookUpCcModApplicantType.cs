using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModApplicantType
    {
        [Key]
        [Column("ApplicantTypeId", Order = 0)]
        public int ApplicantTypeId { get; set; }

        [Required]
        [Column("ApplicantType", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Applicant Type")]
        public string ApplicantType { get; set; }

        [Column("ApplicantTypeBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Applicant Type (bn)")]
        public string ApplicantTypeBn { get; set; }
    }
}
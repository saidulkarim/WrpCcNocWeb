using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjEcoFinAnalysisDetail
    {

        [Key]
        [Column("EconomicalAndFinancialId", Order = 0)]
        public long EconomicalAndFinancialId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("EcoAndFinancialParamId", Order = 2)]
        [Display(Name = "Eco And Financial Parameter")]
        public int EcoAndFinancialParamId { get; set; }
        [ForeignKey("EcoAndFinancialParamId")]
        public virtual LookUpCcModEcoAndFinancial LookUpEcoAndFinancial { get; set; }


        [Column("EcoAndFinancialParamUnit", Order = 3)]
        [MaxLength(50)]
        [Display(Name = "Unit")]
        public string EcoAndFinancialParamUnit { get; set; }
                

        [Column("EcoAndFinancialApplicantCmt", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "Applicant Comments")]
        public string EcoAndFinancialApplicantCmt { get; set; }


        [Column("EcoAndFinancialAuthorityCmt", Order = 5)]
        [MaxLength(150)]
        [Display(Name = "Authority Comments")]
        public string EcoAndFinancialAuthorityCmt { get; set; }

    }
}

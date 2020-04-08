using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModYesNo
    {
        [Key]
        [Column("YesNoId", Order = 0)]        
        public int YesNoId { get; set; }


        [Required]
        [Column("YesNoTitle", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Title")]
        public string YesNoTitle { get; set; }


        [Column("YesNoTitleBn", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Title")]
        public string YesNoTitleBn { get; set; }


        [InverseProperty("LookUpCcModYesNoStake")]
        public virtual List<CcModAppProjectCommonDetail> LookUpCcModYesNoStake { get; set; }


        [InverseProperty("LookUpYesNoAnalyze")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoAnalyze { get; set; }


        [InverseProperty("LookUpYesNoNWPCompat")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoNWPCompat { get; set; }


        [InverseProperty("LookUpYesNoNWMPCompat")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoNWMPCompat { get; set; }


        [InverseProperty("LookUpYesNoFYP")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoFYP { get; set; }


        [InverseProperty("LookUpYesNoSDG")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoSDG { get; set; }


        [InverseProperty("LookUpYesNoDeltaPlan")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoDeltaPlan { get; set; }


        [InverseProperty("LookUpYesNoCostalZone")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoCostalZone { get; set; }


        [InverseProperty("LookUpYesNoAgricultural")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoAgricultural { get; set; }


        [InverseProperty("LookUpYesNoFishery")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoFishery { get; set; }        
        

        [InverseProperty("LookUpYesNoIWRM")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoIWRM { get; set; }


        [InverseProperty("LookUpYesNoGPWM")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoGPWM { get; set; }


        [InverseProperty("LookUpYesNoFeasibility")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoFeasibility { get; set; }


        [InverseProperty("LookUpYesNoSocialIssue")]
        public virtual List<CcModAppProjectCommonDetail> LookUpYesNoSocialIssue { get; set; }
        
        
        [InverseProperty("LookUpYesNoUseOfTool")]
        public virtual List<CcModAppProject_31_IndvDetail> LookUpYesNoUseOfTool { get; set; }

        
        [InverseProperty("LookUpYesNoDuplication")]
        public virtual List<CcModAppProject_31_IndvDetail> LookUpYesNoDuplication { get; set; }
    }
}

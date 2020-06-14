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


        [InverseProperty("LookUpYesNoUseOfTool_31")]
        public virtual List<CcModAppProject_31_IndvDetail> LookUpYesNoUseOfTool_31 { get; set; }


        [InverseProperty("LookUpYesNoUseOfTool_32")]
        public virtual List<CcModAppProject_32_IndvDetail> LookUpYesNoUseOfTool_32 { get; set; }


        [InverseProperty("LookUpYesNoUseOfTool_33")]
        public virtual List<CcModAppProject_33_IndvDetail> LookUpYesNoUseOfTool_33 { get; set; }


        [InverseProperty("LookUpYesNoDuplication_31")]
        public virtual List<CcModAppProject_31_IndvDetail> LookUpYesNoDuplication_31 { get; set; }


        [InverseProperty("LookUpYesNoDuplication_32")]
        public virtual List<CcModAppProject_32_IndvDetail> LookUpYesNoDuplication_32 { get; set; }


        [InverseProperty("LookUpYesNoDuplication_33")]
        public virtual List<CcModAppProject_33_IndvDetail> LookUpYesNoDuplication_33 { get; set; }


        [InverseProperty("LookUpYesNoDuplication_34")]
        public virtual List<CcModAppProject_34_IndvDetail> LookUpYesNoDuplication_34 { get; set; }


        [InverseProperty("LookUpYesNoDuplication_35")]
        public virtual List<CcModAppProject_35_IndvDetail> LookUpYesNoDuplication_35 { get; set; }


        [InverseProperty("LookUpYesNoDuplication_36")]
        public virtual List<CcModAppProject_36_IndvDetail> LookUpYesNoDuplication_36 { get; set; }


        [InverseProperty("LookUpCcModYesNoUseOfFlowMeter_32")]
        public virtual List<CcModAppProject_32_IndvDetail> LookUpCcModYesNoUseOfFlowMeter_32 { get; set; }


        [InverseProperty("LookUpCcModYesNoUseOfFlowMeter_33")]
        public virtual List<CcModAppProject_33_IndvDetail> LookUpCcModYesNoUseOfFlowMeter_33 { get; set; }


        [InverseProperty("LookUpCcModYesNoDivertWtrRtnSrc_32")]
        public virtual List<CcModAppProject_32_IndvDetail> LookUpCcModYesNoDivertWtrRtnSrc_32 { get; set; }


        [InverseProperty("LookUpCcModYesNoDivertWtrRtnSrc_33")]
        public virtual List<CcModAppProject_33_IndvDetail> LookUpCcModYesNoDivertWtrRtnSrc_33 { get; set; }


        [InverseProperty("LookUpYesNoLandUseMap_36")]
        public virtual List<CcModAppProject_36_IndvDetail> LookUpYesNoLandUseMap_36 { get; set; }


        [InverseProperty("LookUpYesNoLandUseDesign_36")]
        public virtual List<CcModAppProject_36_IndvDetail> LookUpYesNoLandUseDesign_36 { get; set; }


        [InverseProperty("LookUpYesNoImpctFldPlnAra_36")]
        public virtual List<CcModAppProject_36_IndvDetail> LookUpYesNoImpctFldPlnAra_36 { get; set; }



    }
}

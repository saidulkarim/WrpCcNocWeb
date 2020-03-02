using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjCompatNWPDetail
    {
        [Key]
        [Column("PrjCompatNWPId", Order = 0)]
        public long PrjCompatNWPId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("NationalWaterPolicyArticleId", Order = 2)]
        [Display(Name = "National Water Policy Article")]
        public int NationalWaterPolicyArticleId { get; set; }
        [ForeignKey("NationalWaterPolicyArticleId")]
        public virtual LookUpCcModNWPArticle LookUpCcModNWPArticle { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WrpCcNocWeb.Models.NocModule.Lookup;

namespace WrpCcNocWeb.Models.NocModule
{
    public class NocModAppCompatNWPDetail
    {
        [Key]
        [Column("PrjCompatNWPId", Order = 0)]
        public long PrjCompatNWPId { get; set; }

        [Required]
        [Column("NocApplicationId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long NocApplicationId { get; set; }
        [ForeignKey("NocApplicationId")]
        public virtual NocModAppCommonDetail NocModAppCommonDetail { get; set; }

        [Required]
        [Column("NationalWaterPolicyArticleId", Order = 2)]
        [Display(Name = "National Water Policy Article")]
        public int NationalWaterPolicyArticleId { get; set; }
        [ForeignKey("NationalWaterPolicyArticleId")]
        public virtual LookUpCcModNWPArticle LookUpCcModNWPArticle { get; set; }
    }
}
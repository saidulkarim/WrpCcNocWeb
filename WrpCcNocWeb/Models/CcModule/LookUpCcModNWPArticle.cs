using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModNWPArticle
    {
        [Key]
        [Column("NationalWaterPolicyArticleId", Order = 0)]        
        public int NationalWaterPolicyArticleId { get; set; }
                
        [Column("NationalWtrPolicyShortTitle", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "National Water Policy Short Title")]
        public string NationalWtrPolicyShortTitle { get; set; }
                   
        [Column("NationalWtrPolicyArticleTitle", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "National Water Policy Article Title")]
        public string NationalWtrPolicyArticleTitle { get; set; }

        [Column("NWPArticleTitleBn", Order = 3)]
        [MaxLength(50)]
        [Display(Name = "National Water Policy Short Title")]
        public string NWPArticleTitleBn { get; set; }

        [Column("NWPArticleShortTitleBn", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "National Water Policy Article Title")]
        public string NWPArticleShortTitleBn { get; set; }

        [Column("NWPArticleLink", Order = 5)]
        [MaxLength(250)]
        [Display(Name = "NWP Article Link")]
        public string NWPArticleLink { get; set; }
    }
}
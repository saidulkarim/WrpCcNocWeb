using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModRecommendation
    {        
        [Key]
        [Column("RecommendationId", Order = 0)]
        public int RecommendationId { get; set; }


        [Required]
        [Column("RecommendationTitle", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Title")]
        public string RecommendationTitle { get; set; }
    }
}
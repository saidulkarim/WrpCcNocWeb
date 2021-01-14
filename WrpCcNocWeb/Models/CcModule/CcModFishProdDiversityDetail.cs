using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModFishProdDiversityDetail
    {
        [Key]
        [Column("FishProdDiversityDetailId", Order = 0)]
        public long FishProdDiversityDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("TypesOfFisheries", Order = 2)]
        [Display(Name = "Types of Fisheries")]
        [MaxLength(10)]
        public string TypesOfFisheries { get; set; } //Capture, Culture

        [Required]
        [Column("Diversity", Order = 3)]
        [Display(Name = "Diversity")]
        [MaxLength(100)]
        public string Diversity { get; set; }

        [Column("FishProductionInTon", Order = 4)]
        [Display(Name = "Production (Ton)")]
        public double? FishProductionInTon { get; set; }
    }
}
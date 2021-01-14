using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModPrjIrrigCropAreaDetail
    {
        [Key]
        [Column("IrrigatedCropId", Order = 0)]
        public long IrrigatedCropId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Name of Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("CropName", Order = 2)]
        [MaxLength(50)]
        [Display(Name = "Crop Name")]
        public string CropName { get; set; }

        [Column("Area", Order = 3)]
        [Display(Name = "Area")]
        public decimal? Area { get; set; }

        [Column("ProductionInTon", Order = 4)]
        [Display(Name = "Production (Ton)")]
        public decimal? ProductionInTon { get; set; }

        [Column("ProductionAmount", Order = 5)]
        [Display(Name = "Production Amount")]
        public decimal? ProductionAmount { get; set; }
    }
}

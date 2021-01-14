using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModHydroSystemDetail
    {
        [Key]
        [Column("HydroSysDetailId", Order = 0)]
        public long HydroSysDetailId { get; set; }
        
        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Name")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }
        
        [Required]
        [Column("HydroSystemCategoryId", Order = 2)]
        [Display(Name = "Hydrological System Category")]
        public int HydroSystemCategoryId { get; set; }
        [ForeignKey("HydroSystemCategoryId")]
        public virtual LookUpCcModHydroSystem LookUpCcModHydroSystem { get; set; }

        [Required]
        [Column("NameOfHydroSystem", Order = 3)]
        [MaxLength(150)]
        [Display(Name = "Hydro System Name")]
        public string NameOfHydroSystem { get; set; }

        [Required]
        [Column("HydroSystemLengthArea", Order = 4)]        
        [Display(Name = "Length or Area")]
        public double HydroSystemLengthArea { get; set; }
        
        [Required]
        [Column("HydroSystemUnit", Order = 5)]
        [MaxLength(50)]
        [Display(Name = "Unit")]
        public string HydroSystemUnit { get; set; }

        [Column("Description", Order = 6)]
        [MaxLength(250)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
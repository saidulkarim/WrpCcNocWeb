using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModHydroSystem
    {
        [Key]
        [Column("HydroSystemCategoryId", Order = 0)]        
        public int HydroSystemCategoryId { get; set; }


        [Required]
        [Column("HydroSystemCategory", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Hydro System Category")]
        public string HydroSystemCategory { get; set; }
           
        
        [Column("HydroSysCategoryUnit", Order = 2)]
        [MaxLength(20)]
        [Display(Name = "Hydro System Unit")]
        public string HydroSysCategoryUnit { get; set; }
    }
}

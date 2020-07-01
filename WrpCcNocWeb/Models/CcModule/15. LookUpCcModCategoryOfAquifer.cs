using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModCategoryOfAquifer //Categorization of Aquifer
    {
        [Key]
        [Column("CategoryOfAquiferId", Order = 0)]
        public int CategoryOfAquiferId { get; set; }
        
        [Required]
        [Column("AquiferName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Aquifer Name")]
        public string AquiferName { get; set; }
                
        [Column("AquiferNameBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Aquifer Name")]
        public string AquiferNameBn { get; set; }
    }
}
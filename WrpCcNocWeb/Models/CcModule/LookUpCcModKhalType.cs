using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModKhalType
    {
        [Key]
        [Column("KhalTypeId", Order = 0)]        
        public int KhalTypeId { get; set; }


        [Required]
        [Column("KhalTypeName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Khal Type Name")]
        public string KhalTypeName { get; set; }
    }
}

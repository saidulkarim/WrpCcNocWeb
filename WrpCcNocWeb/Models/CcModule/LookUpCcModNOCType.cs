using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WrpCcNocWeb.Models
{
    public class LookUpCcModNOCType
    {
        [Key]
        [Column("NOCTypeId", Order = 0)]        
        public int NOCTypeId { get; set; }


        [Required]
        [Column("NOCType", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "NOC Type")]
        public string NOCType { get; set; }                       
    }
}

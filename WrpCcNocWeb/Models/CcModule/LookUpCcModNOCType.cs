using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WrpCcNocWeb.Models
{
    public class LookUpCcModNocType
    {
        [Key]
        [Column("NocTypeId", Order = 0)]        
        public int NocTypeId { get; set; }


        [Required]
        [Column("NocType", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "NOC Type")]
        public string NocType { get; set; }


        [Column("NocTypeBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "NOC Type")]
        public string NocTypeBn { get; set; }
    }
}

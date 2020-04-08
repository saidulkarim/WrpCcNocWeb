using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModProjectType
    {
        [Key]
        [Column("ProjectTypeId", Order = 0)]        
        public int ProjectTypeId { get; set; }


        [Required]
        [Column("ProjectType", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Project Type")]
        public string ProjectType { get; set; }


        [Column("ProjectTypeBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Project Type")]
        public string ProjectTypeBn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminModUserGroup
    {
        [Key]
        [Column("UserGroupId", Order = 0)]
        public int UserGroupId { get; set; }
        
        [Required]
        [Column("UserGroup", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Name of Group")]
        public string UserGroup { get; set; }
    }
}
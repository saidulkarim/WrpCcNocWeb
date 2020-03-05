using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class AdminModUserGrpWiseMenuDetail
    {
        [Key]        
        [Column("UserGrpWiseMenuId", Order = 0)]        
        public int UserGrpWiseMenuId { get; set; }


        [Required]
        [Column("MenuId", Order = 1)]
        [Display(Name = "Menu")]
        public int MenuId { get; set; }
        [ForeignKey("MenuId")]
        public virtual LookUpAdminModMenu LookUpAdminModMenu { get; set; }

        
        [Column("SubMenuId", Order = 2)]        
        [Display(Name = "Sub Menu")]
        public int? SubMenuId { get; set; }
        [ForeignKey("SubMenuId")]
        public virtual LookUpAdminModSubMenu LookUpAdminModSubMenu { get; set; }


        [Required]
        [Column("UserGroupId", Order = 3)]
        [Display(Name = "User Group")]
        public int UserGroupId { get; set; }
        [ForeignKey("UserGroupId")]
        public virtual LookUpAdminModUserGroup LookUpAdminModUserGroup { get; set; }
    }
}

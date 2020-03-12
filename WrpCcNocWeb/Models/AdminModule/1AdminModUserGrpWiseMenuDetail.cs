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
        
        
        [Column("UserGroupId", Order = 3)]
        [Display(Name = "User Group")]
        public long UserGroupId { get; set; }
        [ForeignKey("UserGroupId")]
        public virtual LookUpAdminModUserGroup LookUpAdminModUserGroup { get; set; }


        [Column("AuthorityLevelId", Order = 4)]
        [Display(Name = "Authority Level")]
        public int? AuthorityLevelId { get; set; }
        [ForeignKey("AuthorityLevelId")]
        public virtual LookUpAdminModAuthorityLevel LookUpAdminAuthorityLevel { get; set; }
    }
}
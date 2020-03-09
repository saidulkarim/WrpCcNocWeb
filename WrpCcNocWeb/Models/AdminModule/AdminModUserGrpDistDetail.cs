using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class AdminModUserGrpDistDetail
    {

        [Key]        
        [Column("UserGrpDistId", Order = 0)]           
        public long UserGrpDistId { get; set; }


        [Required]
        [Column("UserId", Order = 1)]        
        [Display(Name = "User Name")]        
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AdminModUsersDetail adminModUsersDetails { get; set; }


        [Required]
        [Column("UserGroupId", Order = 2)]        
        [Display(Name = "User Group")]
        public long UserGroupId { get; set; }
        [ForeignKey("UserGroupId")]
        public virtual LookUpAdminModUserGroup lookUpAdminModUserGroups { get; set; }

    }
}

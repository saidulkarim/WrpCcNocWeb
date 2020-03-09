using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.UserManagement
{
    public class UserMenu
    {
        public long UserId { get; set; }
        public long UserGroupId { get; set; }
        public int MenuId { get; set; }

        [MaxLength(50)]
        public string MenuTitle { get; set; } //50 chras
        public int? SubMenuId { get; set; }

        [MaxLength(50)]
        public string SubMenuTitle { get; set; } //50 chras

        [MaxLength(100)]
        public string Controller { get; set; } //100 chras

        [MaxLength(100)]
        public string Action { get; set; } //100 chras
    }
}

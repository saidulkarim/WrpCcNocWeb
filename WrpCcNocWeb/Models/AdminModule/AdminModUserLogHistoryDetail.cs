using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class AdminModUserLogHistoryDetail
    {
        [Key]        
        [Column("UserLogHistoryId", Order = 0)]        
        public long UserLogHistoryId { get; set; }


        [Required]
        [Column("UserId", Order = 1)]        
        [Display(Name = "User Name")]
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AdminModUsersDetail adminModUserDetails { get; set; }


        [Display(Name = "Date")]
        [Column("LoginDateTime", Order = 2)]       
        public DateTime LoginDateTime { get; set; }
        
                
        [Column("MachineIpOrUrl", Order = 3)] 
        [MaxLength(150)]
        [Display(Name = "Machine IP Or URL")]
        public string MachineIPOrUrl { get; set; }
    }
}

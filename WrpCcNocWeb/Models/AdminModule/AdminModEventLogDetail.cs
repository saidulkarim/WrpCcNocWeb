using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class AdminModEventLogDetail
    {
        [Key]        
        [Column("EventLogId", Order = 0)] 
        public long EventLogId { get; set; }
        
        [Required]
        [Column("UserId", Order = 1)]        
        [Display(Name = "Users Name")]
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AdminModUsersDetail adminModUsersDetails { get; set; }
        
        [Required]        
        [Column("EventTitle", Order = 2)]   
        [MaxLength(100)]
        [Display(Name = "Event Title")]
        public string EventTitle { get; set; }
                 
        [Column("EventDescription", Order = 3)]
        [MaxLength(500)]
        [Display(Name = "Event Description")]
        public string EventDescription { get; set; }

        [Column("EventOccuredTime", Order = 4)]
        [Display(Name = "Event Occured Time")]        
        public DateTime EventOccuredTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models.AdminModule
{
    public class LookUpAdminModSmsHistory
    {
        [Key]
        [Column("SmsHistoryId", Order = 0)]
        public long SmsHistoryId { get; set; }
				
		[Column("SmsFormatId", Order = 1)]
        [Display(Name = "Sms Format")]
        public int SmsFormatId { get; set; }
        [ForeignKey("SmsFormatId")]
        public virtual LookUpAdminModSmsFormat LookUpAdminModSmsFormat { get; set; }
		
		[Required]
        [Column("ReceiverUserId", Order = 2)]        
        [Display(Name = "Receiver")]
        public int ReceiverUserId { get; set; }
		[ForeignKey("UserId")]
        public virtual AdminModUsersDetail AdminModUsersDetail { get; set; }
		
		[Required]
        [Column("UserMobile", Order = 3)]        
        [Display(Name = "Receiver Mobile")]
		[MaxLength(150)]
        public string UserMobile { get; set; }
		        
        [Required]
        [Column("SmsBody", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "Message")]
        public string SmsBody { get; set; }
		
        [Column("SmsSentOn", Order = 5)]
        [Display(Name = "Sent On")]
        public DateTime SmsSentOn { get; set; }
    }
}
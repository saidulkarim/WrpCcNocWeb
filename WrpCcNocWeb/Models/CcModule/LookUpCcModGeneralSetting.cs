using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModGeneralSetting
    {
        [Key]
        [Column("GeneralSettingId", Order = 0)]
        public int GeneralSettingId { get; set; }
        
        //Call Center Support
        [Column("CallCenterNumber", Order = 1)]
        [MaxLength(20)]
        [Display(Name = "Call Center Number")]
        public string CallCenterNumber { get; set; }
                
        [Column("TnTNumber", Order = 2)]
        [MaxLength(20)]
        [Display(Name = "T&T Number")]
        public string TnTNumber { get; set; }
		
		[Column("MobileNumber", Order = 3)]
        [MaxLength(20)]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
		
		//Email Sender Part
		[Column("MailSendFrom", Order = 4)]
        [MaxLength(50)]
        [Display(Name = "Mail Send From")]
        public string MailSendFrom { get; set; }
		
		[Column("SmtpServer", Order = 5)]
        [MaxLength(50)]
        [Display(Name = "SMTP Server")]
        public string SmtpServer { get; set; }
		
		[Column("SmtpOutGoingPort", Order = 6)]
        [MaxLength(10)]
        [Display(Name = "SMTP Out Going Port")]
        public string SmtpOutGoingPort { get; set; }
		
		[Column("MailSenderUserName", Order = 7)]
        [MaxLength(50)]
        [Display(Name = "Mail Sender User Name")]
        public string MailSenderUserName { get; set; }
		
		[Column("MailSenderPassword", Order = 8)]
        [MaxLength(50)]
        [Display(Name = "Mail Sender Password")]
        public string MailSenderPassword { get; set; }

        [Required]
        [Column("IsEmailServiceActive", Order = 9)]        
        [Display(Name = "Is Email Service Active?")]
        public int IsEmailServiceActive { get; set; }

        //SMS
        [Required]
        [Column("SmsApiUrl", Order = 10)]
        [MaxLength(250)]
        [Display(Name = "Name of SMS API URL")]
        public string SmsApiUrl { get; set; }

        [Required]
        [Column("IsSmsApiActive", Order = 11)]        
        [Display(Name = "Is SMS API Active?")]
        public int IsSmsApiActive { get; set; }
    }
}
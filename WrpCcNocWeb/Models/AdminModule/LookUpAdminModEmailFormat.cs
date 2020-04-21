using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models.AdminModule
{
    public class LookUpAdminModEmailFormat
    {
        [Key]
        [Column("EmailFormatId", Order = 0)]
        public int EmailFormatId { get; set; }

        [Required]
        [Column("EmailRecipientType", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Recipient Type")]
        public string EmailRecipientType { get; set; }

        [Column("UserGroupId", Order = 2)]
        [Display(Name = "User Group")]
        public long? UserGroupId { get; set; }
        [ForeignKey("UserGroupId")]
        public virtual LookUpAdminModUserGroup LookUpAdminModUserGroup { get; set; }

        [Column("AuthorityLevelId", Order = 3)]
        [Display(Name = "Authority Level")]
        public int? AuthorityLevelId { get; set; }
        [ForeignKey("AuthorityLevelId")]
        public virtual LookUpAdminModAuthorityLevel LookUpAdminAuthorityLevel { get; set; }

        [Column("ApplicationStateId", Order = 4)]
        public int? ApplicationStateId { get; set; }
        [ForeignKey("ApplicationStateId")]
        public virtual LookUpCcModApplicationState LookUpCcModApplicationState { get; set; }

        [Column("IsCompleted", Order = 5)]
        public int? IsCompleted { get; set; }

        [Required]
        [Column("EmailSubject", Order = 6)]
        [MaxLength(150)]
        [Display(Name = "Subject")]
        public string EmailSubject { get; set; }

        [Required]
        [Column("EmailSalutation", Order = 7)]
        [MaxLength(75)]
        public string EmailSalutation { get; set; }

        [Required]
        [Column("EmailBody1stPara", Order = 8)]
        [MaxLength(550)]
        public string EmailBody1stPara { get; set; }

        [Column("EmailBody2ndPara", Order = 9)]
        [MaxLength(450)]
        public string EmailBody2ndPara { get; set; }

        [Column("EmailBody3rdPara", Order = 10)]
        [MaxLength(350)]
        public string EmailBody3rdPara { get; set; }

        [Required]
        [Column("EmailBodyClosing1stLine", Order = 11)]
        [MaxLength(150)]
        public string EmailBodyClosing1stLine { get; set; }

        [Column("EmailBodyClosing2ndLine", Order = 12)]
        [MaxLength(150)]
        public string EmailBodyClosing2ndLine { get; set; }

        [Required]
        [Column("ValedictoryGreetings", Order = 13)]
        [MaxLength(100)]
        public string ValedictoryGreetings { get; set; }

        [Required]
        [Column("EmailSenderInfo1stLine", Order = 14)]
        [MaxLength(150)]
        public string EmailSenderInfo1stLine { get; set; }

        [Column("EmailSenderInfo2ndLine", Order = 15)]
        [MaxLength(150)]
        public string EmailSenderInfo2ndLine { get; set; }

        [Column("EmailSenderInfo3rdLine", Order = 16)]
        [MaxLength(150)]
        public string EmailSenderInfo3rdLine { get; set; }

        [Column("EmailSenderInfo4thLine", Order = 17)]
        [MaxLength(150)]
        public string EmailSenderInfo4thLine { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models.AdminModule
{
    public class LookUpAdminModSmsFormat
    {
        [Key]
        [Column("SmsFormatId", Order = 0)]
        public int SmsFormatId { get; set; }

        [Required]
        [Column("SmsRecipientType", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "Recipient Type")]
        public string SmsRecipientType { get; set; }

        [Column("UserGroupId", Order = 2)]
        [Display(Name = "User Group")]
        public long? UserGroupId { get; set; }
        [ForeignKey("UserGroupId")]
        public virtual LookUpAdminModUserGroup LookUpAdminModUserGroup { get; set; }

        [Column("AuthorityLevelId", Order = 3)]
        [Display(Name = "Authority Level")]
        public int? AuthorityLevelId { get; set; }
        [ForeignKey("AuthorityLevelId")]
        public virtual LookUpAdminModAuthorityLevel LookUpAdminModAuthorityLevel { get; set; }

        [Column("ApplicationStateId", Order = 4)]
        public int? ApplicationStateId { get; set; }
        [ForeignKey("ApplicationStateId")]
        public virtual LookUpCcModApplicationState LookUpCcModApplicationState { get; set; }

        [Column("IsCompleted", Order = 5)]
        public int? IsCompleted { get; set; }

        [Required]
        [Column("SmsBody", Order = 6)]
        [MaxLength(150)]
        [Display(Name = "Message")]
        public string SmsBody { get; set; }       
    }
}
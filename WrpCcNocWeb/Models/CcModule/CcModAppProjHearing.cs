using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProjHearing
    {
        [Key]
        [Column("AppProjHearingId", Order = 0)]
        public long AppProjHearingId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("SenderUserId", Order = 2)]
        [Display(Name = "Sender User")]
        public long SenderUserId { get; set; }
        [ForeignKey("SenderUserId")]
        public virtual AdminModUsersDetail AdminModUsersDetail { get; set; }

        [Required]
        [Column("HearingSubject", Order = 3)]
        [Display(Name = "Hearing Subject")]
        [MaxLength(150)]
        public string HearingSubject { get; set; }

        [Required]
        [Column("HearingReason", Order = 4)]
        [Display(Name = "Hearing Reason")]
        [MaxLength(500)]
        public string HearingReason { get; set; }

        [Required]
        [Column("HearingPlace", Order = 5)]
        [Display(Name = "Hearing Place")]
        [MaxLength(150)]
        public string HearingPlace { get; set; }

        [Required]
        [Column("DateOfHearing", Order = 6)]
        [Display(Name = "Date of Hearing")]
        [MaxLength(10)] //10-12-2020
        public string DateOfHearing { get; set; }

        [Required]
        [Column("TimeOfHearing", Order = 7)]
        [Display(Name = "Time of Hearing")]
        [MaxLength(10)] //15:00:00
        public string TimeOfHearing { get; set; }

        [Column("HearingCycleNo", Order = 8)]
        [Display(Name = "Hearing Cycle No.")]
        public int HearingCycleNo { get; set; }
    }
}
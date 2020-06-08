using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.CcModule
{
    public class LookUpCcModIsCompletedState
    {
        [Key]
        [Column("IsCompletedId", Order = 0)]
        public int IsCompletedId { get; set; }

        [Required]
        [Column("IsCompletedState", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Completion Status")]
        public string IsCompletedState { get; set; }

        [Required]
        [Column("IsCompletedStateBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Completion Status")]
        public string IsCompletedStateBn { get; set; }

        [Column("Note", Order = 3)]
        [MaxLength(150)]
        [Display(Name = "Note")]
        public string Note { get; set; }

        [Column("NoteBn", Order = 4)]
        [MaxLength(150)]
        [Display(Name = "Note")]
        public string NoteBn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModNavigationClassDetail //Navigation Class Detail
    {
        [Key]
        [Column("NavigationClassDetailId", Order = 0)]
        public long NavigationClassDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("NavigationClassId", Order = 2)]
        [Display(Name = "Navigation Class")]
        public int NavigationClassId { get; set; }
        [ForeignKey("NavigationClassId")]
        public virtual LookUpCcModNavigationClass LookUpCcModNavigationClass { get; set; }
    }
}
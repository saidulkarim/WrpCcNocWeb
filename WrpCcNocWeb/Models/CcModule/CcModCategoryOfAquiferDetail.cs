using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModCategoryOfAquiferDetail
    {
        [Key]
        [Column("CategoryOfAquiferDetailId", Order = 0)]
        public long CategoryOfAquiferDetailId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("CategoryOfAquiferId", Order = 2)]
        [Display(Name = "Category of Aquifer")]
        public int CategoryOfAquiferId { get; set; }
        [ForeignKey("CategoryOfAquiferId")]
        public virtual LookUpCcModCategoryOfAquifer LookUpCcModCategoryOfAquifer { get; set; }		
    }
}
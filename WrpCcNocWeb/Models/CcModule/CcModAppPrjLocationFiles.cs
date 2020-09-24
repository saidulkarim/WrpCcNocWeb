using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppPrjLocationFiles
    {
        [Key]
        [Column("PrjLocationFileId", Order = 0)]
        public long PrjLocationFileId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Name")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }
		
		[Required]
        [Column("LocationId", Order = 2)]
        [Display(Name = "Project Location")]
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual CcModPrjLocationDetail CcModPrjLocationDetail { get; set; }

        [Required]
        [Column("AdditionalAttachmentFile", Order = 3)]
        [MaxLength(150)]
        [Display(Name = "Additional Attachment File")]
        public string AdditionalAttachmentFile { get; set; }
        
        [Required]
        [Column("AttachmentTitle", Order = 4)]
        [MaxLength(100)]
        [Display(Name = "Attachment Title")]
        public string AttachmentTitle { get; set; }
    }
}

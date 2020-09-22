using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class CcModAppAdditionalAttachment
    {
        [Key]
        [Column("AttachmentId", Order = 0)]
        public long AttachmentId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project Name")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }

        [Required]
        [Column("AditionalAttachmentFile", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Aditional Attachment File")]
        public string AditionalAttachmentFile { get; set; }
        
        [Required]
        [Column("AttachmentTitle", Order = 3)]
        [MaxLength(100)]
        [Display(Name = "Attachment Title")]
        public string AttachmentTitle { get; set; }
    }
}

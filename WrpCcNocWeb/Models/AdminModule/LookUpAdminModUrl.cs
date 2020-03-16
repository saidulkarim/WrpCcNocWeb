using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.AdminModule
{
    public class LookUpAdminModUrl
    {
        [Key]
        [Column("UrlId", Order = 0)]
        public int UrlId { get; set; }


        [Required]
        [Column("UrlTypeId", Order = 1)]
        public int UrlTypeId { get; set; }
        [ForeignKey("UrlTypeId")]
        public virtual LookUpAdminModUrlType LookUpAdminModUrlType { get; set; }


        [Required]
        [Column("Url", Order = 2)]
        [MaxLength(250)]        
        public string ApiUrl { get; set; }


        [Display(Name = "Activation Status")]
        [Column("ApiUrlActivationStatus", Order = 3)]
        public int? ApiUrlActivationStatus { get; set; }
    }
}

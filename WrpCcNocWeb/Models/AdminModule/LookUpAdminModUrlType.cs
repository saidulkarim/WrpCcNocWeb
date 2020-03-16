using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.AdminModule
{
    public class LookUpAdminModUrlType
    {
        [Key]
        [Column("UrlTypeId", Order = 0)]
        public int UrlTypeId { get; set; }

        [Required]
        [Column("UrlType", Order = 1)]
        [MaxLength(100)]
        public string UrlType { get; set; }

        [Column("UrlTitle", Order = 2)]
        [MaxLength(150)]
        public string UrlTitle { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminModAuthorityLevel
    {
        [Key]
        [Column("AuthorityLevelId", Order = 0)]
        public int AuthorityLevelId { get; set; }

        [Required]
        [Column("AuthorityLevel", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Authority Level")]
        public string AuthorityLevel { get; set; }

        [Column("AuthorityLevelBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Authority Level")]
        public string AuthorityLevelBn { get; set; }
    }
}
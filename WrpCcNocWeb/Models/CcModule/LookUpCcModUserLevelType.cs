using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModUserLevelType
    {
        [Key]
        [Column("UserLevelTypeId", Order = 0)]
        public int UserLevelTypeId { get; set; }

        [Required]
        [Column("UserLevelTypeName", Order = 1)]
        [MaxLength(150)]
        [Display(Name = "User Level Type")]
        public string UserLevelTypeName { get; set; }

        [Column("UserLevelTypeNameBn", Order = 2)]
        [MaxLength(250)]
        [Display(Name = "User Level Type (Bangla)")]
        public string UserLevelTypeNameBn { get; set; }
    }
}
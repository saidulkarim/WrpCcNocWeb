using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpAdminModCostRange
    {
        [Key]
        [Column("CostRangeId", Order = 0)]
        public int CostRangeId { get; set; }

        [Column("LowerValue", Order = 1)]
        [Display(Name = "Lower Value")]
        public decimal LowerValue { get; set; }

        [Column("UpperValue", Order = 2)]
        [Display(Name = "Upper Value")]
        public decimal UpperValue { get; set; }

        [Column("StateId", Order = 3)]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Column("AdminBndLevel", Order = 4)]
        [Display(Name = "Admin Boundary Level")]
        public int? AdminBndLevel { get; set; }
    }
}

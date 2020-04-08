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

        [Column("LowerValueBn", Order = 3)]
        [Display(Name = "Lower Value")]
        public decimal LowerValueBn { get; set; }

        [Column("UpperValueBn", Order = 4)]
        [Display(Name = "Upper Value")]
        public decimal UpperValueBn { get; set; }

        [Column("StateId", Order = 5)]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Column("AdminBndLevel", Order = 6)]
        [Display(Name = "Admin Boundary Level")]
        public int? AdminBndLevel { get; set; }
    }
}

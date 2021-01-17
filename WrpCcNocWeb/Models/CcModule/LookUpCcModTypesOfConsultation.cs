using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrpCcNocWeb.Models
{
    public class LookUpCcModTypesOfConsultation
    {
        [Key]
        [Column("TypesOfConsultationId", Order = 0)]
        public int TypesOfConsultationId { get; set; }

        [Required]
        [Column("ConsultationName", Order = 1)]
        [MaxLength(100)]
        [Display(Name = "Consultation Type")]
        public string ConsultationName { get; set; }

        [Column("ConsultationNameBn", Order = 2)]
        [MaxLength(150)]
        [Display(Name = "Consultation Type (Bangla)")]
        public string ConsultationNameBn { get; set; }
    }
}
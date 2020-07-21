using System.ComponentModel.DataAnnotations;

namespace WrpCcNocWeb.Models.TempModels
{
    public class CertificateVerify
    {
        [Required(ErrorMessage = "Tracking number is required.")]
        [MaxLength(12, ErrorMessage = "Please enter valid (12 digit) tracking number.")]
        [MinLength(12, ErrorMessage = "Please enter valid (12 digit) tracking number.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid tracking number.")]
        [Display(Name = "Tracking Number")]
        public string AppSubmissionId { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [MaxLength(11, ErrorMessage = "Please enter valid (11 digit) mobile number. e.g. 01511XXXXXX")]
        [MinLength(11, ErrorMessage = "Please enter valid (11 digit) mobile number. e.g. 01511XXXXXX")]
        [Display(Name = "Mobile")]
        public string UserMobile { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Please enter a valid email address.")]
        public string UserEmail { get; set; }
    }
}
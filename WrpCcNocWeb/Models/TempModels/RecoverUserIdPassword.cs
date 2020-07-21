using System.ComponentModel.DataAnnotations;

namespace WrpCcNocWeb.Models.TempModels
{
    public class RecoverUserIdPassword
    {
        [MaxLength(10)]
        public string RecoverType { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", ErrorMessage = "Please enter a valid email address.")]
        public string UserEmail { get; set; }
    }
}

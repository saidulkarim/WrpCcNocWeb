using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.Utility
{
    public class LoginTemp
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        public string UserPass { get; set; }

        [Display(Name = "Enter Code")]
        public string CaptchaCode { get; set; }

        [Display(Name = "SMS Verification Code")]
        public string SmsVerificationCode { get; set; }
    }
}

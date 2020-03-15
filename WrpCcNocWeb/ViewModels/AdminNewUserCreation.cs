using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.ViewModels
{
    public class AdminNewUserCreation
    {
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Display(Name = "User Group")]
        public string UserGroup { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string UserPassword { get; set; }

        [Display(Name = "Confirm Password")]
        public string UserConfirmPassword { get; set; }

        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Display(Name = "Mobile")]
        public string UserMobile { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Security Question")]
        public string SecurityQuestionId { get; set; }

        [Display(Name = "Answer")]
        public string SecurityQuestionAnswer { get; set; }
    }
}
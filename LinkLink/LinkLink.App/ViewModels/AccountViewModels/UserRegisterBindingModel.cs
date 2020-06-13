using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.ViewModels.AccountViewModels
{
    public class UserRegisterBindingModel
    {
        private const string maxLengthErrorMessage = "The {0} should not exceed {1} characters!";
        private const string passwordMismatchErrorMessage = "Password and confirmation password do not match.";

        [Required]
        [EmailAddress]
        [Remote(action: "IsExistingEmail", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = maxLengthErrorMessage)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = passwordMismatchErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}

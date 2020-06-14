using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace LinkLink.App.ViewModels.CompanyViewModels
{
    public class CompanyCreateBindingModel
    {
        private const string maxLengthErrorMessage = "The {0} should not exceed {1} characters!";
        private const string minLengthErrorMessage = "The {0} should be at least {1} characters!";

        [Required]
        [MinLength(3, ErrorMessage = minLengthErrorMessage)]
        [MaxLength(64, ErrorMessage = maxLengthErrorMessage)]
        //[Remote(action: "IsExistingCompanyName", controller: "Company")]
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}

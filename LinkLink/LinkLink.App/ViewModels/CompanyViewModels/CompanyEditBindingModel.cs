using LinkLink.App.ViewModels.OfficeViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinkLink.App.ViewModels.CompanyViewModels
{
    public class CompanyEditBindingModel
    {
        private const string maxLengthErrorMessage = "The {0} should not exceed {1} characters!";
        private const string minLengthErrorMessage = "The {0} should be at least {1} characters!";

        public CompanyEditBindingModel()
        {
            this.EmployeesOffices = new List<OfficeDetailsViewModel>();
        }

        public int CompanyId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = minLengthErrorMessage)]
        [MaxLength(64, ErrorMessage = maxLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public ICollection<OfficeDetailsViewModel> EmployeesOffices { get; set; }
    }
}

using LinkLink.Data.EntityModels.Enums;
using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinkLink.App.ViewModels.EmployeeViewModels
{
    public class EmployeeEditBindingModel
    {
            private const string maxLengthErrorMessage = "The {0} should not exceed {1} characters!";
            private const string minLengthErrorMessage = "The {0} should be at least {1} characters!";
            private const string rangeErrorMessage = "The {0} should be in the range between {1} and {2}!";

            public string Id { get; set; }

            [Required]
            [MinLength(3, ErrorMessage = minLengthErrorMessage)]
            [MaxLength(20, ErrorMessage = maxLengthErrorMessage)]
            public string FirstName { get; set; }

            [Required]
            [MinLength(3, ErrorMessage = minLengthErrorMessage)]
            [MaxLength(20, ErrorMessage = maxLengthErrorMessage)]
            public string LastName { get; set; }

            [Required]
            public DateTime StartingDate { get; set; }

            [Required]
            [Range(1, 30000, ErrorMessage = rangeErrorMessage)]
            public decimal Salary { get; set; }

            [Required]
            [Range(1, 40, ErrorMessage = rangeErrorMessage)]
            public int VacationDays { get; set; }

            [Required]
            public ExperienceLevel ExperienceLevel { get; set; }

            public ICollection<OfficeDetailsServiceModel> EmployeesOffices { get; set; }
    }
}

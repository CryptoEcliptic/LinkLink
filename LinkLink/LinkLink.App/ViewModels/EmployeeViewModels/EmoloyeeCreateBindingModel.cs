using LinkLink.Data.EntityModels;
using LinkLink.Data.EntityModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.ViewModels.EmployeeViewModels
{
    public class EmoloyeeCreateBindingModel
    {
        private const string maxLengthErrorMessage = "The {0} should not exceed {1} characters!";
        private const string minLengthErrorMessage = "The {0} should be at least {1} characters!";
        private const string rangeErrorMessage = "The {0} should be in the range between {1} and {2}!";


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

    }
}

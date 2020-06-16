using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.ViewModels.OfficeViewModels
{
    public class OfficeCreateBindingModel
    {
        private const string maxLengthErrorMessage = "The {0} should not exceed {1} characters!";
        private const string minLengthErrorMessage = "The {0} should be at least {1} characters!";

        public string OfficeId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = minLengthErrorMessage)]
        [MaxLength(64, ErrorMessage = maxLengthErrorMessage)]
        public string Country { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = minLengthErrorMessage)]
        [MaxLength(64, ErrorMessage = maxLengthErrorMessage)]
        public string City { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = minLengthErrorMessage)]
        [MaxLength(64, ErrorMessage = maxLengthErrorMessage)]
        public string Street { get; set; }

        [MinLength(1, ErrorMessage = minLengthErrorMessage)]
        [MaxLength(6, ErrorMessage = maxLengthErrorMessage)]
        public string StreetNumber { get; set; }

        [Display(Name ="Is HeadQuater")]
        public bool IsHQ { get; set; }

    }
}

using LinkLink.Data.EntityModels.Enums;
using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.ViewModels.EmployeeViewModels
{
    public class EmployeeDetailsViewModel
    {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public ICollection<OfficeDetailsServiceModel> EmployeesOffices { get; set; }
    }
}

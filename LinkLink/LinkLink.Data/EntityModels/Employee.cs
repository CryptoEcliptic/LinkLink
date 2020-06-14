using LinkLink.Data.EntityModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinkLink.Data.EntityModels
{
    public class Employee
    {
        public Employee()
        {
            this.EmployeesOffices = new List<EmployeeOffice>();
            this.EmployeeId = Guid.NewGuid().ToString();
        }

        [Key]
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public ICollection<EmployeeOffice> EmployeesOffices { get; set; }
    }
}

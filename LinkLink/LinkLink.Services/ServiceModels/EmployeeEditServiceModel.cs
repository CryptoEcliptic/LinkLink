using LinkLink.Data.EntityModels.Enums;
using System;
using System.Collections.Generic;

namespace LinkLink.Services.ServiceModels
{
    public class EmployeeEditServiceModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

    }
}

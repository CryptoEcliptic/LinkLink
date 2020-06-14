using LinkLink.Data.EntityModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkLink.Services.ServiceModels
{
    public class EmployeeIndexServiceModel
    {
        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }
    }
}

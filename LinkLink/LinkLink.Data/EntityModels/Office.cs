using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinkLink.Data.EntityModels
{
    public class Office
    {
        public Office()
        {
            this.EmployeesOffices = new List<EmployeeOffice>();
        }

        [Key]
        public string OfficeId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public bool IsHQ { get; set; }

        public ICollection<EmployeeOffice> EmployeesOffices { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LinkLink.Services.ServiceModels
{
    public class OfficeEmployeeServiceModel
    {
        public string Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public bool IsSelected { get; set; }
    }
}

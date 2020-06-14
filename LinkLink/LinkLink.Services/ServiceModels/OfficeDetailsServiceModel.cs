using System;
using System.Collections.Generic;
using System.Text;

namespace LinkLink.Services.ServiceModels
{
    public class OfficeDetailsServiceModel
    {
        public string OfficeId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public bool IsHQ { get; set; }
    }
}

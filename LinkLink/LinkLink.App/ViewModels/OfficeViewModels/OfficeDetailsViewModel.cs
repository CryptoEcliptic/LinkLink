using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.ViewModels.OfficeViewModels
{
    public class OfficeDetailsViewModel
    {
        public string OfficeId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public bool IsHQ { get; set; }
    }
}

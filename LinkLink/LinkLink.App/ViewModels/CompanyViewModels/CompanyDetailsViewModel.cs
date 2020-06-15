using LinkLink.App.ViewModels.OfficeViewModels;
using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.ViewModels.CompanyViewModels
{
    public class CompanyDetailsViewModel
    {
        public CompanyDetailsViewModel()
        {
            this.Offices = new List<OfficeDetailsViewModel>();
        }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<OfficeDetailsViewModel> Offices { get; set; }
    }
}

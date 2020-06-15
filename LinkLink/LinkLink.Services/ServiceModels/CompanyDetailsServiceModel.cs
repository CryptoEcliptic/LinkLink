using System;
using System.Collections.Generic;

namespace LinkLink.Services.ServiceModels
{
    public class CompanyDetailsServiceModel
    {
        public CompanyDetailsServiceModel()
        {
            this.Offices = new List<OfficeDetailsServiceModel>();
        }

        public int CompanyId { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<OfficeDetailsServiceModel> Offices { get; set; }
    }
}

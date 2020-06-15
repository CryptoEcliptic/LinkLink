using LinkLink.Data;
using LinkLink.Data.EntityModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinkLink.Services
{
    public class OfficeServices : IOfficeServices
    {
        private readonly ApplicationDbContext _context;

        public OfficeServices(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> CreateOfficeAsync(OfficeCreateServiceModel model)
        {
            Office office = new Office()
            {
                City = model.City,
                Country = model.Country,
                Street = model.Street,
                StreetNumber = model.Street,
                IsHQ = model.IsHQ,
            };

            this._context.Offices.Add(office);
            int result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}

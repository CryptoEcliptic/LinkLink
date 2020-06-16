using LinkLink.Data;
using LinkLink.Data.EntityModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                StreetNumber = model.StreetNumber,
                IsHQ = model.IsHQ,
                CompanyId = model.CompanyId
            };

            this._context.Offices.Add(office);
            int result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<ICollection<OfficeIndexServiceModel>> GetAllOfficesAsync()
        {
           List<Office> offices = await Task.Run(() => this._context.Offices.ToList());

            List<OfficeIndexServiceModel> serviceModel = offices.Select(o => new OfficeIndexServiceModel
            {
                 Id = o.OfficeId,
                 Country = o.Country,
                 City = o.City,
                 Street = o.Street
            })
            .ToList();

            return serviceModel;
        }

        public async Task<ICollection<OfficeEmployeeServiceModel>> ManageEmployeeOffices(string employeeId)
        {
            List<Office> offices = await Task.Run(() => this._context.Offices.ToList());
            Employee employee = await Task.Run(() => this._context.Employees
                                                    .Include(o => o.EmployeesOffices)
                                                    .FirstOrDefault(e => e.EmployeeId == employeeId));
            ICollection<OfficeEmployeeServiceModel> serviceModel = new List<OfficeEmployeeServiceModel>();

            foreach (var office in offices)
            {
                OfficeEmployeeServiceModel model = new OfficeEmployeeServiceModel()
                {
                     Id  = office.OfficeId,
                     Country = office.Country,
                     City = office.City,
                     Street = office.Street,
                };

                //TODO Mark IsSelected only assigned officess to a user

            }

            return serviceModel;
        }
    }
}

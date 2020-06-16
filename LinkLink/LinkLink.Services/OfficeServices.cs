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
            List<Office> offices = await Task.Run(() => this._context.Offices
                                                               .Include(x => x.EmployeesOffices)
                                                               .ToList());

            Employee employee = await Task.Run(() => this._context.Employees
                                                    .Include(eo => eo.EmployeesOffices)
                                                    .ThenInclude(o => o.Office)
                                                    .FirstOrDefault(e => e.EmployeeId == employeeId));

            ICollection<OfficeEmployeeServiceModel> serviceModel = new List<OfficeEmployeeServiceModel>();

            foreach (var office in offices)
            {
                OfficeEmployeeServiceModel officeEmployeeServiceModel = new OfficeEmployeeServiceModel()
                {
                     Id  = office.OfficeId,
                     Country = office.Country,
                     City = office.City,
                     Street = office.Street,
                };

                if (employee.EmployeesOffices.Select(x => x.OfficeId).Contains(office.OfficeId))
                {
                    officeEmployeeServiceModel.IsSelected = true;
                }
                
                
                else
                {
                    officeEmployeeServiceModel.IsSelected = false;
                }

                serviceModel.Add(officeEmployeeServiceModel);
            }

            return serviceModel;
        }

        public async Task<bool> UpdateOfficeEmployees(string employeeId, ICollection<OfficeEmployeesUpdateServiceModel> model)
        {
            Employee employee = await this._context.Employees
                                        .Include(eo => eo.EmployeesOffices)
                                        .ThenInclude(o => o.Office)
                                        .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                return false;
            }

            foreach (var office in model)
            {
                EmployeeOffice employeeOffice = new EmployeeOffice { EmployeeId = employee.EmployeeId, OfficeId = office.OfficeId };

                if (office.IsSelected && !employee.EmployeesOffices.Select(x => x.OfficeId).Contains(employeeOffice.OfficeId))
                {
                    employee.EmployeesOffices.Add(employeeOffice);
                }
                else if(office.IsSelected && employee.EmployeesOffices.Select(x => x.OfficeId).Contains(employeeOffice.OfficeId))
                {
                    continue;
                }
                else if (!office.IsSelected && !employee.EmployeesOffices.Select(x => x.OfficeId).Contains(employeeOffice.OfficeId))
                {
                    continue;
                }
                else
                {
                    var entityToRemove = await this._context.EmployeesOffices.FirstOrDefaultAsync(x => x.OfficeId == employeeOffice.OfficeId && x.EmployeeId == employee.EmployeeId);
                    this._context.EmployeesOffices.Remove(entityToRemove);
                    await this._context.SaveChangesAsync();
                }
            }

            this._context.Employees.Update(employee);
            int result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}

using LinkLink.Data;
using LinkLink.Data.EntityModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly ApplicationDbContext _context;

        public EmployeeServices(ApplicationDbContext context)
        {
            this._context = context;
        }


        public async Task<bool> CreateEmployeeAsync(EmployeeCreateServiceModel model)
        {
            Employee employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ExperienceLevel = model.ExperienceLevel,
                Salary = model.Salary,
                StartingDate = model.StartingDate,
                VacationDays = model.VacationDays,
            };

            this._context.Employees.Add(employee);
            int result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteEmployeeAsync(string id)
        {
            Employee employee = this._context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
            {
                return false;
            }

            this._context.Employees.Remove(employee);
            int result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<EmployeeIndexServiceModel>> GetAllEmployeesAsync()
        {
            List<Employee> employeesFromDb = await Task.Run(() => this._context.Employees.ToList());
            List<EmployeeIndexServiceModel> employees = new List<EmployeeIndexServiceModel>();

            foreach (var em in employeesFromDb)
            {
                EmployeeIndexServiceModel employee = new EmployeeIndexServiceModel()
                {
                    EmployeeId = em.EmployeeId,
                    FirstName = em.FirstName,
                    LastName = em.LastName,
                    ExperienceLevel = em.ExperienceLevel
                };

                employees.Add(employee);
            }

            return employees;
        }

        public async Task<EmployeeDetailsServiceModel> GetEmployeeByIdAsync(string id)
        {
            Employee employee = await this._context.Employees
                                        .Include(eo => eo.EmployeesOffices)
                                        .ThenInclude(o => o.Office)
                                        .FirstOrDefaultAsync(e => e.EmployeeId == id);

            IEnumerable<string> officesKeys = employee.EmployeesOffices.Select(x => x.OfficeId).ToList();

            List<Office> offices = this._context.Offices
                                                 .Where(x => officesKeys.Contains(x.OfficeId))
                                                 .ToList();

            List<OfficeDetailsServiceModel> officeDetailServiceModelCollection = offices.Select(o => new OfficeDetailsServiceModel
            {
                City = o.City,
                Country = o.Country,
                OfficeId = o.OfficeId,
                Street = o.Street,
                StreetNumber = o.StreetNumber,
                IsHQ = o.IsHQ
            })
            .ToList();

            EmployeeDetailsServiceModel serviceModel = new EmployeeDetailsServiceModel()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                StartingDate = employee.StartingDate,
                ExperienceLevel = employee.ExperienceLevel,
                VacationDays = employee.VacationDays,
                EmployeesOffices = officeDetailServiceModelCollection
            };

            return serviceModel;
        }

        public async Task<bool> UpdateAsync(EmployeeEditServiceModel employee)
        {
            Employee dbEmployee = await this._context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.Id);

            if (dbEmployee == null)
            {
                return false;
            }

            dbEmployee.ExperienceLevel = employee.ExperienceLevel;
            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.LastName = employee.LastName;
            dbEmployee.Salary = employee.Salary;
            dbEmployee.VacationDays = employee.VacationDays;
            dbEmployee.StartingDate = employee.StartingDate;

            var employeeToUpdate = _context.Employees.Attach(dbEmployee);
            employeeToUpdate.State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}

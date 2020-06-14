using LinkLink.Data;
using LinkLink.Data.EntityModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.Services
{
    public class EmployeeRepository : IEmployeeServices
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            this._context = context;
        }


        public async Task<bool> CreateEmployeeAsync(CreateEmployeeServiceModel model)
        {
            Employee employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ExperienceLevel = model.ExperienceLevel,
                Salary = model.Salary,
                StartingDate = model.StartingDate,
                VacationDays = model.VacationDays,
                EmployeesOffices = model.EmployeesOffices
            };

           await Task.Run(() => this._context.Employees.Add(employee));
           var result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
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

        public EmployeeDetailsServiceModel GetEmployeeById(int Id)
        {
            throw new NotImplementedException();
        }

        public UpdateEmployeeServiceModel Update(UpdateEmployeeServiceModel employee)
        {
            throw new NotImplementedException();
        }
    }
}

using LinkLink.Data.EntityModels;
using LinkLink.Data.Repositories.Contracts;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;

namespace LinkLink.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmployeeServices(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            this._employeeRepository = employeeRepository;
            this._hostingEnvironment = hostingEnvironment;
        }


        public bool CreateEmployee(CreateEmployeeServiceModel model)
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

            Employee employeeCreated = this._employeeRepository.Add(employee);

            if (employeeCreated != null)
            {
                return true;
            }

            return false;
        }

        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeIndexServiceModel> GetAllEmployees()
        {
            throw new NotImplementedException();
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

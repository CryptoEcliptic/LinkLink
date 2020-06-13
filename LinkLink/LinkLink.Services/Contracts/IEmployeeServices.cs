using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkLink.Services.Contracts
{
    public interface IEmployeeServices
    {
        EmployeeDetailsServiceModel GetEmployeeById(int Id);

        IEnumerable<EmployeeIndexServiceModel> GetAllEmployees();

        bool CreateEmployee(CreateEmployeeServiceModel employee);

        UpdateEmployeeServiceModel Update(UpdateEmployeeServiceModel employee);

        bool DeleteEmployee(int id);
    }
}

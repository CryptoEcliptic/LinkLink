﻿using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinkLink.Services.Contracts
{
    public interface IEmployeeServices
    {
        EmployeeDetailsServiceModel GetEmployeeById(int Id);

        Task<IEnumerable<EmployeeIndexServiceModel>> GetAllEmployeesAsync();

        Task<bool> CreateEmployeeAsync(CreateEmployeeServiceModel employee);

        UpdateEmployeeServiceModel Update(UpdateEmployeeServiceModel employee);

        bool DeleteEmployee(int id);
    }
}

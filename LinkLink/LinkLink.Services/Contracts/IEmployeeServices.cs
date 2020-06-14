using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinkLink.Services.Contracts
{
    public interface IEmployeeServices
    {
        Task<EmployeeDetailsServiceModel> GetEmployeeByIdAsync(string Id);

        Task<IEnumerable<EmployeeIndexServiceModel>> GetAllEmployeesAsync();

        Task<bool> CreateEmployeeAsync(CreateEmployeeServiceModel employee);

        Task<bool> UpdateAsync(EmployeeEditServiceModel employee);

        Task<bool> DeleteEmployeeAsync(string id);
    }
}

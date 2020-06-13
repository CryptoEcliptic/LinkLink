using LinkLink.Data.EntityModels;
using System.Collections.Generic;

namespace LinkLink.Data.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        Employee GetById(int Id);

        IEnumerable<Employee> GetEmployees();

        Employee Add(Employee employee);

        Employee Update(Employee employee);

        Employee Delete(int id);
    }
}

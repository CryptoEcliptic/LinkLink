using LinkLink.Data.EntityModels;
using LinkLink.Data.Repositories.Contracts;
using System.Collections.Generic;

namespace LinkLink.Data.Repositories
{
    class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _context.Employees.Find(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }

        public Employee GetById(int id)
        {
            Employee employee = _context.Employees.Find(id);
            return employee;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        public Employee Update(Employee employee)
        {
            var employeeToUpdate = _context.Employees.Attach(employee);
            employeeToUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employee;
        }
    }
}

using LinkLink.App.ViewModels.EmployeeViewModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkLink.App.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            this._employeeServices = employeeServices;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<EmployeeIndexServiceModel> model = await this._employeeServices.GetAllEmployeesAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmoloyeeCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceModel = new EmployeeCreateServiceModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Salary = model.Salary,
                    StartingDate = model.StartingDate,
                    VacationDays = model.VacationDays,
                    ExperienceLevel = model.ExperienceLevel
                };

                bool result = await this._employeeServices.CreateEmployeeAsync(serviceModel);
                if (!result)
                {
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction("Index", "Employee");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            EmployeeDetailsServiceModel employee = await this._employeeServices.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            EmployeeDetailsServiceModel employee = await this._employeeServices.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            EmployeeEditBindingModel model = new EmployeeEditBindingModel()
            {
                Id = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                StartingDate = employee.StartingDate,
                Salary = employee.Salary,
                ExperienceLevel = employee.ExperienceLevel,
                VacationDays = employee.VacationDays,
                EmployeesOffices = employee.EmployeesOffices
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditBindingModel model)
        {
            if (ModelState.IsValid)
            {
                EmployeeEditServiceModel employeeEditServiceModel = new EmployeeEditServiceModel()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StartingDate = model.StartingDate,
                    Salary = model.Salary,
                    ExperienceLevel = model.ExperienceLevel,
                    VacationDays = model.VacationDays,
                };

                bool result = await this._employeeServices.UpdateAsync(employeeEditServiceModel);

                if (!result)
                {
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction("Details", new { id = model.Id });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await this._employeeServices.DeleteEmployeeAsync(id);

            if (!result)
            {
                return RedirectToAction("Error", "Home");
                
            }

            return RedirectToAction("Index", "Employee");
        }
    }
}

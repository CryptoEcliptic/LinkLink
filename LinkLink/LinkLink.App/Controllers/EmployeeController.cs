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
            //TODO Return available offices
            EmoloyeeCreateBindingModel model = new EmoloyeeCreateBindingModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmoloyeeCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceModel = new CreateEmployeeServiceModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Salary = model.Salary,
                    StartingDate = model.StartingDate,
                    VacationDays = model.VacationDays,
                    ExperienceLevel = model.ExperienceLevel
                };

                await this._employeeServices.CreateEmployeeAsync(serviceModel);
                return RedirectToAction("Index", "Employee");
            }

            return View(model);
        }
    }
}

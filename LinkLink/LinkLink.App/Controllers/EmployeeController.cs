﻿using LinkLink.App.ViewModels.EmployeeViewModels;
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
                var serviceModel = new CreateEmployeeServiceModel()
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
    }
}

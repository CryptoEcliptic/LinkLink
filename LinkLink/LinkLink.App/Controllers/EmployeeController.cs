using LinkLink.App.ViewModels.EmployeeViewModels;
using LinkLink.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            this._employeeServices = employeeServices;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmoloyeeCreateBindingModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    this._employeeServices.CreateEmployee();
            //    return RedirectToAction("Details", new { id = newEmployee.Id });
            //}

            return View(model);
        }
    }
}

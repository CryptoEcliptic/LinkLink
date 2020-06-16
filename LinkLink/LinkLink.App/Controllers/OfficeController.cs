using LinkLink.App.ViewModels.OfficeViewModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.Controllers
{
    [Authorize]
    public class OfficeController : Controller
    {
        private readonly IOfficeServices _officeServices;

        public OfficeController(IOfficeServices officeServices)
        {
            this._officeServices = officeServices;
        }

        [HttpGet]
        public IActionResult Create(int companyId)
        {
            this.ViewBag.CompanyId = companyId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfficeCreateBindingModel model, int companyId)
        {
            if (ModelState.IsValid)
            {
                var serviceModel = new OfficeCreateServiceModel()
                {
                    CompanyId = companyId,
                    Country = model.Country,
                    City = model.City,
                    Street = model.Street,
                    StreetNumber = model.StreetNumber,
                    IsHQ = model.IsHQ,
                };

                bool result = await this._officeServices.CreateOfficeAsync(serviceModel);

                if (!result)
                {
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction("Details", "Company", new { id = companyId });
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddOfficeEmployee(string employeeId)
        {
            this.ViewBag.EmployeeId = employeeId;
            ICollection<OfficeEmployeeServiceModel> serviceModel = await this._officeServices.ManageEmployeeOffices(employeeId);

            List<OfficeEmployeeViewModel> model = serviceModel
                        .Select(o => new OfficeEmployeeViewModel
                        {
                             Id = o.Id,
                             Country = o.Country,
                             City = o.City,
                             Street = o.Street,
                             IsSelected = o.IsSelected,
                        })
                        .ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOfficeEmployee(List<OfficeEmployeeViewModel> model, string employeeId)
        {
  
            ICollection<OfficeEmployeesUpdateServiceModel> serviceModel = model
                                    .Select(o => new OfficeEmployeesUpdateServiceModel
                                    {
                                        OfficeId = o.Id,
                                        IsSelected = o.IsSelected,
                                    })
                                    .ToList();

            bool rerult = await this._officeServices.UpdateOfficeEmployees(employeeId, serviceModel);

            return RedirectToAction("Details", "Employee", new { id = employeeId });
        }
    }
}

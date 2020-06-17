using LinkLink.App.ViewModels.CompanyViewModels;
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
    public class CompanyController : Controller
    {
        private readonly ICompanyServices _companyServices;

        public CompanyController(ICompanyServices companyServices)
        {
            this._companyServices = companyServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CompanyIndexServiceModel> companies = await this._companyServices.GetAllCompaniesAsync();

            List<CompanyIndexViewModel> model = new List<CompanyIndexViewModel>();

            foreach (var cp in companies)
            {
                CompanyIndexViewModel company = new CompanyIndexViewModel()
                {
                    CompanyId = cp.CompanyId,
                    Name = cp.Name,
                    CreationDate = cp.CreationDate.ToString("MM/dd/yyyy"),
                };

                model.Add(company);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var serviceModel = new CompanyCreateServiceModel()
                {
                    Name = model.Name,
                    CreationDate = model.CreationDate,
                };

                bool result = await this._companyServices.CreateCompanyAsync(serviceModel);
                if (!result)
                {
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction("Index", "Company");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            CompanyDetailsServiceModel company = await this._companyServices.GetByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            List<OfficeDetailsViewModel> offices = company.Offices
                .Select(x => new OfficeDetailsViewModel
                {
                    City = x.City,
                    OfficeId = x.OfficeId,
                    Country = x.Country,
                    IsHQ = x.IsHQ == true? "The office is Headquarter" : "",
                    Street = x.Street,
                    StreetNumber = x.StreetNumber
                })
                .ToList();

            CompanyDetailsViewModel model = new CompanyDetailsViewModel()
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                CreationDate = company.CreationDate.ToString("MM/dd/yyyy"),
                Offices = offices
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            CompanyDetailsServiceModel company = await this._companyServices.GetByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            List<OfficeDetailsViewModel> offices = company.Offices
                .Select(x => new OfficeDetailsViewModel
                {
                    City = x.City,
                    OfficeId = x.OfficeId,
                    Country = x.Country,
                    IsHQ = x.IsHQ == true ? "The office is Headquarter" : "",
                    Street = x.Street,
                    StreetNumber = x.StreetNumber
                })
                .ToList();

            CompanyEditBindingModel model = new CompanyEditBindingModel()
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                CreationDate = company.CreationDate,
                EmployeesOffices = offices,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompanyEditBindingModel model)
        {
            if (ModelState.IsValid)
            {
                CompanyEditServiceModel serviceModel = new CompanyEditServiceModel()
                {
                    CompanyId = model.CompanyId,
                    CreationDate = model.CreationDate,
                    Name = model.Name
                };

                bool result = await this._companyServices.UpdateAsync(serviceModel);

                if (!result)
                {
                    return RedirectToAction("Error", "Home");
                }

                return RedirectToAction("Details", new { id = model.CompanyId });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await this._companyServices.DeleteCompanyAsync(id);

            if (!result)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Company");
        }

        public async Task<JsonResult> IsExistingCompanyName(string name)
        {
            bool result = await this._companyServices.IsExistingNameAsync(name);

            if (!result )
            {
                return Json(true);
            }
            return Json($"Company withe name: {name} already exists!");
        }
    }
}

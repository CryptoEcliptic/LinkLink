using LinkLink.App.ViewModels.CompanyViewModels;
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
                    CreationDate = cp.CreationDate,
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

        public async Task<JsonResult> IsExistingCompanyName(string name)
        {
            bool result = await this._companyServices.IsExistingNameAsync(name);

            if (!result)
            {
                return Json(true);
            }
            return Json($"Company withe name: {name} already exists!");
        }
    }
}

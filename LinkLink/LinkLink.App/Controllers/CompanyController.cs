using LinkLink.App.ViewModels.CompanyViewModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLink.App.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyServices _companyServices;

        public CompanyController(ICompanyServices companyServices)
        {
            this._companyServices = companyServices;
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

        //public async Task<JsonResult> IsExistingCompanyName(string name)
        //{

        //    if (user == null)
        //    {
        //        return Json(true);
        //    }
        //    return Json($"User with username: {username} already exists!");
        //}
    }
}

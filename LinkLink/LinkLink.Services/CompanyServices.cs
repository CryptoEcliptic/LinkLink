﻿using LinkLink.Data;
using LinkLink.Data.EntityModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinkLink.Services
{
    public class CompanyServices : ICompanyServices
    {
        private readonly ApplicationDbContext _context;

        public CompanyServices(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> CreateCompanyAsync(CompanyCreateServiceModel companyModel)
        {
            Company company = new Company()
            {
                Name = companyModel.Name,
                CreationDate = companyModel.CreationDate
            };

            this._context.Companies.Add(company);
            int result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}

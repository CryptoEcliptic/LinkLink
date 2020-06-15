using LinkLink.Data;
using LinkLink.Data.EntityModels;
using LinkLink.Services.Contracts;
using LinkLink.Services.ServiceModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<CompanyIndexServiceModel>> GetAllCompaniesAsync()
        {
            IQueryable<Company> companies = await Task.Run(() => this._context.Companies);

            List<CompanyIndexServiceModel> model = new List<CompanyIndexServiceModel>();

            foreach (var cp in companies)
            {
                CompanyIndexServiceModel company = new CompanyIndexServiceModel()
                {
                    CompanyId = cp.CompanyId,
                    Name = cp.Name,
                    CreationDate = cp.CreationDate,
                };
                model.Add(company);
            }

            return model;
        }

        public async Task<CompanyDetailsServiceModel> GetByIdAsync(int id)
        {
            Company company = await this._context.Companies
                .Include(o => o.Offices)
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            List<OfficeDetailsServiceModel> offices = company.Offices
               .Select(x => new OfficeDetailsServiceModel
               {
                   City = x.City,
                   OfficeId = x.OfficeId,
                   Country = x.Country,
                   IsHQ = x.IsHQ,
                   Street = x.Street,
                   StreetNumber = x.StreetNumber
               })
               .ToList();

            CompanyDetailsServiceModel model = new CompanyDetailsServiceModel()
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                CreationDate = company.CreationDate,
                Offices = offices,
            };

            return model;
        }

        public async Task<bool> UpdateAsync(CompanyEditServiceModel model)
        {
            Company dbECompany = await this._context.Companies.FirstOrDefaultAsync(e => e.CompanyId == model.CompanyId);

            if (dbECompany == null)
            {
                return false;
            }

            dbECompany.Name = model.Name;
            dbECompany.CreationDate = model.CreationDate;

            var companyToUpdate = _context.Companies.Attach(dbECompany);
            companyToUpdate.State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsExistingNameAsync(string name)
        {
            Company company = await Task.Run(() => this._context.Companies.FirstOrDefault(c => c.Name == name));

            if (company != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            Company company = this._context.Companies.FirstOrDefault(e => e.CompanyId == id);

            if (company == null)
            {
                return false;
            }

            this._context.Companies.Remove(company);
            int result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}
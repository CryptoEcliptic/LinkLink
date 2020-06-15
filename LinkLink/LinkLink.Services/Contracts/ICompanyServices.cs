using LinkLink.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LinkLink.Services.Contracts
{
    public interface ICompanyServices
    {
        Task<bool> CreateCompanyAsync(CompanyCreateServiceModel company);

        Task<bool> IsExistingNameAsync(string name);

        Task<IEnumerable<CompanyIndexServiceModel>> GetAllCompaniesAsync();

        Task<CompanyDetailsServiceModel> GetByIdAsync(int id);

        Task<bool> UpdateAsync(CompanyEditServiceModel model);

        Task<bool> DeleteCompanyAsync(int id);

    }
}

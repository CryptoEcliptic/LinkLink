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

        //Task<bool> GetByNameAsync(string name);

    }
}

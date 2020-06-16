using LinkLink.Services.ServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkLink.Services.Contracts
{
    public interface IOfficeServices
    {
        Task<bool> CreateOfficeAsync(OfficeCreateServiceModel model);

        Task<ICollection<OfficeIndexServiceModel>> GetAllOfficesAsync();
    }
}

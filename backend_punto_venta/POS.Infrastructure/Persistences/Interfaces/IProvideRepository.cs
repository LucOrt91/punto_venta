using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IProvideRepository
    {
        Task<BaseEntityResponse<Provider>> ListProvider (BaseFilterRequest filter);
    }
}

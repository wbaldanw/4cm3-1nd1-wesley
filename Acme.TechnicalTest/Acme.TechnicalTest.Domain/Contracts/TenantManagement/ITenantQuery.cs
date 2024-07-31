using Acme.TechnicalTest.Domain.DTO.TenantManagement;

namespace Acme.TechnicalTest.Domain.Contracts.TenantManagement
{
    public interface ITenantQuery
    {
        Task<TenantView?> Get(long id);
        Task<List<TenantView>> GetAll();
    }
}

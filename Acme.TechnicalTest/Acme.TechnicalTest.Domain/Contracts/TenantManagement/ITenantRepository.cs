using Acme.TechnicalTest.Domain.Domain.TenantManagement;

namespace Acme.TechnicalTest.Domain.Contracts.TenantManagement
{
    public interface ITenantRepository
    {
        void Add(Tenant tenant);
        Task DeleteAllTenantData(long tenantId);
        Task<Tenant?> GetById(long id);
        Task<List<Tenant>> GetChildrenTenant(long tenantId);
        Task<bool> HasTenatWithName(string name);
    }
}

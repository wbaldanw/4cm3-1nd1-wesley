using Acme.TechnicalTest.Domain.DTO.TenantManagement;

namespace Acme.TechnicalTest.Domain.Contracts.TenantManagement
{
    public interface IAddTenantUC
    {
        Task<long> Create(AddTenantRequest tenantDTO);
    }
}

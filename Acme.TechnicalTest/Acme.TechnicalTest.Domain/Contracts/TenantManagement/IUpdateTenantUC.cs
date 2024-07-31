using Acme.TechnicalTest.Domain.DTO.TenantManagement;

namespace Acme.TechnicalTest.Domain.Contracts.TenantManagement
{
    public interface IUpdateTenantUC
    {
        Task Update(long id, UpdateTenantRequest request);
    }
}

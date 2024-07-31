
namespace Acme.TechnicalTest.Domain.Contracts.TenantManagement
{
    public interface ICopyDataFromParentTenantUC
    {
        Task CopyData(long childTenantId);
    }
}

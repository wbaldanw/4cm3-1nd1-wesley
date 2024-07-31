namespace Acme.TechnicalTest.Domain.Contracts.TenantManagement
{
    public interface IDeleteTenantUC
    {
        Task Delete(long id);
    }
}

namespace Acme.TechnicalTest.Domain.Contracts.Shared
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}

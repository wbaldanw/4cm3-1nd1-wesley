using Acme.TechnicalTest.Domain.Domain.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        Task<Category?> Get(long id);
        Task<bool> CategoryHasProducts(long id);
        void Delete(Category category);
        Task<Category?> GetByReferenceSourceAndTenant(Guid reference, long tenantId);
        Task<List<Category>> GetAllFromTenant(long? tenantId);
    }
}

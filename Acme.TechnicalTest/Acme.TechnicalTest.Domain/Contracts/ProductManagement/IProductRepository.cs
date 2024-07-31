using Acme.TechnicalTest.Domain.Domain.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface IProductRepository
    {
        void Add(Product Product);
        Task<Product?> Get(long id);        
        void Delete(Product Product);
        Task<List<Product>> GetAllFromTenant(long? tenantId);
    }
}

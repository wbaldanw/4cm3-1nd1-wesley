using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface IProductQuery
    {
        Task<ProductView?> Get(long id);

        Task<List<ProductView>> GetAll();
    }
}

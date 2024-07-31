using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface ICategoryQuery
    {
        Task<CategoryView?> Get(long id);

        Task<List<CategoryView>> GetAll();
    }
}

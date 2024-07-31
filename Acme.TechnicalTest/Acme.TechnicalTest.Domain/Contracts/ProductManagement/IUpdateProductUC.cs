using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface IUpdateProductUC
    {
        Task Update(long id, UpdateProductRequest request);
    }   
}

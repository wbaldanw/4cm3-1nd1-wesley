using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface IAddProductUC
    {
        Task<long> Add(AddProductRequest request);
    }
}

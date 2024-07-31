using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface IUpdateCategoryUC
    {
        Task Update(long id, UpdateCategoryRequest request);
    }   
}

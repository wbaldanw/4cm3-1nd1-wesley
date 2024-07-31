using Acme.TechnicalTest.Domain.Domain.TenantManagement;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface IAddCategoryUC
    {
        Task<long> Add(AddCategoryRequest request);        
    }
}

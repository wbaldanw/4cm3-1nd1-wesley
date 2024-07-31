using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface ICloneProductToChildTenantUC
    {
        Task CloneProduct(ProductCreatedMessage dto);
    }
}

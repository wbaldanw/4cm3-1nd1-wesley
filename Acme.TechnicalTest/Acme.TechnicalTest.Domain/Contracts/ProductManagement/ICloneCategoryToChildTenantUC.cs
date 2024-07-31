using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface ICloneCategoryToChildTenantUC
    {
        Task CloneCategory(CategoryCreatedMessage dto);
    }
}

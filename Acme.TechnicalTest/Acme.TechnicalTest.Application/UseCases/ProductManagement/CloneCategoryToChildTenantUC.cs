using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Application.UseCases.ProductManagement
{
    public class CloneCategoryToChildTenantUC : ICloneCategoryToChildTenantUC
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ITenantRepository tenantRepository;
        private readonly IUnitOfWork uow;

        public CloneCategoryToChildTenantUC(ICategoryRepository categoryRepository,
                                            ITenantRepository tenantRepository,
                                            IUnitOfWork uow)
        {
            this.categoryRepository = categoryRepository;
            this.tenantRepository = tenantRepository;
            this.uow = uow;
        }        

        public async Task CloneCategory(CategoryCreatedMessage dto)
        {
            var childTenants = await tenantRepository.GetChildrenTenant(dto.TenantId);

            foreach (var childTenant in childTenants)
            {
                var category = new Category(dto.Name);
                category.SetReferenceSource(dto, childTenant);

                categoryRepository.Add(category);
            }

            await uow.SaveChangesAsync();
        }
    }    
}

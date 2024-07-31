using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Application.UseCases.ProductManagement
{
    public class CloneProductToChildTenantUC : ICloneProductToChildTenantUC
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITenantRepository tenantRepository;
        private readonly IUnitOfWork uow;

        public CloneProductToChildTenantUC(IProductRepository productRepository,
                                           ICategoryRepository categoryRepository,
                                           ITenantRepository tenantRepository,
                                           IUnitOfWork uow)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.tenantRepository = tenantRepository;
            this.uow = uow;
        }

        public async Task CloneProduct(ProductCreatedMessage dto)
        {
            var childTenants = await tenantRepository.GetChildrenTenant(dto.TenantId);

            foreach (var childTenant in childTenants)
            {
                var product = new Product(dto.Name);
                product.SetReferenceSource(dto, childTenant);

                product.Description = dto.Description;

                if (dto.CategoryReference.HasValue)
                {
                    var category = await categoryRepository.GetByReferenceSourceAndTenant(dto.CategoryReference.Value, childTenant.Id);
                    if (category is not null)
                        product.Category = category;
                }

                productRepository.Add(product);
            }
            
            await uow.SaveChangesAsync();
        }
    }
}

using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Acme.TechnicalTest.Domain.Domain.TenantManagement;

namespace Acme.TechnicalTest.Application.UseCases.TenantManagement
{
    public class CopyDataFromParentTenantUC : ICopyDataFromParentTenantUC
    {
        private readonly ITenantRepository tenantRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork uow;

        public CopyDataFromParentTenantUC(ITenantRepository tenantRepository,
                                          ICategoryRepository categoryRepository,
                                          IProductRepository productRepository,
                                          IUnitOfWork uow)
        {
            this.tenantRepository = tenantRepository;
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.uow = uow;
        }

        public async Task CopyData(long childTenantId)
        {
            var tenant = await tenantRepository.GetById(childTenantId);

            if (tenant?.ParentId is null)
                return;

            var categories = await categoryRepository.GetAllFromTenant(tenant.ParentId);
            var products = await productRepository.GetAllFromTenant(tenant.ParentId);
            List<Category> categoriesForTheNewTenant = SetupCategories(tenant, categories).ToList();
            SetupProducts(tenant, products, categoriesForTheNewTenant);

            await uow.SaveChangesAsync();
        }

        private void SetupProducts(Tenant? tenant, List<Product> products, List<Category> categoriesForTheNewTenant)
        {
            foreach (var product in products)
            {
                var newProduct = new Product(product.Name);
                newProduct.SetReferenceSource(product, tenant.Parent);
                newProduct.Description = product.Description;

                if (product.Category is not null)
                {
                    var category = categoriesForTheNewTenant.FirstOrDefault(x => x.ReferenceSource == product.Category.Reference);
                    newProduct.Category = category;
                }

                productRepository.Add(newProduct);
            }
        }

        private IEnumerable<Category> SetupCategories(Tenant? tenant, List<Category> categories)
        {
            var categoriesForTheNewTenant = new List<Category>();
            foreach (var category in categories)
            {
                var newCategory = new Category(category.Name);
                newCategory.SetReferenceSource(category, tenant.Parent);
                
                categoryRepository.Add(newCategory);

                yield return newCategory;
            }
        }
    }
}

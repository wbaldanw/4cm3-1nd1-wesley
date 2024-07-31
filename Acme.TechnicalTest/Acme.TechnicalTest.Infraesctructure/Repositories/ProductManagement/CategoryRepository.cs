using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;

namespace Acme.TechnicalTest.Infraesctructure.Repositories.ProductManagement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.context = appDbContext;
        }

        public void Add(Category category) => context.Categories.Add(category);

        public Task<bool> CategoryHasProducts(long id)
        {
            return context.Products.AnyAsync(x => x.CategoryId == id);
        }

        public void Delete(Category category) => context.Remove(category);

        public Task<Category?> Get(long id) => context.Categories.FirstOrDefaultAsync(x => x.Id == id);

        public Task<List<Category>> GetAllFromTenant(long? tenantId)
        {
            return context.Categories
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ToListAsync();
        }

        public Task<Category?> GetByReferenceSourceAndTenant(Guid reference, long tenantId)
        {
            return context.Categories
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.ReferenceSource == reference && x.TenantId == tenantId);
        }
    }
}

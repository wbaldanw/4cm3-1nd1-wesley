using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;

namespace Acme.TechnicalTest.Infraesctructure.Repositories.ProductManagement
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.context = appDbContext;
        }

        public void Add(Product Product) => context.Products.Add(Product);        

        public void Delete(Product Product) => context.Remove(Product);

        public Task<Product?> Get(long id) => context.Products.FirstOrDefaultAsync(x => x.Id == id);

        public Task<List<Product>> GetAllFromTenant(long? tenantId)
        {
            return context.Products
                .IgnoreQueryFilters()
                .Where(x => x.TenantId == tenantId)
                .ToListAsync();
        }
    }
}

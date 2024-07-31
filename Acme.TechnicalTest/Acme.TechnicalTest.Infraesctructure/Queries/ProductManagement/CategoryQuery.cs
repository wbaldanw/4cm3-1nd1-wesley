using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;
using Microsoft.EntityFrameworkCore;

namespace Acme.TechnicalTest.Infraesctructure.Queries.ProductManagement
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly AppDbContext context;

        public CategoryQuery(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<CategoryView?> Get(long id)
        {
            var entity = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return entity is null ? null : new CategoryView(entity);
        }

        public Task<List<CategoryView>> GetAll() => context.Categories
                .Select(c => new CategoryView(c))
                .AsNoTracking()
                .ToListAsync();
    }
}

using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.DTO.TenantManagement;
using Microsoft.EntityFrameworkCore;

namespace Acme.TechnicalTest.Infraesctructure.Queries.TenantManagement
{
    public class TenantQuery : ITenantQuery
    {
        private readonly AppDbContext context;

        public TenantQuery(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<TenantView?> Get(long id)
        {
            var tenant = await context.Tenants                
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);                

            return tenant is null ? null : new TenantView(tenant);
        }

        public Task<List<TenantView>> GetAll()
        {
            return context.Tenants
                .Select(t => new TenantView(t))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

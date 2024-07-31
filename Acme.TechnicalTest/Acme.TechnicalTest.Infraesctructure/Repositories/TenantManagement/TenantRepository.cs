using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.TenantManagement;
using Microsoft.EntityFrameworkCore;

namespace Acme.TechnicalTest.Infraesctructure.Repositories.TenantManagement
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext context;

        public TenantRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Tenant tenant) => context.Tenants.Add(tenant);

        public async Task DeleteAllTenantData(long tenantId)
        {
            await context.Database.ExecuteSqlAsync($@"
                DELETE FROM pm.Products WHERE TenantId = {tenantId};
                DELETE FROM pm.Categories WHERE TenantId = {tenantId};
                DELETE FROM tm.Tenants WHERE Id = {tenantId}");
        }

        public Task<Tenant?> GetById(long id) => 
            context.Tenants
            .Include(x => x.Parent)
            .FirstOrDefaultAsync(tenant => tenant.Id == id);

        public Task<List<Tenant>> GetChildrenTenant(long tenantId) => context.Tenants
                .Where(x => x.ParentId == tenantId)
                .ToListAsync();

        public Task<bool> HasTenatWithName(string name) => 
            context.Tenants.AnyAsync(tenant => tenant.Name == name);
    }
}

using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Acme.TechnicalTest.Infraesctructure.Queries.ProductManagement
{
    public class ProductQuery : IProductQuery
    {
        private readonly AppDbContext context;
        private readonly ILoggedUser loggedUser;
        private readonly string? connectionString;

        public ProductQuery(AppDbContext context,
                            ILoggedUser loggedUser,
                            IConfiguration configuration)
        {
            this.context = context;
            this.loggedUser = loggedUser;
            connectionString = configuration.GetConnectionString("SqlDb");
        }

        public async Task<ProductView?> Get(long id)
        {
            var entity = await context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return entity is null ? null : new ProductView(entity);
        }

        public async Task<List<ProductView>> GetAll() 
        {
            using var connection = new SqlConnection(connectionString);

            var sql = @"SELECT * FROM pm.Products p
                            LEFT JOIN pm.Categories c ON p.CategoryId = c.Id 
                            WHERE p.TenantId = @TenantId";

            var products = await connection.QueryAsync<ProductView, CategoryView, ProductView>(sql, (p, c) =>
            {
                p.Category = c;
                return p;
            }, new { loggedUser.TenantId });

            return products.ToList();            
        }
    }
}

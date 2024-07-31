using Acme.TechnicalTest.Domain.Contracts.UserManagement;
using Acme.TechnicalTest.Domain.Domain.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace Acme.TechnicalTest.Infraesctructure.Repositories.UserManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User?> GetByEmail(string email) =>
            await context.Users
            .Include(x => x.Tenant)
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }
}

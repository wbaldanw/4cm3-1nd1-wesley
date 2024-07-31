using Acme.TechnicalTest.Domain.Domain.UserManagement;

namespace Acme.TechnicalTest.Domain.Contracts.UserManagement
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
    }
}

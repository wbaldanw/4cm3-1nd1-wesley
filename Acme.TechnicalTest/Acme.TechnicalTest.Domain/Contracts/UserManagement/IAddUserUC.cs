using Acme.TechnicalTest.Domain.DTO.UserManagement;

namespace Acme.TechnicalTest.Domain.Contracts.UserManagement
{
    public interface IAddUserUC
    {
        Task Create(AddUserRequest dto);
    }
}

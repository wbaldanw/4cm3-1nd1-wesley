using Acme.TechnicalTest.Domain.DTO.UserManagement;

namespace Acme.TechnicalTest.Domain.Contracts.UserManagement
{
    public interface ISigninUC
    {
        Task<SignInResponse> SignIn(SignInRequest loginDTO);
    }
}

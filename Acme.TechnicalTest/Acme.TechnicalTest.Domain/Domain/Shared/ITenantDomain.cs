using Acme.TechnicalTest.Domain.Contracts.Shared;

namespace Acme.TechnicalTest.Domain.Domain.Shared
{
    public interface ITenantDomain
    {
        public long TenantId { get; }
        public void SetTenant(ILoggedUser loggedUser);
    }
}

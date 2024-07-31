using Acme.TechnicalTest.Domain.Contracts.Shared;

namespace Acme.TechnicalTest.Domain.Domain.Shared
{
    public abstract class TenantEntity : Entity, ITenantDomain, IReference
    {
        protected TenantEntity()
        {
            Reference = Guid.NewGuid();
        }

        public Guid Reference { get; private set; }
        public long TenantId { get; protected set; }
        public void SetTenant(ILoggedUser loggedUser)
        {
            if (TenantId != 0)
                return;

            TenantId = loggedUser.TenantId;
        }        
    }
}

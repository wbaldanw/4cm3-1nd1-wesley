using Acme.TechnicalTest.Domain.Domain.TenantManagement;

namespace Acme.TechnicalTest.Domain.Domain.Shared
{
    public abstract class CloneableTenant : TenantEntity
    {
        public Guid? ReferenceSource { get; protected set; }
        public void SetReferenceSource(IReference referenceSource, Tenant tenant)
        {
            ReferenceSource = referenceSource.Reference;
            this.TenantId = tenant.Id;
        }
        
    }
}

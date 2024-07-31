using MediatR;

namespace Acme.TechnicalTest.Domain.Domain.TenantManagement.Events
{
    public class TenantCreatedEvent : IDomainEvent, INotification
    {
        private readonly Tenant tenant;

        public TenantCreatedEvent(Tenant tenant)
        {
            this.tenant = tenant;
        }

        public long TenantId => tenant.Id;
    }
}

using MediatR;

namespace Acme.TechnicalTest.Domain.Domain.TenantManagement.Events
{
    public class TenantDeletedEvent : IDomainEvent, INotification
    {
        public TenantDeletedEvent(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}

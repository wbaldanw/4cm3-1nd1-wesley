using Acme.TechnicalTest.Domain.Domain.TenantManagement.Events;
using MediatR;

namespace Acme.TechnicalTest.Application.EventHandlers.TenantManagement
{
    public class DeleteHierarchicalDataHandler : INotificationHandler<TenantDeletedEvent>
    {
        public Task Handle(TenantDeletedEvent notification, CancellationToken cancellationToken)
        {
            //TODO: NOT IMPLEMENTED DUE THE DOUBTS. CHECK THE DOCUMENTATION.
            return Task.CompletedTask;
        }
    }
}

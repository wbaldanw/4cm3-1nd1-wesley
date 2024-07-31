using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.TenantManagement.Events;
using Hangfire;
using MediatR;

namespace Acme.TechnicalTest.Application.EventHandlers.TenantManagement
{
    public class ApplyHardDeletionForTenantHandler : INotificationHandler<TenantDeletedEvent>
    {
        public Task Handle(TenantDeletedEvent notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue<ITenantRepository>(x => x.DeleteAllTenantData(notification.Id));
            return Task.CompletedTask;
        }
    }    
}

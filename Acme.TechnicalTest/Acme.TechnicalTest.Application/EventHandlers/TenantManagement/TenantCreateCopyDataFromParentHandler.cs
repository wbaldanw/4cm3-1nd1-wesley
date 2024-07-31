using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.TenantManagement.Events;
using Hangfire;
using MediatR;

namespace Acme.TechnicalTest.Application.EventHandlers.TenantManagement
{
    public class TenantCreateCopyDataFromParentHandler : INotificationHandler<TenantCreatedEvent>
    {
        public Task Handle(TenantCreatedEvent notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue<ICopyDataFromParentTenantUC>(c => c.CopyData(notification.TenantId));
            return Task.CompletedTask;
        }
    }
}

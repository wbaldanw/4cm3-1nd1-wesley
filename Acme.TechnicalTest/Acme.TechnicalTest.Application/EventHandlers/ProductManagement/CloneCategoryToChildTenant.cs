using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Domain.ProductManagement.Events;
using Hangfire;
using MediatR;

namespace Acme.TechnicalTest.Application.EventHandlers.ProductManagement
{
    public class CloneCategoryToChildTenant : INotificationHandler<CategoryCreatedEvent>
    {
        public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue<ICloneCategoryToChildTenantUC>(c => c.CloneCategory(notification.Category));
            return Task.CompletedTask;
        }
    }
}

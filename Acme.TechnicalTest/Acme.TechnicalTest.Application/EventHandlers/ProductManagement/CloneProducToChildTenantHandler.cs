using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Domain.ProductManagement.Events;
using Hangfire;
using MediatR;

namespace Acme.TechnicalTest.Application.EventHandlers.ProductManagement
{
    public class CloneProducToChildTenantHandler : INotificationHandler<ProductCreatedEvent>
    {
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue<ICloneProductToChildTenantUC>(x => x.CloneProduct(notification.Product));
            return Task.CompletedTask;
        }
    }
}

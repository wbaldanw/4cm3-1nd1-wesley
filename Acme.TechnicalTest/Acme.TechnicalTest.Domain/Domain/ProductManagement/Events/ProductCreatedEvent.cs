using Acme.TechnicalTest.Domain.DTO.ProductManagement;
using MediatR;

namespace Acme.TechnicalTest.Domain.Domain.ProductManagement.Events
{
    public class ProductCreatedEvent: IDomainEvent, INotification
    {
        private Product product;

        public ProductCreatedEvent(Product product)
        {
            this.product = product;
        }

        public ProductCreatedMessage Product { get => new ProductCreatedMessage(product); }
        }
}

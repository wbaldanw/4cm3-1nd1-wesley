using MediatR;

namespace Acme.TechnicalTest.Domain.Domain.ProductManagement.Events
{
    public class ProductDeleted : IDomainEvent, INotification
    {
        public ProductDeleted(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}

using Acme.TechnicalTest.Domain.DTO.ProductManagement;
using MediatR;

namespace Acme.TechnicalTest.Domain.Domain.ProductManagement.Events
{
    public class CategoryCreatedEvent: IDomainEvent, INotification
    {
        private Category category;

        public CategoryCreatedEvent(Category category)
        {
            this.category = category;
        }        

        public CategoryCreatedMessage Category => new CategoryCreatedMessage(category);
    }

    public class CategoryDeleted : IDomainEvent, INotification
    {
        public CategoryDeleted(long id)
        {
            Id = id;
        }

        public long Id { get; }
    }
}

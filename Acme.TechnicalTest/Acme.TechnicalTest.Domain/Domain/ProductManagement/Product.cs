using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Domain.ProductManagement.Events;

namespace Acme.TechnicalTest.Domain.Domain.ProductManagement
{
    public class Product : CloneableTenant 
    {
        private Product() { }

        public Product(string name) : base()
        {
            var invalidException = new InvalidFieldsException();

            invalidException.AddInvalidFields(ValidateName(name));

            if (invalidException.HasInvalidFields)
                throw invalidException;

            this.name = name;

            AddDomainEvent(new ProductCreatedEvent(this));
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                var ex = new InvalidFieldsException(ValidateName(value));

                if (ex.HasInvalidFields)
                    throw ex;

                name = value;
            }
        }

        public Category? Category { get; set; }
        public long? CategoryId { get; private set; }

        public string? Description { get; set; }

        private IEnumerable<InvalidField> ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                yield return new InvalidField(nameof(name), "Name is required.");
        }

        public void MarkAsDelete() => AddDomainEvent(new ProductDeleted(this.Id));
    }
}

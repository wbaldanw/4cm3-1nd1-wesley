using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Domain.ProductManagement.Events;

namespace Acme.TechnicalTest.Domain.Domain.ProductManagement
{
    public class Category : CloneableTenant
    {
        private Category() { }

        public Category(string name) : base()
        {   
            var invalidException = new InvalidFieldsException();

            invalidException.AddInvalidFields(ValidateName(name));

            if (invalidException.HasInvalidFields)
                throw invalidException;

            this.name = name;

            this.AddDomainEvent(new CategoryCreatedEvent(this));
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

        private IEnumerable<InvalidField> ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                yield return new InvalidField(nameof(name), "Name is required.");
        }

        public void MarkAsDelete()
        {
            AddDomainEvent(new CategoryDeleted(this.Id));
        }
    }
}

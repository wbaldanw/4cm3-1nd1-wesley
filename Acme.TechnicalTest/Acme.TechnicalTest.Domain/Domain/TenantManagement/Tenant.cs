using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Domain.TenantManagement.Events;

namespace Acme.TechnicalTest.Domain.Domain.TenantManagement
{
    public class Tenant : Entity
    {
        public static Tenant CreateAcmeTenant()
        {
            return new Tenant("Acme");
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidFieldsException(nameof(name), "Name is required.");

            if (name == "Acme")
                throw new InvalidFieldsException(nameof(name), "Name cannot be 'Acme'.");

            Name = name;
        }

        private Tenant(string name)
        {
            Name = name;

            if (name != "Acme")
                AddDomainEvent(new TenantCreatedEvent(this));
        }

        public Tenant(string name, Tenant parent) : this(name)
        {
            var invalidFields = new InvalidFieldsException();

            if (string.IsNullOrEmpty(name))
                invalidFields.AddInvalidField(nameof(name), "Name is required.");

            if (parent is null)
                invalidFields.AddInvalidField(nameof(parent), "Parent is required.");

            if (invalidFields.HasInvalidFields)
                throw invalidFields;

            Name = name;
            Parent = parent;
        }

        private Tenant() { }

        public string Name { get; private set; }

        public Tenant? Parent { get; private set; }
        public long? ParentId { get; private set; }   
        public bool MarkedAsDeleted { get; private set; }

        private List<Tenant> children = new List<Tenant>();
        public IReadOnlyCollection<Tenant> Children
        {
            get => children;
            private set => children = value.ToList();
        }

        public void MarkAsDeleted()
        {
            this.AddDomainEvent(new TenantDeletedEvent(this.Id));
            MarkedAsDeleted = true;
        }
    }
}

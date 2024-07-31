using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Domain.Shared;
using Acme.TechnicalTest.Domain.Domain.TenantManagement;
using Microsoft.AspNetCore.Identity;

namespace Acme.TechnicalTest.Domain.Domain.UserManagement
{
    public class User : IdentityUser<long>
    {
        public User(Email email, string fullName, Tenant tenant)
        {
            ValidateFields(email, fullName);

            Email = email;
            UserName = email;
            FullName = fullName;
            Tenant = tenant;
        }

        private User() { }

        private void ValidateFields(Email email, string fullName)
        {
            var exception = new InvalidFieldsException();

            if (email is null)
                exception.AddInvalidField(nameof(email), "Email is required.");

            if (fullName.Length < 3)
                exception.AddInvalidField(nameof(fullName), "Full name must have at least 3 characters.");

            if (string.IsNullOrEmpty(fullName))
                exception.AddInvalidField(nameof(fullName), "Full name is required.");

            if (exception.HasInvalidFields)
                throw exception;
        }

        public string FullName { get; private set; }
        public Tenant Tenant { get; private set; }
        public long TenantId { get; private set; }
    }
}

using System.Text.RegularExpressions;

namespace Acme.TechnicalTest.Domain.Domain.Shared
{
    public class Email : SimpleValueObject<string>
    {
        private Email() : base("") { }

        public Email(string value) : base(value)
        {
            if (!IsValid(value))
                throw new ArgumentException("Email is not valid");
        }

        public static bool IsValid(string email)
        {
            string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            Regex regex = new Regex(emailRegex);
            return regex.IsMatch(email);
        }

        public static implicit operator Email(string value) => new Email(value);
    }
}

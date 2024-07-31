namespace Acme.TechnicalTest.Domain.Contracts.Shared
{
    public interface ILoggedUser
    {
        public long UserId { get; }
        public long TenantId { get; }
    }
}

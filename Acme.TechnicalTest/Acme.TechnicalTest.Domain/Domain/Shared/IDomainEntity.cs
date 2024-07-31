namespace Acme.TechnicalTest.Domain.Domain.Shared
{
    public interface IDomainEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        void ClearEvents();
    }
}

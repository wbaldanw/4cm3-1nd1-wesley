namespace Acme.TechnicalTest.Domain.DTO
{
    [Serializable]
    public abstract class CloneableMessage : IReference
    {
        protected CloneableMessage() { }

        protected CloneableMessage(CloneableTenant sourceDomain)
        {
            Reference = sourceDomain.Reference;
            if (sourceDomain.ReferenceSource.HasValue)
                Reference = sourceDomain.ReferenceSource.Value;

            TenantId = sourceDomain.TenantId;
        }

        public Guid Reference { get; set; }
        public long TenantId { get; set; }
    }
}

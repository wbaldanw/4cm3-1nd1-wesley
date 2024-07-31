namespace Acme.TechnicalTest.Domain.DTO.TenantManagement
{
    public class AddTenantRequest
    {
        public required string Name { get; set; }
        public long ParentId { get; set; }
    }
}

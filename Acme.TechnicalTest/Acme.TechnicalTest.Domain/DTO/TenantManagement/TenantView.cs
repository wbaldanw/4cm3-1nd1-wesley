using Acme.TechnicalTest.Domain.Domain.TenantManagement;

namespace Acme.TechnicalTest.Domain.DTO.TenantManagement
{
    public class TenantView
    {
        public TenantView(Tenant tenant)
        {
            Id = tenant.Id;
            Name = tenant.Name;
        }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}

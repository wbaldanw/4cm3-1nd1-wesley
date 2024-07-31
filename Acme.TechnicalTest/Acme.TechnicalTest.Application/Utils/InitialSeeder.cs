using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Contracts.UserManagement;
using Acme.TechnicalTest.Domain.DTO.TenantManagement;
using Acme.TechnicalTest.Domain.DTO.UserManagement;

namespace Acme.TechnicalTest.Application.Utils
{
    public class InitialSeeder
    {
        private readonly IAddUserUC addUserUC;
        private readonly IAddTenantUC addTenantUC;
        private readonly ITenantRepository tenantRepository;

        public InitialSeeder(IAddUserUC addUserUC,
                             IAddTenantUC addTenantUC,
                             ITenantRepository tenantRepository)
        {
            this.addUserUC = addUserUC;
            this.addTenantUC = addTenantUC;
            this.tenantRepository = tenantRepository;
        }

        public async Task Seed()
        {            
            if (await tenantRepository.HasTenatWithName("Acme"))
                return;

            var tenantId = await addTenantUC.Create(new AddTenantRequest() { Name = "Acme" });
            await addUserUC.Create(new AddUserRequest()
            {
                Email = "admin@email.com",
                Password = "Admin!",
                FullName = "Admin",
                TenantId = tenantId
            });
        }
    }
}

using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.DTO.TenantManagement;

namespace Acme.TechnicalTest.Application.UseCases.TenantManagement
{
    public class UpdateTenantUC : IUpdateTenantUC
    {
        private readonly ITenantRepository tenantRepository;
        private readonly IUnitOfWork uow;

        public UpdateTenantUC(
            ITenantRepository tenantRepository,
            IUnitOfWork uow)
        {
            this.tenantRepository = tenantRepository;
            this.uow = uow;
        }

        public async Task Update(long id, UpdateTenantRequest request)
        {
            var tenant = await tenantRepository.GetById(id);

            if (tenant is null)
                throw new EntityNotFoundException($"Tenant with id {id} not found");

            tenant.UpdateName(request.Name);
            await uow.SaveChangesAsync();
        }
    }
}

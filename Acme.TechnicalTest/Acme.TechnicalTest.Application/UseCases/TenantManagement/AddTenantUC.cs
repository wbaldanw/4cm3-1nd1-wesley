using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.TenantManagement;
using Acme.TechnicalTest.Domain.DTO.TenantManagement;

namespace Acme.TechnicalTest.Application.UseCases.TenantManagement
{
    public class AddTenantUC : IAddTenantUC
    {
        private readonly ITenantRepository tenantRepository;
        private readonly IUnitOfWork uow;

        public AddTenantUC(
            ITenantRepository tenantRepository, 
            IUnitOfWork uow)
        {
            this.tenantRepository = tenantRepository;
            this.uow = uow;
        }

        public async Task<long> Create(AddTenantRequest request)
        {
            Tenant tenant;
            if (request.Name == "Acme")
                tenant = Tenant.CreateAcmeTenant();
            else
                tenant = await CreateComonTenant(request);

            tenantRepository.Add(tenant);
            await uow.SaveChangesAsync();

            return tenant.Id;
        }

        private async Task<Tenant> CreateComonTenant(AddTenantRequest request)
        {
            var parentTenant = await tenantRepository.GetById(request.ParentId);
            if (parentTenant is null)
                throw new DomainException("Parent Tenant not found.");

            var tenant = new Tenant(request.Name, parentTenant);
            return tenant;
        }
    }

    public class DeleteTenantUC : IDeleteTenantUC
    {
        private readonly ITenantRepository tenantRepository;
        private readonly IUnitOfWork uow;

        public DeleteTenantUC(
                       ITenantRepository tenantRepository, 
                                  IUnitOfWork uow)
        {
            this.tenantRepository = tenantRepository;
            this.uow = uow;
        }

        public async Task Delete(long id)
        {
            var tenant = await tenantRepository.GetById(id);
            if (tenant is null)
                throw new EntityNotFoundException("Tenant not found.");

            var childrenTenant = await tenantRepository.GetChildrenTenant(id);
            if (childrenTenant.Any())
                throw new DomainException("Tenant has children.");

            tenant.MarkAsDeleted();            
            await uow.SaveChangesAsync();
        }
    }
}

using Acme.TechnicalTest.Domain.Contracts.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.Shared;
using Acme.TechnicalTest.Domain.DTO.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acme.TechnicalTest.Api.Controllers.TenantManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = PolicyNames.TenantManagementPolicy)]
    public class TenantController : ControllerBase
    {
        private readonly IAddTenantUC addTenantUC;
        private readonly IUpdateTenantUC updateTenantUC;
        private readonly ITenantQuery tenantQuery;
        private readonly IDeleteTenantUC deleteTenantUC;

        public TenantController(IAddTenantUC addTenantUC,
                                IUpdateTenantUC updateTenantUC,
                                IDeleteTenantUC deleteTenantUC,
                                ITenantQuery tenantQuery)
        {
            this.addTenantUC = addTenantUC;
            this.updateTenantUC = updateTenantUC;
            this.deleteTenantUC = deleteTenantUC;
            this.tenantQuery = tenantQuery;
        }

        [HttpPost]
        public async Task<ActionResult> AddTenant([FromBody] AddTenantRequest request)
        {
            await addTenantUC.Create(request);

            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateTenant([FromRoute]long id, [FromBody]UpdateTenantRequest request)
        {
            await updateTenantUC.Update(id, request);
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TenantView>> GetTenant([FromRoute]long id)
        {
            var tenant = await tenantQuery.Get(id);
            return Ok(tenant);
        }

        [HttpGet]
        public async Task<ActionResult<List<TenantView>>> GetAll()
        {
            var tenants = await tenantQuery.GetAll();
            return Ok(tenants);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteTenant([FromRoute]long id)
        {
            await deleteTenantUC.Delete(id);
            return Accepted();            
        }
    }
}

using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Acme.TechnicalTest.Application.Utils
{
    public class LoggedUserDataInfo : ILoggedUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoggedUserDataInfo(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public long UserId 
        {
            get 
            {
                long userId;

                if (!long.TryParse(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value, out userId))
                    throw new UnauthorizedAccessException("User is not authorized");

                return userId;
            }
        }

        public long TenantId
        {
            get
            {
                long tenantId;

                long.TryParse(httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(x => x.Type == CustomClaimTypes.TenantId)?.Value, out tenantId);

                return tenantId;
            }
        }
    }
}

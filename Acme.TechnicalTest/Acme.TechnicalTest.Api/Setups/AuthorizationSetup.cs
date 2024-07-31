using static Acme.TechnicalTest.Domain.Domain.Shared.ClaimsForPolicy;
using static Acme.TechnicalTest.Domain.Domain.Shared.PolicyNames;

namespace Acme.TechnicalTest.Api.Setups
{
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationWithPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(TenantManagementPolicy,
                    policy =>
                    {
                        policy.RequireClaim(TenantManagement, "writer");
                    });
                });
        }
    }
}

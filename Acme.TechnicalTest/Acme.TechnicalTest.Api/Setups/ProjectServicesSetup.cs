using Acme.TechnicalTest.Application.UseCases.UserManagement;
using Acme.TechnicalTest.Application.Utils;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Infraesctructure.Repositories.UserManagement;

namespace Acme.TechnicalTest.Api.StartUps
{
    public static class ProjectServicesSetup
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services) 
        {
            services.Scan(scan => scan
                           .FromAssemblyOf<AddUserUC>()
                           .AddClasses()
                           .AsImplementedInterfaces()
                           .WithScopedLifetime());

            services.Scan(scan => scan
                           .FromAssemblyOf<UserRepository>()
                           .AddClasses()
                           .AsImplementedInterfaces()
                           .WithScopedLifetime());

            services.AddScoped<InitialSeeder>();
            services.AddScoped<ILoggedUser, LoggedUserDataInfo>();

            return services;
        }
    }
}

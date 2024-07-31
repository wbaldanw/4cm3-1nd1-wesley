using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Acme.TechnicalTest.Domain.Domain.Shared;
using Acme.TechnicalTest.Domain.Domain.TenantManagement;
using Acme.TechnicalTest.Domain.Domain.UserManagement;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Acme.TechnicalTest.Infraesctructure
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<long>, long,
        IdentityUserClaim<long>, IdentityUserRole<long>, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        private readonly IMediator mediator;
        private readonly ILoggedUser loggedUser;

        public AppDbContext(DbContextOptions<AppDbContext> options,
                            IMediator mediator,
                            ILoggedUser loggedUser) : base(options)
        {
            this.mediator = mediator;
            this.loggedUser = loggedUser;
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTenant();

            var events = ChangeTracker.Entries<IDomainEntity>().Where(x => x.State == EntityState.Deleted &&
                    typeof(IDomainEntity).IsAssignableFrom(x.Entity.GetType()))
                .Select(x => x.Entity).ToList();

            var changesAffected = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return changesAffected;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            SetupAspNetIdentityTables(builder);

            builder.SetQueryFilterOnAllEntities<ITenantDomain>(x => (x.TenantId == loggedUser.TenantId));
        }

        private void SetupAspNetIdentityTables(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<long>>().ToTable("Roles", "um");
            builder.Entity<IdentityUserRole<long>>().ToTable("UserRoles", "um");
            builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims", "um");
            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins", "um");
            builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens", "um");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims", "um");
        }

        private void SetTenant()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is ITenantDomain).ToList();            
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {   
                    if (entry.Entity is ITenantDomain)
                    {
                        ((ITenantDomain)entry.Entity).SetTenant(loggedUser);
                        continue;
                    }                    
                }
            }
        }

        private async Task DispatchEvents(List<IDomainEntity> deletedDomain)
        {   
            var domainEvents = ChangeTracker.Entries<IDomainEntity>()
                .Where(x => typeof(IDomainEntity).IsAssignableFrom(x.Entity.GetType())).Select(x => x.Entity).ToList();

            domainEvents.AddRange(deletedDomain);

            var events = new List<IDomainEvent>();
            foreach (var domainEvent in domainEvents)
            {
                events.AddRange(domainEvent.DomainEvents);
                domainEvent.ClearEvents();
            }

            foreach (var ev in events)
                await mediator.Publish(ev);
        }
    }
}

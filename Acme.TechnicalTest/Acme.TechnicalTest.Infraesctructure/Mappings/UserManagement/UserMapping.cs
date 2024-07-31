using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Acme.TechnicalTest.Domain.Domain.UserManagement;

namespace Acme.TechnicalTest.Infraesctructure.Mappings.UserManagement
{
    internal class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "um");            

            builder.Property(x => x.FullName)
                .HasMaxLength(350)
                .IsRequired();

            builder.HasOne(x => x.Tenant)
                .WithMany()
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acme.TechnicalTest.Infraesctructure.Mappings.ProductManagement
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "pm");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        }
    }
}

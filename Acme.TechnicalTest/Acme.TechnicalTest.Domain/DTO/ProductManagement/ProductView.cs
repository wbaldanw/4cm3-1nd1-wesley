using Acme.TechnicalTest.Domain.Domain.ProductManagement;

namespace Acme.TechnicalTest.Domain.DTO.ProductManagement
{

    public class ProductView
    {
        public ProductView() { }

        public ProductView(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            CategoryId = product.CategoryId;

            if (product.Category is not null)
                Category = new CategoryView(product.Category);
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long? CategoryId { get; set; }
        public CategoryView? Category { get; set; }
    }
}

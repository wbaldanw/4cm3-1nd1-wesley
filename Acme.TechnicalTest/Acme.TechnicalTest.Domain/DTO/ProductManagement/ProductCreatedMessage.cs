using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using System.Xml.Linq;

namespace Acme.TechnicalTest.Domain.DTO.ProductManagement
{
    [Serializable]
    public class ProductCreatedMessage: CloneableMessage
    {
        public ProductCreatedMessage() { }

        public ProductCreatedMessage(Product product) : base(product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;

            if (product.Category is not null) 
            {
                CategoryReference = product.Category.ReferenceSource.HasValue ? 
                    product.Category.ReferenceSource : product.Category?.Reference;
                CategoryName = product.Category?.Name;                
            }
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public Guid? CategoryReference { get; set; }
    }
}

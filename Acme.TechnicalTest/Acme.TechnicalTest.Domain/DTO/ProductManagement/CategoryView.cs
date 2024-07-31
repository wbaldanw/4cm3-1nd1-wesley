using Acme.TechnicalTest.Domain.Domain.ProductManagement;

namespace Acme.TechnicalTest.Domain.DTO.ProductManagement
{    
    public class CategoryView 
    {
        public CategoryView() { }

        public CategoryView(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}

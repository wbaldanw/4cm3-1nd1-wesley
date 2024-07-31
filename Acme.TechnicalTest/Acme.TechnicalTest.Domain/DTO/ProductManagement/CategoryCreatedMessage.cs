using Acme.TechnicalTest.Domain.Domain.ProductManagement;

namespace Acme.TechnicalTest.Domain.DTO.ProductManagement
{
    [Serializable]
    public class CategoryCreatedMessage: CloneableMessage
    {
        public CategoryCreatedMessage() 
        { }

        public CategoryCreatedMessage(Category category) : base(category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public long Id { get; set; }
        public string Name { get; set; }
    }
}

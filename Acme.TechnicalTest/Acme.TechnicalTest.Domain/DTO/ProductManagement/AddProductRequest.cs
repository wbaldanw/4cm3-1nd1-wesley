namespace Acme.TechnicalTest.Domain.DTO.ProductManagement
{
    public class AddProductRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public long? CategoryId { get; set; }
    }
}

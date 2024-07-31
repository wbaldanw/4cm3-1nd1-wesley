using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Application.UseCases.ProductManagement
{
    public class AddProductUC : IAddProductUC
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork uow;

        public AddProductUC(IProductRepository productRepository,
                            ICategoryRepository categoryRepository,
                            IUnitOfWork uow)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.uow = uow;
        }

        public async Task<long> Add(AddProductRequest request)
        {
            var product = new Product(request.Name);
            product.Description = request.Description;

            if (request.CategoryId.HasValue)
            {
                var category = await categoryRepository.Get(request.CategoryId.Value);
                if (category is null)
                    throw new EntityNotFoundException($"Category with id {request.CategoryId} not found.");

                product.Category = category;
            }
            
            productRepository.Add(product);
            await uow.SaveChangesAsync();

            return product.Id;
        }
    }
}

using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Application.UseCases.ProductManagement
{
    public class UpdateProductUC : IUpdateProductUC
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork uow;

        public UpdateProductUC(IProductRepository productRepository,
                               ICategoryRepository categoryRepository,
                               IUnitOfWork uow)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.uow = uow;
        }

        public async Task Update(long id, UpdateProductRequest request)
        {
            var product = await productRepository.Get(id);
            if (product is null)
                throw new EntityNotFoundException($"Product with id {id} not found.");

            product.Name = request.Name;
            product.Description = request.Description;

            if (request.CategoryId.HasValue)
            {
                var category = await categoryRepository.Get(request.CategoryId.Value);
                if (category is null)
                    throw new EntityNotFoundException($"Category with id {request.CategoryId} not found.");

                product.Category = category;
            }

            await uow.SaveChangesAsync();
        }
    }
}

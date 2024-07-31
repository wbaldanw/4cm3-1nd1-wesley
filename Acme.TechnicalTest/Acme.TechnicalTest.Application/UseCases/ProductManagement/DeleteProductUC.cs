using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;

namespace Acme.TechnicalTest.Application.UseCases.ProductManagement
{
    public class DeleteProductUC: IDeleteProductUC
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork uow;

        public DeleteProductUC(IProductRepository productRepository, IUnitOfWork uow)
        {
            this.productRepository = productRepository;
            this.uow = uow;
        }

        public async Task Delete(long id)
        {
            var product = await productRepository.Get(id);
            if (product is null)
                throw new EntityNotFoundException($"Product with id {id} not found.");

            product.MarkAsDelete();

            productRepository.Delete(product);
            await uow.SaveChangesAsync();
        }
    }
}

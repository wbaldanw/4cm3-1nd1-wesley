using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;

namespace Acme.TechnicalTest.Application.UseCases.ProductManagement
{
    public class DeleteCategoryUC: IDeleteCategoryUC
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork uow;

        public DeleteCategoryUC(ICategoryRepository categoryRepository, IUnitOfWork uow)
        {
            this.categoryRepository = categoryRepository;
            this.uow = uow;
        }

        public async Task Delete(long id)
        {
            if (await categoryRepository.CategoryHasProducts(id))
                throw new DomainException("Category has products associated.");

            var category = await categoryRepository.Get(id);
            if (category is null)
                throw new EntityNotFoundException($"Category with id {id} not found.");

            category.MarkAsDelete();

            categoryRepository.Delete(category);
            await uow.SaveChangesAsync();
        }
    }
}

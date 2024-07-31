using Acme.Core.Exceptions;
using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;

namespace Acme.TechnicalTest.Application.UseCases.ProductManagement
{
    public class UpdateCategoryUC : IUpdateCategoryUC
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork uow;

        public UpdateCategoryUC(ICategoryRepository categoryRepository, IUnitOfWork uow)
        {
            this.categoryRepository = categoryRepository;
            this.uow = uow;
        }

        public async Task Update(long id, UpdateCategoryRequest request)
        {
            var category = await categoryRepository.Get(id);
            if (category is null)
                throw new EntityNotFoundException($"Category with id {id} not found.");

            category.Name = request.Name;
            await uow.SaveChangesAsync();
        }
    }
}

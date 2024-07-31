using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.Contracts.Shared;
using Acme.TechnicalTest.Domain.Domain.ProductManagement;
using Acme.TechnicalTest.Domain.Domain.TenantManagement;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;
using MediatR;

namespace Acme.TechnicalTest.Application.UseCases.ProductManagement
{
    public class AddCategoryUC : IAddCategoryUC
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork uow;

        public AddCategoryUC(ICategoryRepository categoryRepository, IUnitOfWork uow)
        {
            this.categoryRepository = categoryRepository;
            this.uow = uow;
        }

        public async Task<long> Add(AddCategoryRequest request)
        {
            var category = new Category(request.Name);
            
            categoryRepository.Add(category);
            await uow.SaveChangesAsync();

            return category.Id;
        }        
    }
}

namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface IDeleteCategoryUC
    {
        Task Delete(long id);
    }
}

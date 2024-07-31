namespace Acme.TechnicalTest.Domain.Contracts.ProductManagement
{
    public interface IDeleteProductUC
    {
        Task Delete(long id);
    }
}

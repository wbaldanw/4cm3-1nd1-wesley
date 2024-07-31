using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;
using Microsoft.AspNetCore.Mvc;

namespace Acme.TechnicalTest.Api.Controllers.ProductManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAddProductUC addProductUC;
        private readonly IUpdateProductUC updateProductUC;
        private readonly IDeleteProductUC deleteProductUC;
        private readonly IProductQuery ProductQuery;

        public ProductController(IAddProductUC addProductUC,
                                  IUpdateProductUC updateProductUC,
                                  IDeleteProductUC deleteProductUC,
                                  IProductQuery ProductQuery)
        {
            this.addProductUC = addProductUC;
            this.updateProductUC = updateProductUC;
            this.deleteProductUC = deleteProductUC;
            this.ProductQuery = ProductQuery;
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            await addProductUC.Add(request);
            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateProduct([FromRoute]long id, [FromBody] UpdateProductRequest request)
        {
            await updateProductUC.Update(id, request);
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductView>> GetProduct([FromRoute]long id)
        {
            var Product = await ProductQuery.Get(id);
            return Ok(Product);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductView>>> GetAll()
        {
            var categories = await ProductQuery.GetAll();
            return Ok(categories);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute]long id)
        {
            await deleteProductUC.Delete(id);
            return NoContent();
        }
    }
}

using Acme.TechnicalTest.Domain.Contracts.ProductManagement;
using Acme.TechnicalTest.Domain.DTO.ProductManagement;
using Microsoft.AspNetCore.Mvc;

namespace Acme.TechnicalTest.Api.Controllers.ProductManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IAddCategoryUC addCategoryUC;
        private readonly IUpdateCategoryUC updateCategoryUC;
        private readonly IDeleteCategoryUC deleteCategoryUC;
        private readonly ICategoryQuery categoryQuery;

        public CategoryController(IAddCategoryUC addCategoryUC,
                                  IUpdateCategoryUC updateCategoryUC,
                                  IDeleteCategoryUC deleteCategoryUC,
                                  ICategoryQuery categoryQuery)
        {
            this.addCategoryUC = addCategoryUC;
            this.updateCategoryUC = updateCategoryUC;
            this.deleteCategoryUC = deleteCategoryUC;
            this.categoryQuery = categoryQuery;
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] AddCategoryRequest request)
        {
            await addCategoryUC.Add(request);
            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateCategory([FromRoute]long id, [FromBody] UpdateCategoryRequest request)
        {
            await updateCategoryUC.Update(id, request);
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryView>> GetCategory([FromRoute]long id)
        {
            var category = await categoryQuery.Get(id);
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryView>>> GetAll()
        {
            var categories = await categoryQuery.GetAll();
            return Ok(categories);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCategory([FromRoute]long id)
        {
            await deleteCategoryUC.Delete(id);
            return NoContent();
        }
    }
}

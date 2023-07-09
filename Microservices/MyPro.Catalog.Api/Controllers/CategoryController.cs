using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPro.Catalog.Api.DbContext;
using MyPro.Catalog.Api.Entities;

namespace MyPro.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICatalogDbContext dbContext;

        public CategoryController(ICatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var categories = this.dbContext.Categories;

            if (!categories.Any())
                return NotFound();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] int id)
        {
            var category = this.dbContext.Categories.FirstOrDefault(_ => _.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name)
        {
            this.dbContext.Categories.Add(new Category
            {
                Name = name
            });

            await this.dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            return Ok();
        }
    }
}


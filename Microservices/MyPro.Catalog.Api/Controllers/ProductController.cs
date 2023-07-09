using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPro.Catalog.Api.DbContext;

namespace MyPro.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICatalogDbContext dbContext;

        public ProductController(ICatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(dbContext.Products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(this.dbContext.Products.FirstOrDefault(_ => _.Id == id));
        }

        public record NewProduct(
            [Required]
            string name,
            [Required]
            string description,
            [Required]
            int category,
            [Required]
            decimal price);

        [HttpPost]
        public IActionResult Post([FromBody] NewProduct newProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyPro.Cart.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MonitorController : ControllerBase
    {
        [HttpGet(nameof(Test))]
        public IActionResult Test()
        {
            return new JsonResult(true);
        }
    }
}


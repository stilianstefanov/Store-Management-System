using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController() 
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test Get");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Test Post");
        }
    }
}

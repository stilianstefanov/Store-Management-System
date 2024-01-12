using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        public WarehousesController()
        {
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from WarehouseService");
        }
    }
}

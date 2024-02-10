using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    [Route("api/borrowers/{borrowerId}/purchases/{purchaseId}/[controller]")]
    [ApiController]
    public class PurchaseProductsController : ControllerBase
    {
    }
}

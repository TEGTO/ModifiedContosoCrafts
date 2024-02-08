using Microsoft.AspNetCore.Mvc;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private JsonFileProductService productService;

        public ProductsController(JsonFileProductService productService)
        {
            this.productService = productService;
        }
		[HttpGet]
        public IEnumerable<Product> Get()
        {
            return productService.GetProducts();
        }
        [Route("rate")]
        [HttpPost]
        public ActionResult Get([FromQuery] string productId, [FromQuery] float rating)
        {
            productService.AddRatings(productId, rating);
            return Ok();
        }
    }
}

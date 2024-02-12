using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebContosoCrafts.WebSiteSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return productService.GetProducts();
        }
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProductById(string productId)
        {
            Product requstedProduct = productService.GetProductById(productId);
            return requstedProduct == null ? NotFound() : requstedProduct;
        }
        [HttpPost("rate")]
        public ActionResult PostProductRating([FromQuery] string productId, [FromQuery] float rating)
        {
            if (productService.GetProductById(productId) == null)
                return NotFound();
            productService.AddRatings(productId, rating);
            return Ok();
        }
        [HttpPut("create")]
        public ActionResult<Product> PutNewProduct()
        {
            Product product = productService.CreateNewProduct();
            return CreatedAtAction(nameof(GetProducts), new { productId = product.Id }, product);
        }
        [HttpDelete("delete/{productId}")]
        public ActionResult DeleteProduct(string productId)
        {
            productService.DeleteProductById(productId);
            return Ok();
        }
    }
}

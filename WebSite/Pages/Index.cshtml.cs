using Microsoft.AspNetCore.Mvc.RazorPages;
using WebSite.Models;
using WebSite.Services;

namespace WebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private JsonFileProductService productService;

        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, JsonFileProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }

        public void OnGet()
        {
            Products = productService.GetProducts();
        }
    }
}
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IProductService productService;

        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IProductService productService)
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
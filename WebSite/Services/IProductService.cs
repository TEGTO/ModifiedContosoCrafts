using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.Services
{
	public interface IProductService
	{
		public void AddRatings(string productId, float rating);
		public IEnumerable<Product> GetProducts();
		public Product GetProductById(string productsId);
		public Product CreateNewProduct();
		public void DeleteProductById(string productsId);
	}
}

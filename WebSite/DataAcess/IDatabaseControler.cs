using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.DataAcess
{
	public interface IDatabaseControler
	{
		public void AddRatings(string productId, float rating);
		public Product CreateNewProduct();
		public void DeleteProductById(string productId);
		public Product GetProductById(string productId);
		public IEnumerable<Product> GetProducts();
	}
}

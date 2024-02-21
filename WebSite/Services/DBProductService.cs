using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services.DatabaseAcess;

namespace ContosoCrafts.WebSite.Services
{
    public class DBProductService : IProductService
	{
		private IDatabaseAcess controler;

		public DBProductService(IDatabaseAcess controler)
		{
			this.controler = controler;
		}
		public void AddRatings(string productId, float rating)
		{
			controler.AddRatings(productId, rating);
		}
		public Product CreateNewProduct()
		{
			return controler.CreateNewProduct();
		}
		public void DeleteProductById(string productId)
		{
			controler.DeleteProductById(productId);
		}
		public Product GetProductById(string productId)
		{
			return controler.GetProductById(productId);
		}
		public IEnumerable<Product> GetProducts()
		{
			return controler.GetProducts();
		}
	}
}

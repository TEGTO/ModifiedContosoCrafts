using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.Services.DatabaseAcess
{
    public interface IDatabaseAcess
    {
        public void AddRatings(string productId, float rating);
        public Product CreateNewProduct();
        public void DeleteProductById(string productId);
        public Product GetProductById(string productId);
        public IEnumerable<Product> GetProducts();
    }
}

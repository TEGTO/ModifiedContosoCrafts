using ContosoCrafts.WebSite.Models;
using MySqlConnector;
using System.Data.Common;

namespace ContosoCrafts.WebSite.DataAcess
{
	public class MySqlDatabaseControler : IDatabaseControler
	{
		private readonly MySqlConnection connection;

		public MySqlDatabaseControler(MySqlConnection connection)
		{
			this.connection = connection;
			OpenConnection();
		}
		public void AddRatings(string productId, float rating)
		{
			var query = "INSERT INTO ratings (ProductId, Rating) VALUES (@ProductId, @Rating)";
			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@ProductId", productId);
				command.Parameters.AddWithValue("@Rating", rating);
				command.ExecuteNonQuery();
			}
		}
		public Product CreateNewProduct()
		{
			Product product = new Product
			{
				Id = Guid.NewGuid().ToString(),
				Maker = string.Empty,
				Image = string.Empty,
				Url = string.Empty,
				Title = string.Empty,
				Description = string.Empty
			};
			var query = "INSERT INTO products (Id, Maker, Image, Url, Title, Description) VALUES (@Id, @Maker, @Image, @Url, @Title, @Description)";
			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@Id", product.Id);
				command.Parameters.AddWithValue("@Maker", product.Maker);
				command.Parameters.AddWithValue("@Image", product.Image);
				command.Parameters.AddWithValue("@Url", product.Url);
				command.Parameters.AddWithValue("@Title", product.Title);
				command.Parameters.AddWithValue("@Description", product.Description);
				command.ExecuteNonQuery();
			}
			return product;
		}
		public void DeleteProductById(string productId)
		{
			connection.OpenAsync();
			var removeRatingsQuery = "DELETE FROM ratings WHERE ProductId = @ProductId";
			using (var removeRatingsCommand = new MySqlCommand(removeRatingsQuery, connection))
			{
				removeRatingsCommand.Parameters.AddWithValue("@ProductId", productId);
				removeRatingsCommand.ExecuteNonQuery();
			}
			var removeProductQuery = "DELETE FROM products WHERE Id = @ProductId";
			using (var removeProductCommand = new MySqlCommand(removeProductQuery, connection))
			{
				removeProductCommand.Parameters.AddWithValue("@ProductId", productId);
				removeProductCommand.ExecuteNonQuery();
			}
		}
		public Product GetProductById(string productId)
		{
			var product = new Product();
			var query = "SELECT Id, Maker, Image, Url, Title, Description FROM products WHERE Id = @ProductId";
			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@ProductId", productId);
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
						product = GetNewProductFromReader(reader);
				}
			}
			product.Ratings = GetProductRatings(productId);
			return product.Id == null ? null : product;
		}
		public IEnumerable<Product> GetProducts()
		{
			var products = new List<Product>();
			var query = "SELECT * FROM products";
			using (var command = new MySqlCommand(query, connection))
			{
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var product = GetNewProductFromReader(reader);
						products.Add(product);
					}
				}
			}
			for (int i = 0; i < products.Count; i++)
				products[i].Ratings = GetProductRatings(products[i].Id);
			return products;
		}
		private float[] GetProductRatings(string productId)
		{
			var ratings = new List<float>();
			var query = "SELECT Rating FROM ratings WHERE ProductId = @ProductId";
			using (var command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@ProductId", productId);
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
						ratings.Add(reader.GetFloat("Rating"));
				}
			}
			return ratings.ToArray();
		}
		private void OpenConnection()
		{
			if (connection.State != System.Data.ConnectionState.Open)
				connection.Open();
		}
		private Product GetNewProductFromReader(MySqlDataReader reader)
		{
			var product = new Product
			{
				Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? null : reader.GetString("Id"),
				Maker = reader.IsDBNull(reader.GetOrdinal("Maker")) ? null : reader.GetString("Maker"),
				Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : reader.GetString("Image"),
				Url = reader.IsDBNull(reader.GetOrdinal("Url")) ? null : reader.GetString("Url"),
				Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString("Title"),
				Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
			};
			return product;
		}
	}
}

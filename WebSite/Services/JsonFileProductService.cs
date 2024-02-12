using System.Text.Json;
using ContosoCrafts.WebSite.Models;

namespace ContosoCrafts.WebSite.Services
{
	public class JsonFileProductService : IProductService
	{
		private static int it = 0;

		private List<Product> products;

		public IWebHostEnvironment WebHostEnvironment { get; private set; }

		private string JsonFileName
		{
			get
			{
				return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json");
			}
		}

		public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
		{
			this.WebHostEnvironment = webHostEnvironment;
			ReadProductsFromJson();
		}
		public IEnumerable<Product> GetProducts()
		{
			return products.ToArray();
		}
		public void AddRatings(string productId, float rating)
		{
			Product product = products.First(x => x.Id == productId);
			if (product.Ratings == null)
				product.Ratings = new float[] { rating };
			else
			{
				List<float> ratings = product.Ratings.ToList();
				ratings.Add(rating);
				product.Ratings = ratings.ToArray();
			}
			SaveProductsToJson();
		}
		public Product GetProductById(string productsId)
		{
			Product product = products.Find(x => x.Id == productsId);
			return product;
		}
		public Product CreateNewProduct()
		{
			Product product = new Product();
			product.Id = it.ToString();
			it++;
			products.Add(product);
			SaveProductsToJson();
			return product;
		}
		public void DeleteProductById(string productsId)
		{
			if (products.Find(x => x.Id == productsId) != null)
			{
				products.Remove(GetProductById(productsId));
				SaveProductsToJson();
			}
		}
		private void SaveProductsToJson()
		{
			using (var outPutStream = File.OpenWrite(JsonFileName))
			{
				outPutStream.SetLength(0);
				JsonSerializer.Serialize<IEnumerable<Product>>(
				  new Utf8JsonWriter(outPutStream, new JsonWriterOptions
				  {
					  SkipValidation = true,
					  Indented = true
				  }),
				  products);
			}
		}
		private void ReadProductsFromJson()
		{
			using (var jsonFileReader = File.OpenText(JsonFileName))
			{
				products = JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
					new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					}).ToList();
			}
		}
	}
}


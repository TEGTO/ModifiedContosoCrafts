using System.Text.Json;
using WebSite.Models;

namespace WebSite.Services
{
    public class JsonFileProductService
    {
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
        }
        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
        public void AddRatings(string productId, float rating)
        {
            IEnumerable<Product> products = GetProducts();
            Product product = products.First(x => x.Id == productId);
            if (product.Ratings == null)
                product.Ratings = new float[] { rating };
            else
            {
                List<float> ratings = product.Ratings.ToList();
                ratings.Add(rating);
                product.Ratings = ratings.ToArray();
            }
            using (var outPutStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                  new Utf8JsonWriter(outPutStream, new JsonWriterOptions
                  {
                      SkipValidation = true,
                      Indented = true
                  }),
                  products);
            }
        }
    }
}


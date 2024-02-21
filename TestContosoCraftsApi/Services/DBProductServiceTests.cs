using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services.DatabaseAcess;
using Moq;

namespace ContosoCrafts.WebSite.Services.Tests
{
    [TestFixture()]
	public class DBProductServiceTests
	{
		private Mock<IDatabaseAcess> mockControler;
		private DBProductService productService;

		[SetUp]
		public void Setup()
		{
			mockControler = new Mock<IDatabaseAcess>();
			productService = new DBProductService(mockControler.Object);
		}
		[Test]
		public void GetProductByIdTest_NotExistsProduct_NullReturn()
		{
			// Arrange
			string productId = "testProductId";
			mockControler.Setup(c => c.GetProductById(productId)).Returns((Product)null);
			// Act
			var product = productService.GetProductById(productId);
			// Assert
			Assert.IsNull(product);
			mockControler.Verify(conn => conn.GetProductById(productId), Times.Once());
		}
		[Test]
		public void AddRatingsTest_ValidProduct_AddsRatingToDatabase()
		{
			// Arrange
			string productId = "testProductId";
			float rating = 4.5f;
			mockControler.Setup(c => c.AddRatings(productId, rating));
			var productWithRating = new Product
			{
				Id = productId,
				Ratings = new float[] { rating }
			};
			mockControler.Setup(c => c.GetProductById(productId)).Returns(productWithRating);
			// Act
			productService.AddRatings(productId, rating);
			var product = productService.GetProductById(productId);
			// Assert
			Assert.IsNotNull(product);
			Assert.AreEqual(rating, product.Ratings.FirstOrDefault());
			mockControler.Verify(conn => conn.AddRatings(productId, rating), Times.Once());
			mockControler.Verify(conn => conn.GetProductById(productId), Times.Once());
		}
		[Test]
		public void AddRatingsTest_NotExistsProduct_RatingNotAdded()
		{
			// Arrange
			string notExistsProduct = "testProductId";
			float rating = 4.5f;
			mockControler.Setup(c => c.AddRatings(notExistsProduct, rating));
			mockControler.Setup(c => c.GetProductById(notExistsProduct)).Returns((Product)null);
			// Act
			productService.AddRatings("invalidProductId", rating);
			var product = productService.GetProductById(notExistsProduct);
			// Assert
			Assert.IsNull(product);
			mockControler.Verify(conn => conn.AddRatings("invalidProductId", rating), Times.Once());
			mockControler.Verify(conn => conn.GetProductById(notExistsProduct), Times.Once());
		}
		[Test()]
		public void CreateNewProductTest_CreateNewProduct()
		{
			mockControler.Setup(c => c.CreateNewProduct()).Returns(new Product());
			// Act
			var product = productService.CreateNewProduct();
			// Assert
			Assert.IsNotNull(product);
			mockControler.Verify(conn => conn.CreateNewProduct(), Times.Once());
		}

		[Test()]
		public void GetProductsTest_GetProducts()
		{
			mockControler.Setup(c => c.GetProducts()).Returns(new List<Product>() { new Product(), new Product() });
			// Act
			var products = productService.GetProducts();
			// Assert
			Assert.IsNotNull(products);
			Assert.IsTrue(products.Count() == 2);
			mockControler.Verify(conn => conn.GetProducts(), Times.Once());
		}

		[Test()]
		public void DeleteProductByIdTest_ValidProduct_DeletedProductIsNull()
		{
			// Arrange
			string productId = "testProductId";
			mockControler.Setup(c => c.DeleteProductById(productId));
			mockControler.Setup(c => c.GetProductById(productId)).Returns((Product)null);
			// Act
			productService.DeleteProductById(productId);
			var product = productService.GetProductById(productId);
			// Assert
			Assert.IsNull(product);
			mockControler.Verify(conn => conn.DeleteProductById(productId), Times.Once());
			mockControler.Verify(conn => conn.GetProductById(productId), Times.Once());
		}

		[Test()]
		public void DeleteProductByIdTest_InvalidProduct_ProductIsNotDeleted()
		{
			// Arrange
			string productId = "testProductId";
			var newProduct = new Product
			{
				Id = productId,
			};
			mockControler.Setup(c => c.DeleteProductById(""));
			mockControler.Setup(c => c.GetProductById(productId)).Returns(newProduct);
			// Act
			productService.DeleteProductById("");
			var product = productService.GetProductById(productId);
			// Assert
			Assert.IsNotNull(product);
			mockControler.Verify(conn => conn.DeleteProductById(""), Times.Once());
			mockControler.Verify(conn => conn.GetProductById(productId), Times.Once());
		}
	}
}

using ContosoCrafts.WebSite.Services;
using Moq;
using TestContosoCraftsApi.DataAcess;

namespace TestContosoCraftsApi.Controllers
{
	public class Tests
	{
		private Mock<IMySqlConnection> mockConnection;
		private MySqlProductService productService;

		[SetUp]
		public void Setup()
		{
			mockConnection = new Mock<IMySqlConnection>();
			productService = new MySqlProductService(mockConnection.Object);
		}
		[Test]
		public void AddRatings_ValidInput_AddsRatingToDatabase()
		{
			// Arrange
			string productId = "testProductId";
			float rating = 4.5f;
			// Act
			productService.AddRatings(productId, rating);
			// Assert
			mockConnection.Verify(conn => conn.Open(), Times.Once());
			mockConnection.Verify(conn => conn.CreateCommand(), Times.Once());
			mockConnection.Verify(conn => conn.ExecuteNonQueryAsync(It.IsAny<string>()), Times.Once());
		}
	}
}

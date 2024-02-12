using MySqlConnector;

namespace TestContosoCraftsApi.DataAcess
{
	public interface IMySqlConnection
	{
		Task OpenAsync();
		Task<MySqlCommand> CreateCommand();
		Task<int> ExecuteNonQueryAsync(string query);
	}
}

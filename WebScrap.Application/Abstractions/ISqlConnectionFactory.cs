using Microsoft.Data.SqlClient;
using WebScrap.Core.Models;

namespace WebScrap.Application.Abstractions;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection(string connectionString);
    void CloseConnection(SqlConnection connection);
    Task SaveData(ApiResponseDto data, SqlConnection connection);
}

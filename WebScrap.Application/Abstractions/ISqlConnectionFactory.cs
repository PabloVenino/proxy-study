using Microsoft.Data.SqlClient;

namespace WebScrap.Application.Abstractions;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection(string connectionString);
    void CloseConnection(SqlConnection connection);
}
    


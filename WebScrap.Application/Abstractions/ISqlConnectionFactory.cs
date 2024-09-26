using Microsoft.Data.SqlClient;

namespace WebScrap.Application.Abstractions;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
    void CloseConnection(SqlConnection connection);
}
    


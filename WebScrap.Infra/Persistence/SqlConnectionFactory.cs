using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebScrap.Application.Abstractions;

namespace WebScrap.Infra.Persistence;

internal sealed class SqlConnectionFactory() : ISqlConnectionFactory
{
    public SqlConnection CreateConnection(string connectionString) =>
        connectionString == null ? throw new ArgumentNullException(nameof(connectionString)) : new SqlConnection(connectionString);
    
     
    public void CloseConnection(SqlConnection connection) 
    {
        ArgumentNullException.ThrowIfNull(connection);

        connection.Close();
    }
    
}

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebScrap.Application.Abstractions;

namespace WebScrap.Infra.Persistence;

internal sealed class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
{
    private readonly IConfiguration _config = configuration;

    public SqlConnection CreateConnection()
    {
        string? connectionString = _config?["ConnectionStrings:Connection"];

        if (connectionString == null)
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        return new SqlConnection(connectionString);
    }

    public void CloseConnection(SqlConnection connection) 
    {
        if (connection == null)
        {
            throw new ArgumentNullException(nameof(connection));
        }

        connection.Close();
    }
    
}

using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using WebScrap.Application.Abstractions;
using WebScrap.Core.Models;

namespace WebScrap.Infra.Persistence;

public sealed class SqlConnectionFactory() : ISqlConnectionFactory
{
    public SqlConnection CreateConnection(string connectionString) =>
        connectionString == null ? throw new ArgumentNullException(nameof(connectionString)) : new SqlConnection(connectionString);
    
     
    public void CloseConnection(SqlConnection connection) 
    {
        ArgumentNullException.ThrowIfNull(connection);

        connection.Close();
    }
    
    public async Task SaveData(ApiResponseDto data, SqlConnection connection)
    {
        var parameters = new
        {
            scrap_name = "Scrap from Magalu",
            proxy_used = "localhost:8080",
            item_category = "eletrodomesticos",
            item_name = data.Data.Product.Title,
            item_price = decimal.Parse(data.Data.Product.Price.BestPrice, CultureInfo.InvariantCulture),
            item_has_discount = true,
            item_discount = decimal.Parse(data.Data.Product.Price.FullPrice, CultureInfo.InvariantCulture) - decimal.Parse(data.Data.Product.Price.BestPrice, CultureInfo.InvariantCulture),
            item_price_observations = data.Data.Product.Installment.PaymentMethodDescription,
            item_rating_count = 9999, // unknown for this example
            item_rating = data.Data.Product.Rating.Score,
        };

        await connection.QueryAsync("scraping_magalu_upsert", param: parameters, commandType: CommandType.StoredProcedure);
    }
}

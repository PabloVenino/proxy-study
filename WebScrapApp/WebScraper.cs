using WebScrap.Application.Abstractions;
using WebScrapApp.ProxyHandlers;
using System.Net.Http.Json;
using System.Text.Json;
using System.IO.Compression;
using System.Text;
using WebScrap.Core.Models;

namespace WebScrapApp;

public class WebScraper(string connectionString, ISqlConnectionFactory sqlConnection)
{
    private readonly ISqlConnectionFactory _sqlConnection = sqlConnection;

    public async Task ScrapeData(ProxyRotator proxyRotator, string url)
    {
        try
        {
            var connection = _sqlConnection.CreateConnection(connectionString);

            var client = proxyRotator.ScrapeDataWithRandomProxy();

            object payload = new
            {
                query = "\n      query(\n          $id: String!\n        ) {\n          product(\n            productRequest: {\n              id: $id\n            }\n          ) {\n            id\n            title\n            reference\n            url\n            image\n            rating {\n              score\n            }\n            price {\n              price\n              fullPrice\n              bestPrice\n            }\n            installment {\n              paymentMethodDescription\n              quantity\n              amount\n              totalAmount\n            }\n          }\n        }\n      ",
                variables = new { id = "238492300" }
            };

            // For this specific request for Magazine Luiza
            client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Google Chrome\";v=\"129\", \"Not=A?Brand\";v=\"8\", \"Chromium\";v=\"129\"");
            client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
            client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
            client.DefaultRequestHeaders.Add("sec-fetch-site", "cross-site");

            var response = await client.PostAsJsonAsync<object>(url, payload);


            var content = await response.Content.ReadAsStringAsync();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            Stream decompressedStream;

            if (response.Content.Headers.ContentEncoding.Contains("gzip"))
                decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
            else
                decompressedStream = responseStream;

            using var reader = new StreamReader(decompressedStream, Encoding.UTF8);

            string jsonResponse = await reader.ReadToEndAsync();
            Console.WriteLine(jsonResponse);

            JsonSerializerOptions jsonSerializerOptions = new()
            {
                PropertyNameCaseInsensitive = true
            };

            ApiResponseDto apiResponse = JsonSerializer.Deserialize<ApiResponseDto>(jsonResponse, jsonSerializerOptions);

            await _sqlConnection.SaveData(apiResponse, connection);

            return;
        } 
        catch 
        {
            throw;
        }

    }
}


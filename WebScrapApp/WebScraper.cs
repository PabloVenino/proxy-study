using HtmlAgilityPack;
using WebScrap.Application.Abstractions;
using WebScrapApp.ProxyHandlers;

namespace WebScrapApp;

public class WebScraper(string connectionString, ISqlConnectionFactory connectionFactory)
{
    private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

    public async Task ScrapeData(ProxyRotator proxyRotator, string url)
    {
        try
        {
            _connectionFactory.CreateConnection(connectionString);

            var client = proxyRotator.ScrapeDataWithRandomProxy();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            HtmlDocument doc = new();
            doc.LoadHtml(content);
            
            // TODO: Get data from their possibles APIs, public GET Requests, GraphQL requets, and so on.

            // Examples of nodes using Xpaths
            var nodes = doc.DocumentNode.SelectNodes("//li/a[@href] | //p/a[@href] | //td/a[@href]");

            if(nodes is not null)
            {
                foreach(var node in nodes)
                {
                    string hrefValue = node.GetAttributeValue("href", string.Empty);
                    string title = node.InnerText;
                    
                    Uri baseUri = new(url);
                    Uri fullUri = new(baseUri, hrefValue);

                    Console.WriteLine($"Title: {title}, Link: {fullUri.AbsoluteUri}");
                }
            }
            else
                Console.WriteLine("Nothing too much reliable found.");
        } 
        catch 
        {
            throw;
        }

    }
}


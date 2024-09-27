using WebScrapApp.ProxyHandlers;
using Microsoft.Extensions.DependencyInjection;
using WebScrap.Application;
using WebScrap.Infra;
using Microsoft.Extensions.Configuration;
using WebScrap.Application.Abstractions;

namespace WebScrapApp;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Inicio");
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        // Services Pipeline
        var sqlFactory = serviceProvider.GetService<ISqlConnectionFactory>();


        var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
        
        var config = builder.Build();

        var connectionString = config["ConnectionString"];

        
        // Put here your own list of Proxies, these are a free ones, that may not work.
        string[] proxies =
        {
            "http://117.74.125.100:1133",
            "http://177.38.5.45:4153",
            "http://8.218.198.49:8888"
        };

        var proxyRotator = new ProxyRotator(proxies);
        string urlToScrape = "http://www.wikipedia.org/";
        var webScraper = new WebScraper(connectionString, sqlFactory);
        await webScraper.ScrapeData(proxyRotator, urlToScrape);
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services
            .AddApplication()
            .AddInfra();
    }
}

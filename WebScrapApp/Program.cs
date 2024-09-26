using WebScrapApp.ProxyHandlers;
using Microsoft.Extensions.DependencyInjection;
using WebScrap.Application;

namespace WebScrapApp;

public class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // Put here your own list of Proxies, these are a free ones, that may not work.
        string[] proxies =
        {
            "http://117.74.125.100:1133",
            "http://177.38.5.45:4153",
            "http://8.218.198.49:8888"
        };

        var proxyRotator = new ProxyRotator(proxies);
        string urlToScrape = "https://www.wikipedia.org/";
        await WebScraper.ScrapeData(proxyRotator, urlToScrape);
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
    }
}

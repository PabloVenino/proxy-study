using WebScrapApp.ProxyHandlers;

namespace WebScrapApp;

public class Program
{
    static async Task Main(string[] args)
    {
        // A list of free proxies IPs
        string[] proxies =
        {
            "http://162.223.89.84:80",
            "http://203.80.189.33:8080",
            "http://94.45.74.60:8080",
            "http://162.248.225.8:80",
            "http://167.71.5.83:3128"
        };

        var proxyRotator = new ProxyRotator(proxies, false);
        string urlToScrape = "https://www.wikipedia.org/";
        await WebScraper.ScrapeData(proxyRotator, urlToScrape);
    }
}

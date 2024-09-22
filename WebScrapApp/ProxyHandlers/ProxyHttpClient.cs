using System.Net;

namespace WebScrapApp.ProxyHandlers;

internal class ProxyHttpClient
{
    public static HttpClient CreateClient(string proxyUrl)
    {
        var httpClienteHandler = new HttpClientHandler()
        {
            Proxy = new WebProxy(proxyUrl),
            UseProxy = true
        };

        return new HttpClient(httpClienteHandler);
    }
}

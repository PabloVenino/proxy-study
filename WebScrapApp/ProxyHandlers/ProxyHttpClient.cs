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

        // Bypass for SLL Certificate
        httpClienteHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sllPolicyErrors) => { return true; };

        return new HttpClient(httpClienteHandler);
    }
}

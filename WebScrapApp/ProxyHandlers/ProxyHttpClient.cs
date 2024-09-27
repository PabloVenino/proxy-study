using System.Net;
using System.Net.Http.Headers;

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

        var client = new HttpClient(httpClienteHandler);

        client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
        {
            NoCache = true
        };

        client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br, zstd");
        client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9,pt-BR;q=0.8,pt;q=0.7,ja-JP;q=0.6,ja;q=0.5");
        client.DefaultRequestHeaders.Pragma.ParseAdd("no-cache");

        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) Chrome/129.0.0.0");

        return client;
    }
}

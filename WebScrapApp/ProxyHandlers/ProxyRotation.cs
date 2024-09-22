
namespace WebScrapApp.ProxyHandlers;

public class ProxyRotator
{
    private readonly List<string> _validProxies = new List<string>();
    private readonly Random _random = new();

    public ProxyRotator(string[] proxies, bool isLocal)
    {
        if (isLocal)
            _validProxies.Add("");
        else
            _validProxies = ProxyChecker.GetWorkingProxies([.. proxies]).Result;

        if (_validProxies.Count == 0)
            throw new InvalidOperationException("Unfortunately the proxies are invalid for some reason.");
    }

    public HttpClient ScrapeDataWithRandomProxy()
    {
        if (_validProxies.Count == 0)
            throw new InvalidOperationException("Unfortunately the proxies are invalid for some reason.");

        var proxyUrl = _validProxies[_random.Next(_validProxies.Count)];
        
        return ProxyHttpClient.CreateClient(proxyUrl);
    }
}

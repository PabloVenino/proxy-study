
using System.Diagnostics.CodeAnalysis;

namespace WebScrapApp.ProxyHandlers;

internal class ProxyChecker
{
    private static SemaphoreSlim _semaphore = new(1, 1);
    private static int _currentProxyNumber = 0;

    public static async Task<List<string>> GetWorkingProxies(List<string> proxies)
    {
        var tasks = new List<Task<Tuple<string, bool>>>();

        foreach(var proxy in proxies)
        {
            tasks.Add(CheckProxy(proxy, proxies.Count()));
        }

        var results = await Task.WhenAll(tasks);

        var workingProxies = new List<string>();
        
        foreach(var result  in results)
        {
            if(result.Item2)
            {
                workingProxies.Add(result.Item1);
            }
        }

        return workingProxies;
    }

    private static async Task<Tuple<string, bool>> CheckProxy(string proxyUrl, int totalProxies)
    {
        var client = ProxyHttpClient.CreateClient(proxyUrl);
        bool isWorking = await IsProxyWorking(client);

        await _semaphore.WaitAsync();

        try
        {
            _currentProxyNumber++;
            Console.WriteLine($"Proxy: {_currentProxyNumber} of {totalProxies}");
        }
        finally
        {
            _semaphore.Release();
        }

        return new Tuple<string, bool>(proxyUrl, isWorking);
    }

    private static async Task<bool> IsProxyWorking(HttpClient client)
    {
        try
        {
            var testUrl = "http://www.google.com";
            var response = await client.GetAsync(testUrl);
            return response.IsSuccessStatusCode;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
}

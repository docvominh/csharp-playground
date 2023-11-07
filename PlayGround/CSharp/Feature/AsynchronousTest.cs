using System.Net;

namespace CSharp.Feature;

class AsynchronousTest
{
    private async Task<string> GetDataAsync()
    {
        var client = new HttpClient();
        Console.WriteLine("Request: " + Thread.CurrentThread.Name);
        var result = await client.GetAsync("https://dummyjson.com/products/1");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine("Parsing response: " + Thread.CurrentThread.Name);
            return await (result.Content.ReadAsStringAsync() ?? throw new InvalidOperationException());
        }

        return null!;
    }

    [Test]
    public async Task TestAsynchronous()
    {
        Console.WriteLine("App Start: " + Thread.CurrentThread.Name);
        var task = GetDataAsync();
        Console.WriteLine("Do some works: " + Thread.CurrentThread.Name);
        var result = await task;
        Console.WriteLine("Show result: " + Thread.CurrentThread.Name);
        Console.WriteLine(result);

        Assert.Pass();
    }
}
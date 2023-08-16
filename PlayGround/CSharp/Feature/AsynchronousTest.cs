using System.Net;

namespace CSharp.Feature;

class AsynchronousTest
{
    private async Task<string> GetData()
    {
        var client = new HttpClient();
        var result = await client.GetAsync("https://dummyjson.com/products/1");
        Console.WriteLine("Processing..." + Thread.CurrentThread.Name);

        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await (result.Content.ReadAsStringAsync() ?? throw new InvalidOperationException());
        }

        return null!;
    }


    [Test]
    public async Task TestAsynchronous()
    {
        var task = GetData();
        Console.WriteLine("Loading..." + Thread.CurrentThread.Name);
        var result = await task;
        Console.WriteLine(result);

        Assert.Pass();
    }
}
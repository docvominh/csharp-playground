using System.Diagnostics;
using System.Net;

namespace CSharp.Feature;

class AsynchronousTest
{
    private async Task<string> GetProduct(int productId)
    {
        var client = new HttpClient();
        var result = await client.GetAsync($"https://dummyjson.com/products/{productId}");

        if (result.StatusCode == HttpStatusCode.OK)
        {
            return await (result.Content.ReadAsStringAsync() ?? throw new InvalidOperationException());
        }

        return result.ToString();
    }

    [Test]
    public async Task TestAsynchronous()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        await GetProduct(1);
        await GetProduct(2);
        await GetProduct(3);

        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        Assert.Pass();
    }

    [Test]
    public async Task TestAsynchronous2()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var getProduct1Task = GetProduct(1);
        var getProduct2Task = GetProduct(2);
        var getProduct3Task = GetProduct(3);

        await Task.WhenAll(getProduct1Task, getProduct2Task, getProduct3Task);

        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        Assert.Pass();
    }


    [Test]
    public async Task TestAsynchronous3()
    {
        var client = new HttpClient();
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        var product1 = await client.GetAsync("https://dummyjson.com/products/1");
        var product2 = await client.GetAsync("https://dummyjson.com/products/2");
        var product3 = await client.GetAsync("https://dummyjson.com/products/3");
        stopwatch.Stop();

        Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }

    [Test]
    public async Task TestAsynchronous4()
    {
        var client = new HttpClient();
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        var getProduct1Task = client.GetAsync("https://dummyjson.com/products/1");
        var getProduct2Task = client.GetAsync("https://dummyjson.com/products/2");
        var getProduct3Task = client.GetAsync("https://dummyjson.com/products/3");

        await Task.WhenAll(getProduct1Task, getProduct2Task, getProduct3Task);

        stopwatch.Stop();


        Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }

    [Test]
    public async Task GetProduct()
    {
        // var client = new HttpClient();
        // Stopwatch stopwatch = new Stopwatch();
        //
        // stopwatch.Start();
        // Task<HttpResponseMessage> getProductTask1 = client.GetAsync("https://dummyjson.com/products/1");
        // Task<HttpResponseMessage> getProductTask2 = client.GetAsync("https://dummyjson.com/products/2");
        // Task<HttpResponseMessage> getProductTask3 = client.GetAsync("https://dummyjson.com/products/3");
        //
        // await Task.WhenAll(getProductTask1, getProductTask2, getProductTask3);
        // stopwatch.Stop();
        // Console.WriteLine(await getProductTask1.Result.Content.ReadAsStringAsync());
        // Console.WriteLine(await getProductTask2.Result.Content.ReadAsStringAsync());
        // Console.WriteLine(await getProductTask3.Result.Content.ReadAsStringAsync());
        // Console.WriteLine(stopwatch.ElapsedMilliseconds); // About ~780 ms

        var cts = new CancellationTokenSource();

        // Create a cancellation token from the source
        var token = cts.Token;

        // Start a task that will complete after a delay
        var task = Task.Factory.StartNew(() =>
        {
            for (int i = 0; i < 100; i++)
            {
                // Check if the task has been cancelled
                if (token.IsCancellationRequested)
                {
                    // End the task
                    Console.WriteLine("Task cancelled");
                    return;
                }

                // Perform some work
                Console.WriteLine("Task running");
                Thread.Sleep(200);
            }

            // Task completed
            Console.WriteLine("Task completed");
        }, token);

        // Wait for a key press
        Console.ReadKey();

        // Cancel the task
        cts.Cancel();

        // Wait for the task to complete
        task.Wait(token);
    }
}

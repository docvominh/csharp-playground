using Newtonsoft.Json;

namespace CSharp.Feature;

public class Dispose
{
    [Test]
    public void TestDispose()
    {
        // TestInternal();
        // // resourceManager.Dispose();
        // GC.SuppressFinalize(this);
        // Thread.Sleep(30000);

        var x = 1;
        var y = JsonConvert.SerializeObject(x);
        Assert.That("1", Is.EqualTo(y));
    }

    private void TestInternal()
    {
        ResourceManager resourceManager = new ResourceManager();
        resourceManager.Resources = new List<string>() { "1", "2" };
    }
}

class ResourceManager : IDisposable
{
    bool disposed;
    public List<string> Resources { get; set; }



    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                Console.WriteLine("Disposing");
                Resources.Clear();
                Resources = null;
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
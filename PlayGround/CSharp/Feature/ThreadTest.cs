namespace CSharp.Feature;

public class ThreadTest
{
    [Fact]
    public void TestThread()
    {
        Console.WriteLine("Main Thread: " + Thread.CurrentThread.Name);

        Task.Run(() => { Console.WriteLine("BackGround Thread: " + Thread.CurrentThread.Name); });
    }
}
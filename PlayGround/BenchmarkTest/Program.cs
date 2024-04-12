using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


Console.WriteLine("Start");
BenchmarkRunner.Run<BenchCheck>();
Console.WriteLine("Done");

[MemoryDiagnoser]
public class BenchCheck
{
    [Benchmark]
    public void OneMilionLoop()
    {
        for (int i = 0; i < 1000000; i++)
        {
            string value = i+"";
        }
    }
}

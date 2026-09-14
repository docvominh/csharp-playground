using BenchmarkDotNet.Attributes;

namespace CSharp.Feature;

[MemoryDiagnoser]
public class BenchMarkTest
{
    [Fact]
    public void TestBenchMark()
    {
        OneMilionLoop();
    }

    [Benchmark]
    private void OneMilionLoop()
    {
        for (int i = 0; i < 1000000; i++)
        {
            long value = i;
        }
    }
}

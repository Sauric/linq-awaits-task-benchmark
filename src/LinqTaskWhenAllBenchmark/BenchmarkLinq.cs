using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace Sauric.Research;

[MemoryDiagnoser]
[SimpleJob(RunStrategy.ColdStart)]
public class BenchmarkLinq
{
    [Benchmark]
    public async Task SimpleApproachAsync()
    {
        var tasks = Enumerable.Range(0, 10).Select(static _ => SomeMethod()).ToArray();
        await Task.WhenAll(tasks);
    }
    
    [Benchmark]
    public async Task TaskYieldApproachAsync()
    {
        var tasks = Enumerable.Range(0, 10).Select(static _ => SomeMethodYield()).ToArray();
        await Task.WhenAll(tasks);
    }
    
    [Benchmark]
    public async Task TaskRunApproachAsync()
    {
        var tasks = Enumerable.Range(0, 10).Select(static _ => Task.Run(() => SomeMethod())).ToArray();
        await Task.WhenAll(tasks);
    }

    private static async Task SomeMethod()
    {
        Thread.Sleep(1000);
        await Task.Delay(1000);
    }
    
    private static async Task SomeMethodYield()
    {
        await Task.Yield();
        Thread.Sleep(1000);
        await Task.Delay(1000);
    }
}
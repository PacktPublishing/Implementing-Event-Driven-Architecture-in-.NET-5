using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole
{
  [SimpleJob(RunStrategy.Monitoring, id: "Data Parallelism")]
  [MemoryDiagnoser]
  [EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.CpuSampling)]
  [ThreadingDiagnoser]
  [CsvMeasurementsExporter]
  [RPlotExporter]
  public class DataParallelism
  {
    private const int Count = 1_000_000;
    private readonly long[] nums = Enumerable.Range(1, Count).Select(t => (long)t).ToArray();

    [Benchmark(Description = "6.PLINQLB")]
    public void SumPLINQLoadBalance()
    {
      // yield calling thread
      Thread.Sleep(1);

      long total = Partitioner.Create(nums, true).AsParallel().Sum();

      Console.WriteLine($"{nameof(SumPLINQLoadBalance)} total is {total:N0}");
    }

    [Benchmark(Description = "5.PLINQ")]
    public void SumPLINQ()
    {
      // yield calling thread
      Thread.Sleep(1);

      long total = nums.AsParallel().Sum();

      Console.WriteLine($"{nameof(SumPLINQ)} total is {total:N0}");
    }

    [Benchmark(Description = "4.LINQ")]
    public void SumLINQ()
    {
      // yield calling thread
      Thread.Sleep(1);

      long total = nums.Sum();

      Console.WriteLine($"{nameof(SumLINQ)} total is {total:N0}");
    }

    [Benchmark(Description = "3.PForLB")]
    public void SumParallelForLoadBalance()
    {
      // yield calling thread
      Thread.Sleep(1);
      long total = 0;

      Parallel.ForEach<long, long>(
        source: Partitioner.Create(nums, true),
        localInit: () => 0,
        body: (n, loopState, localState) =>
        {
          return (localState + n);
        },
        localFinally: (localState) => Interlocked.Add(ref total, localState)
        );

      Console.WriteLine($"{nameof(SumParallelForLoadBalance)} total is {total:N0}");
    }

    [Benchmark(Description = "2.PFor")]
    public void SumParallelFor()
    {
      // yield calling thread
      Thread.Sleep(1);

      long total = 0;

      Parallel.For<long>(
        fromInclusive: 0,
        toExclusive: nums.Length,
        localInit: () => 0,
        body: (i, loopState, localState) => { return localState + nums[i]; },
        localFinally: (localState) => Interlocked.Add(ref total, localState)
        );

      Console.WriteLine($"{nameof(SumParallelFor)} total is {total:N0}");
    }

    [Benchmark(Baseline = true, Description = "1.For")]
    public void SumFor()
    {
      // yield calling thread
      Thread.Sleep(1);

      long total = 0;

      for (int i = 0; i < nums.Length; i++)
      {
        total += nums[i];
      }

      Console.WriteLine($"{nameof(SumFor)} total is {total:N0}");
    }
  }
}
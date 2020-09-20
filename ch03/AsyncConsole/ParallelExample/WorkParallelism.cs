using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole
{
  [SimpleJob(RunStrategy.Monitoring, id: "Work Parallelism")]
  [MemoryDiagnoser]
  [EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.CpuSampling)]
  [ThreadingDiagnoser]
  [CsvMeasurementsExporter]
  [RPlotExporter]
  public class WorkParallelism
  {
    const int count = 100_000;
    readonly int[] job = Enumerable.Range(1, count).ToArray();

    [Benchmark(Description = "3.Task.WaitAll")]
    public void WorkConcurrentParallel()
    {
      Task[] taskArray = new Task[]
      {
        Task.Factory.StartNew(()=>CalculateSum(job)),
        Task.Factory.StartNew(()=>CalculateSum(job)),
        Task.Factory.StartNew(()=>CalculateSum(job)),
        Task.Factory.StartNew(()=>CalculateSum(job)),
      };

      Task.WaitAll(taskArray);
    }

    [Benchmark(Description = "2.Parallel.Invoke")]
    public void WorkAsync()
    {
      Parallel.Invoke(
        () => CalculateSum(job),
        () => CalculateSum(job),
        () => CalculateSum(job),
        () => CalculateSum(job)
        );
    }

    [Benchmark(Description = "1.Serial", Baseline = true)]
    public void WorkSync()
    {
      CalculateSum(job);
      CalculateSum(job);
      CalculateSum(job);
      CalculateSum(job);
    }

    public void CalculateSum(int[] nums)
    {
      long total = 0;

      for (int i = 0; i < nums.Length; i++)
      {
        total += nums[i];
      }

      Thread.Sleep(1);
      Console.WriteLine($"{nameof(CalculateSum)} total is {total:N0}");
    }
  }
}
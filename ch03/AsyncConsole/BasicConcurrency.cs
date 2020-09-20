using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace AsyncApp
{
  [SimpleJob(RunStrategy.Monitoring, id: "Async Patterns")]
  [MemoryDiagnoser]
  [EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.CpuSampling)]
  [ThreadingDiagnoser]
  [CsvMeasurementsExporter]
  [RPlotExporter]
  public class BasicConcurrency
  {
    [Benchmark]
    public void RunUsingCustomThreads()
    {
      var threadA = new Thread(Logic);
      var threadB = new Thread(Logic);

      threadA.IsBackground = true;
      threadA.Priority = ThreadPriority.AboveNormal;
#pragma warning disable CA1416 // Validate platform compatibility
      threadA.SetApartmentState(ApartmentState.STA);
#pragma warning restore CA1416 // Validate platform compatibility

      threadA.IsBackground = true;
      threadB.Priority = ThreadPriority.Normal;
#pragma warning disable CA1416 // Validate platform compatibility
      threadB.SetApartmentState(ApartmentState.STA);
#pragma warning restore CA1416 // Validate platform compatibility

      threadA.Start("A");
      threadB.Start("B");

      threadA.Join();
      threadB.Join();
    }

    [Benchmark]
    public void RunUsingThreadPool()
    {
      ThreadPool.QueueUserWorkItem(Logic, "A", false);
      ThreadPool.QueueUserWorkItem(Logic, "B", false);

      while (ThreadPool.PendingWorkItemCount != 0)
      {
        // Give time for threads to finish
        Thread.Sleep(1);
      }
    }

    [Benchmark(Baseline = true)]
    public Task RunAsync()
    {
      var tasksToRun = Task.WhenAll(
          LogicAsync("A"),
          LogicAsync("B")
          );

      return tasksToRun;
    }

    private Task LogicAsync(object threadName)
    {
      // Option 1
      // Queue a task for the Logic method to run on the thread pool
      // return Task.Run(() => Logic(threadName));

      // Option 2
      // Create and start a task for the Logic method with param threadName 
      // and default creation options.
      return Task.Factory.StartNew(Logic, threadName, TaskCreationOptions.LongRunning);
    }
    private void Logic(object threadName)
    {
      WriteLine($"Logic {threadName}");
      foreach (var n in Enumerable.Range(1, 5))
      {
        WriteLine($"{threadName}: {n}");
      }
    }
  }
}

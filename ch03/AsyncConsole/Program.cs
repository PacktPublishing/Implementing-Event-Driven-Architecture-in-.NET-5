using BenchmarkDotNet.Running;

namespace AsyncConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      StartupBenchmark();
    }

    private static void StartupBenchmark()
    {
      /*
       * Uncomment a benchmark and run
       * dotnet run --project .\AsyncConsole\ -c Release
       * at a shell prompt
       */

      //BenchmarkRunner.Run<BasicConcurrency>();
      //BenchmarkRunner.Run<DataParallelism>();
      //BenchmarkRunner.Run<WorkParallelism>();
    }
  }
}
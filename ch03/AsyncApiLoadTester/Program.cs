using BenchmarkDotNet.Running;

namespace AsyncApiLoadTester
{
  class Program
  {
    static void Main(string[] args)
    {
      StartupBenchmark();
    }

    private static void StartupBenchmark()
    {
      BenchmarkRunner.Run<AsyncApiLoadTester>();
    }
  }
}

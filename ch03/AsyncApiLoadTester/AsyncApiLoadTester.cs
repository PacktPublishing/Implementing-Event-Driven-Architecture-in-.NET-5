using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncApiLoadTester
{

  [SimpleJob(RunStrategy.Throughput, id: "AsyncApiLoadTester")]
  [MemoryDiagnoser]
  [EventPipeProfiler(BenchmarkDotNet.Diagnosers.EventPipeProfile.CpuSampling)]
  [ThreadingDiagnoser]
  [CsvMeasurementsExporter]
  [RPlotExporter]
  public class AsyncApiLoadTester
  {
    private HttpClient client;

    [GlobalSetup]
    public void Setup()
    {
      client = new HttpClient()
      {
        BaseAddress = new System.Uri("http://localhost:5000")
      };
    }

    [GlobalCleanup]
    public void Cleanup()
    {
      client?.Dispose();
    }

    [Benchmark(Baseline = true)]
    public async Task TestWeatherForecastSync()
    {
      _ = await client.GetAsync("/WeatherForecast");
    }

    [Benchmark]
    public async Task TestWeatherForecastAsync()
    {
      _ = await client.GetAsync("/WeatherForecastAsync");
    }
  }
}

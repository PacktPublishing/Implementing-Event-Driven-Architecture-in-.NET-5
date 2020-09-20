using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncApi.Weather
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastAsyncController : ControllerBase
  {
    private readonly ILogger<WeatherForecastAsyncController> _logger;

    public WeatherForecastAsyncController(ILogger<WeatherForecastAsyncController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
      return await Task.Run(() =>
      {
      // Simulate an optimized 20ms latency
      Task.Delay(TimeSpan.FromMilliseconds(20));
        return WeatherForecastFixture.GetFiveDayForecastAsync();
      });
    }
  }
}

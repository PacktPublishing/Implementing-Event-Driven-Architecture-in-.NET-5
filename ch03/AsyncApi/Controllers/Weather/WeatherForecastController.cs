using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AsyncApi.Weather
{
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
  private readonly ILogger<WeatherForecastController> _logger;

  public WeatherForecastController(ILogger<WeatherForecastController> logger)
  {
    _logger = logger;
  }

  [HttpGet]
  public IEnumerable<WeatherForecast> Get()
  {
    // Simulate an optimized 20ms latency
    Thread.Sleep(TimeSpan.FromMilliseconds(20));
    return WeatherForecastFixture.GetFiveDayForecast();
  }
}
}

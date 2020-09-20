using System;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncApi.Weather
{
  public static class WeatherForecastFixture
  {
    private static readonly string[] Summaries = new[]
    {
      "Freezing",
      "Bracing",
      "Chilly",
      "Cool",
      "Mild",
      "Warm",
      "Balmy",
      "Hot",
      "Sweltering",
      "Scorching"
    };

    public static WeatherForecast[] GetFiveDayForecast()
    {
      var rng = new Random();
      return Enumerable.Range(1, 5)
        .Select(index => new WeatherForecast
        {
          Date = DateTime.Now.AddDays(index),
          Celcius = rng.Next(-20, 55),
          Summary = Summaries[rng.Next(Summaries.Length)]
        }).ToArray();
    }

    public static Task<WeatherForecast[]> GetFiveDayForecastAsync()
    {
      return Task.FromResult(GetFiveDayForecast());
    }
  }
}
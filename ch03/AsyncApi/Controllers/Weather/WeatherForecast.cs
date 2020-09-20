using System;

namespace AsyncApi.Weather
{
  public record WeatherForecast
  {
    public DateTime Date { get; set; }

    public int Celcius { get; set; }

    public int Fahrenheit => Celcius.ToFahrenheit();

    public string Summary { get; set; }
  }

  public static class TemperatureConvertExtensions
  {
    public static int ToFahrenheit(this int c) => 32 + (int)(c / 0.5556);
  }
}
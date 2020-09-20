using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncConsole
{
  public static class IOComputeBound
  {
    /// <summary>
    /// Download web data, an IO-bound with no concurrency
    /// </summary>
    public static void DownloadWebData()
    {
      var client = new WebClient();
      string dataUri = "http://to_file";

      // An I/O-bound operation, no concurrency
      // Blocks current or calling thread until the file is downloaded
      var dataBuffer = client.DownloadData(dataUri);
      string data = Encoding.ASCII.GetString(dataBuffer);

      Console.WriteLine($@"Downloaded file path is ""{data}"".");
    }

    /// <summary>
    /// Download web data, an IO-bound with concurrency
    /// </summary>
    public static async Task DownloadWebDataAsync()
    {
      var client = new WebClient();

      string dataUri = "http://to_file";

      // An I/O-bound operation, with concurrency
      // Does not block current or calling thread, yields other operations to continue
      var dataBuffer = await client.DownloadDataTaskAsync(new Uri(dataUri));
      // once the task had completed, results are marshelled back to calling thread 
      // execution continues
      string data = Encoding.ASCII.GetString(dataBuffer);

      Console.WriteLine($@"Downloaded file path is ""{data}"".");
    }
  }
}

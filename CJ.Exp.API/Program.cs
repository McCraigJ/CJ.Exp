using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CJ.Exp.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // NLog: setup the logger first to catch all errors
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
              logging.ClearProviders();
              logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseNLog()
            .Build();
  }
}

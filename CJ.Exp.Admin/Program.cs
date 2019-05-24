using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace CJ.Exp.Admin
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // NLog: setup the logger first to catch all errors
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      try
      {
        BuildWebHost(args).Run();
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Stopped program because of exception");
      }
      finally
      {
        NLog.LogManager.Shutdown();
      }

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

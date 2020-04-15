using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog.Web;
using System;
using System.IO;

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
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .ConfigureAppConfiguration((hostBuildingCtxt, configBuilder) =>
                    {
                        configBuilder.Sources.Clear();

                        configBuilder
                            .AddEnvironmentVariables()
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{hostBuildingCtxt.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    })
                    .ConfigureKestrel((hostBuilderCtxt, opt) =>
                    {
                        int listenPort = Convert.ToInt32(hostBuilderCtxt.Configuration["HostPort"]);
                        opt.ListenAnyIP(listenPort);
                    })
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();

                host.Run();

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
    }
}

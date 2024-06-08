using Ebd.Presentation.Api.Extensions;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace Ebd.Presentation.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                .ConfigureLogging((_, builder) =>
                {
                    ConfigureLog(builder);
                });
            });

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((ctx, builder) =>
        {
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", true, true);
            builder.AddEnvironmentVariables();
        })
        .UseStartup<Startup>()
        .ConfigureLogging((_, builder) =>
           {
               ConfigureLog(builder);
           })
        .Build();

    private static void ConfigureLog(ILoggingBuilder builder)
    {
        var log4NetConfig = new FileInfo("log4net.config");
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, log4NetConfig);

        builder.SetMinimumLevel(LogLevel.Trace);
        builder.AddLog4Net();
        builder.AddColorConsoleLogger(configuration =>
        {
            configuration.LogLevels.Add(LogLevel.Warning, ConsoleColor.DarkYellow);
            configuration.LogLevels.Add(LogLevel.Error, ConsoleColor.DarkMagenta);
            configuration.LogLevels.Add(LogLevel.Critical, ConsoleColor.Red);
        });
    }
}

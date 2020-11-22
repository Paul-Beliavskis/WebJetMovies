using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebJetMovies.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Host failed unexpectedly ", ex);
            }
        }

        protected static IConfiguration Configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("Api/appsettings.json", true, true)
        .AddJsonFile($"Api/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
        .AddJsonFile("Api/appsettings.Local.json", true)
        .AddEnvironmentVariables()
        .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    // use the configuration settings from appsettings.json
                    .UseConfiguration(Configuration).UseSerilog((context, loggerConfiguration) =>
                    {
                        //Ensure the logger resource is flushed when app is unloaded
                        AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

                        loggerConfiguration.WriteTo.Console();
                    })

                    .UseStartup<Startup>().CaptureStartupErrors(false); //This will ensure errors in the startup get propagated up instead of the default .net core behaviour;
                });
    }
}
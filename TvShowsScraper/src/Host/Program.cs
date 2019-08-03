using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using TvShowsScraper.Services;
using TvShowsScraper.Services.External;

namespace TvShowsScraper.Host
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<Runner>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            services.AddScoped<IScrapeService, ScrapeService>();
            services.AddScoped<IImportService, ImportService>();
            
            AddHttpClients(services, configuration);

            AddLogging(services, configuration);

            services.AddTransient<Runner>();
            
            return services;
        }

        private static void AddLogging(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddConfiguration(configuration.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });
        }

        private static void AddHttpClients(IServiceCollection services, IConfiguration configuration)
        {
            var tvMazeApiUrl = configuration.GetSection("TvMazeApiUrl").Value;
            var tvShowsApiUrl = configuration.GetSection("TvShowsApiUrl").Value;

            services
                .AddHttpClient<ITvMazeClient, TvMazeClient>(client =>
                {
                    client.BaseAddress = new Uri(tvMazeApiUrl);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .AddPolicyHandler(WaitAndRetryForeverPolicyAsync())
                .AddPolicyHandler(GetPolicyAsync());
            
            services
                .AddHttpClient<ITvShowsClient, TvShowsClient>(client =>
                {
                    client.BaseAddress = new Uri(tvShowsApiUrl);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .AddPolicyHandler(WaitAndRetryForeverPolicyAsync())
                .AddPolicyHandler(GetPolicyAsync());
        }

        private static IAsyncPolicy<HttpResponseMessage> WaitAndRetryForeverPolicyAsync()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(message => message.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryForeverAsync(
                    retryAttempt =>
                    TimeSpan.FromSeconds(10));
        }
        
        private static IAsyncPolicy<HttpResponseMessage> GetPolicyAsync()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => 
                    TimeSpan.FromSeconds(10));
        }
    }
}
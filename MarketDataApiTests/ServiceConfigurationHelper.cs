using System;
using System.IO;
using MarketDataApi.Config.Deribit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketDataApiTests
{
    public class ServiceConfigurationHelper
    {
        private static IConfiguration BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            return configuration;
        }

        public static T GetMockService<T>()
        {
            var configuration = BuildConfiguration();
            var serviceCollection = ConfigureProvider(configuration);
            return GetService<T>(serviceCollection);
        }

        public static IServiceCollection ConfigureProvider(IConfiguration configuration)
        {
            IServiceCollection services = new ServiceCollection();
            RegisterConfigurations(configuration, services);

            return services;
        }

        private static void RegisterConfigurations(IConfiguration configuration, IServiceCollection services)
        {
            DeribitConfig deribitConfig = new DeribitConfig();
            configuration.Bind("Deribit", deribitConfig);
            services.AddSingleton(deribitConfig);
        }

        private static T GetService<T>(IServiceCollection services)
        {
            var servicesProvider = services.BuildServiceProvider();
            return servicesProvider.GetService<T>() ?? throw new InvalidOperationException("Failed to find required service");
        }
    }
}

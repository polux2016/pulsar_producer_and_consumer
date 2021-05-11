using CustomProject.Pulsar.Concept.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomProject.Pulsar.Concept.Helpers
{
	public static class ServiceRegistryHelper
	{
		public static IHostBuilder ConfigureDefaultHostBuilder()
		{
			var host = Host
				.CreateDefaultBuilder()
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
				})
				.ConfigureServicesForCompaniesAssembly();

			return host;
		}

		public static void AddPulsarConceptServices(this IServiceCollection services)
		{
			services.AddTransient<IPulsarClientFactory, PulsarClientFactory>();
			services.AddSingleton<IPulsarSettings, PulsarSettings>();
			services.AddSingleton<IPulsarConfigurationBuilder, PulsarConfigurationBuilder>();
		}

		private static IHostBuilder ConfigureServicesForCompaniesAssembly(this IHostBuilder hostBuilder)
		{
			hostBuilder.ConfigureServices(AddPulsarConceptServices);

			return hostBuilder;
		}
	}
}

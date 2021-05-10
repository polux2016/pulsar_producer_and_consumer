using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace CustomProject.Pulsar.Concept.Helpers
{
	public static class ServiceRegistryHelper
	{
		private const string CompanyKey = "CustomProject";

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

		private static void AddServicesForCompaniesAssembly(IServiceCollection services)
		{
			services.AddServicesOfAllTypes(scanAssembliesStartsWith: new[] { CompanyKey });
		}

		private static IHostBuilder ConfigureServicesForCompaniesAssembly(this IHostBuilder hostBuilder)
		{
			hostBuilder.ConfigureServices(AddServicesForCompaniesAssembly);

			return hostBuilder;
		}
	}
}

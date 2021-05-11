using CustomProject.Pulsar.Concept.Helpers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CustomProject.Producer.FunctionExample.Startup))]

namespace CustomProject.Producer.FunctionExample
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			builder.Services.AddPulsarConceptServices();
		}
	}
}

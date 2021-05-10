using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IPulsarConfigurationBuilder : IPulsarNativeClientProvider, ISingletonService
	{
	}
}

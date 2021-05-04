namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IConfigurationBuilder : IPulsarNativeClientProvider
	{
		IPulsarClientProxy DefaultBuild();
	}
}

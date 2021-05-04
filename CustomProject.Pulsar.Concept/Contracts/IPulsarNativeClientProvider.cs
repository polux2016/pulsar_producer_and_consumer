using DotPulsar.Abstractions;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IPulsarNativeClientProvider
	{
		IPulsarClient PulsarNativeClient { get; }
	}
}

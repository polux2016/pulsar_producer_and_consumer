using CustomProject.Pulsar.Contracts;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IPulsarClientProxy
	{
		IPulsarClientProxy SetDefaultPulsarSettings(IPulsarSettings pulsarSettings);

		IProducerAdapter<TMessage> NewDefaultProducer<TMessage>() where TMessage : ITopicMessage;

		IConsumerAdapter NewDefaultConsumer<TMessage>(string subscription) where TMessage : ITopicMessage;

		IReaderAdapter NewDefaultReader<TMessage>() where TMessage : ITopicMessage;
	}
}

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IPulsarClientFactory
	{
		IProducerAdapter<T> NewDefaultProducer<T>() where T : ITopicMessage;

		IConsumerAdapter<T> NewDefaultConsumer<T>(string subscription) where T : ITopicMessage;

		IReaderAdapter<T> NewDefaultReader<T>() where T : ITopicMessage;
	}
}

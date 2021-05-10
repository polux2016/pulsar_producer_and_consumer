using CustomProject.Pulsar.Concept.Contracts;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;

namespace CustomProject.Pulsar.Concept
{
	public class PulsarClientFactory : IPulsarClientFactory
	{
		private readonly IPulsarClient _pulsarNativeClient;

		private readonly IPulsarSettings _pulsarSettings;

		public PulsarClientFactory(IPulsarConfigurationBuilder configurationBuilder,
			IPulsarSettings pulsarSettings)
		{
			_pulsarNativeClient = configurationBuilder.PulsarNativeClient;
			_pulsarSettings = pulsarSettings;
		}

		public IProducerAdapter<T> NewDefaultProducer<T>() where T : ITopicMessage
		{
			var nativeProducer = _pulsarNativeClient.NewProducer()
				.Topic(GetFullTopicName<T>())
				.Create();

			return new ProducerAdapter<T>(nativeProducer);
		}

		public IConsumerAdapter<T> NewDefaultConsumer<T>(string subscription)
			where T : ITopicMessage
		{
			var nativeConsumer = _pulsarNativeClient.NewConsumer()
				.SubscriptionName(subscription)
				.Topic(GetFullTopicName<T>())
				.Create();

			return new ConsumerAdapter<T>(nativeConsumer);
		}

		public IReaderAdapter<T> NewDefaultReader<T>() where T : ITopicMessage
		{
			var nativeReader = _pulsarNativeClient.NewReader()
				.StartMessageId(MessageId.Earliest)
				.Topic(GetFullTopicName<T>())
				.Create();

			return new ReaderAdapter<T>(nativeReader);
		}

		private string GetFullTopicName<T>()
		{
			var persistence = _pulsarSettings.IsPersistent ? "persistent" : "non-persistent";

			var topic = GetTopicName<T>();

			return $"{persistence}://{_pulsarSettings.Tenant}/{_pulsarSettings.Namespace}/{topic}";
		}

		private string GetTopicName<T>()
		{
			return typeof(T).Name.ToLower();
		}
	}
}

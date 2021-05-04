using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Contracts;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;

namespace CustomProject.Pulsar.Concept
{
	public class PulsarClientProxy : IPulsarClientProxy
	{
		private readonly IPulsarClient _pulsarNativeClient;

		private IPulsarSettings _pulsarSettings;

		public PulsarClientProxy(IConfigurationBuilder configurationBuilder,
			IPulsarSettings pulsarSettings)
		{
			_pulsarNativeClient = configurationBuilder.PulsarNativeClient;
			_pulsarSettings = pulsarSettings;
		}

		public IPulsarClientProxy SetDefaultPulsarSettings(IPulsarSettings pulsarSettings)
		{
			_pulsarSettings = pulsarSettings;
			return this;
		}

		public IProducerAdapter<TMessage> NewDefaultProducer<TMessage>() where TMessage : ITopicMessage
		{
			var nativeProducer = _pulsarNativeClient.NewProducer()
				.Topic(GetFullTopicName<TMessage>())
				.Create();

			return new ProducerAdapter<TMessage>(nativeProducer);
		}

		public IConsumerAdapter NewDefaultConsumer<TMessage>(string subscription)
			where TMessage : ITopicMessage
		{
			var nativeConsumer = _pulsarNativeClient.NewConsumer()
				.SubscriptionName(subscription)
				.Topic(GetFullTopicName<TMessage>())
				.Create();

			return new ConsumerAdapter(nativeConsumer);
		}

		public IReaderAdapter NewDefaultReader<TMessage>() where TMessage : ITopicMessage
		{
			var nativeReader = _pulsarNativeClient.NewReader()
				.StartMessageId(MessageId.Earliest)
				.Topic(GetFullTopicName<TMessage>())
				.Create();

			return new ReaderAdapter(nativeReader);
		}

		private string GetFullTopicName<TMessage>()
		{
			var persistence = _pulsarSettings.IsPersistent ? "persistent" : "non-persistent";

			var topic = GetTopicName<TMessage>();

			return $"{persistence}://{_pulsarSettings.Tenant}/{_pulsarSettings.Namespace}/{topic}";
		}

		private string GetTopicName<TMessage>()
		{
			return typeof(TMessage).Name.ToLower();
		}
	}
}

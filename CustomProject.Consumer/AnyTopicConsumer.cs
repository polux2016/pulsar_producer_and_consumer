using CustomProject.Pulsar.Concept;
using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Contracts.TopicMessages;
using Microsoft.Extensions.Logging;

namespace CustomProject.Consumer
{
	public class AnyTopicConsumer : PulsarConsumerBackgroundService<AnyTopicMessage>
	{
		private readonly ILogger<AnyTopicConsumer> _logger;

		protected override string GetSubscription()
		{
			return "Subscription";
		}

		public AnyTopicConsumer(IPulsarClientFactory pulsarClientFactory,
			ILogger<AnyTopicConsumer> logger) : base(pulsarClientFactory, logger)
		{
			_logger = logger;
		}

		protected override void Handler(AnyTopicMessage message)
		{
			_logger.LogInformation($"[{nameof(AnyTopicMessage)}]. Message handled.");
		}
	}
}

using CustomProject.Pulsar.Concept;
using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Contracts.TopicMessages;
using Microsoft.Extensions.Logging;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace CustomProject.Reader
{
	[HostedService]
	public class AnyTopicReader : PulsarReaderBackgroundService<AnyTopicMessage>
	{
		private readonly ILogger<AnyTopicReader> _logger;

		public AnyTopicReader(IPulsarClientFactory pulsarClientFactory,
			ILogger<AnyTopicReader> logger) : base(pulsarClientFactory, logger)
		{
			_logger = logger;
		}

		protected override void Handler(AnyTopicMessage message)
		{
			_logger.LogInformation($"[{nameof(AnyTopicMessage)}]. Message read.");
		}
	}
}

using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using CustomProject.Pulsar.Concept.Contracts;
using Microsoft.Extensions.Logging;

namespace CustomProject.Pulsar.Concept
{
	public abstract class PulsarConsumerBackgroundService<T> : BackgroundService where T : ITopicMessage
	{
		private readonly IPulsarClientFactory _pulsarClientFactory;

		private readonly ILogger _logger;

		protected abstract string GetSubscription();

		protected abstract void Handler(T message);

		protected PulsarConsumerBackgroundService(IPulsarClientFactory pulsarClientFactory,
			ILogger logger)
		{
			_pulsarClientFactory = pulsarClientFactory;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var consumerAdapter = _pulsarClientFactory.NewDefaultConsumer<T>(GetSubscription());

			await foreach (var messageDto in consumerAdapter.Messages(stoppingToken))
			{
				try
				{
					Handler(messageDto.Message);

					await consumerAdapter.AcknowledgeCumulative(messageDto.PulsarMessageId, stoppingToken);

					_logger.LogDebug($"Message '{messageDto.PulsarMessageId}' was consumed");
				}
				catch(Exception exception)
				{
					_logger.LogError($"Message '{messageDto.PulsarMessageId}' wasn't consumed", exception);

					throw;
				}
			}
		}
    }
}

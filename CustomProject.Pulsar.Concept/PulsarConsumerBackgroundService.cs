using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using CustomProject.Pulsar.Concept.Contracts;

namespace CustomProject.Pulsar.Concept
{
	public abstract class PulsarConsumerBackgroundService<T> : BackgroundService where T : ITopicMessage
	{
		private readonly IPulsarClientFactory _pulsarClientFactory;

		protected abstract string GetSubscription();

		protected abstract void Handler(T message);

		protected PulsarConsumerBackgroundService(IPulsarClientFactory pulsarClientFactory)
		{
			_pulsarClientFactory = pulsarClientFactory;
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

					Console.WriteLine($"Message '{messageDto.PulsarMessageId}' was consumed");
				}
				catch
				{
					Console.WriteLine($"Message '{messageDto.PulsarMessageId}' wasn't consumed");
					throw;
				}
			}
		}
    }
}

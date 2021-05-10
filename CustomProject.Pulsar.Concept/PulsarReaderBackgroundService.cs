using CustomProject.Pulsar.Concept.Contracts;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomProject.Pulsar.Concept
{
	public abstract class PulsarReaderBackgroundService<T> : BackgroundService where T : ITopicMessage
	{
		private readonly IPulsarClientFactory _pulsarClientFactory;

		protected abstract void Handler(T message);

		protected PulsarReaderBackgroundService(IPulsarClientFactory pulsarClientFactory)
		{
			_pulsarClientFactory = pulsarClientFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var readerAdapter = _pulsarClientFactory.NewDefaultReader<T>();

			await foreach (var messageDto in readerAdapter.Messages(stoppingToken))
			{
				try
				{
					Handler(messageDto.Message);

					Console.WriteLine($"Message '{messageDto.PulsarMessageId}' was read");
				}
				catch
				{
					Console.WriteLine($"Message '{messageDto.PulsarMessageId}' wasn't read");
					throw;
				}
			}
		}
	}
}

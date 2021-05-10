using CustomProject.Pulsar.Concept.Contracts;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CustomProject.Pulsar.Concept
{
	public abstract class PulsarReaderBackgroundService<T> : BackgroundService where T : ITopicMessage
	{
		private readonly IPulsarClientFactory _pulsarClientFactory;

		private readonly ILogger _logger;

		protected abstract void Handler(T message);

		protected PulsarReaderBackgroundService(IPulsarClientFactory pulsarClientFactory, 
			ILogger logger)
		{
			_pulsarClientFactory = pulsarClientFactory;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var readerAdapter = _pulsarClientFactory.NewDefaultReader<T>();

			await foreach (var messageDto in readerAdapter.Messages(stoppingToken))
			{
				try
				{
					Handler(messageDto.Message);

					_logger.LogDebug($"Message '{messageDto.PulsarMessageId}' was read");
				}
				catch(Exception exception)
				{ 
					_logger.LogError($"Message '{messageDto.PulsarMessageId}' wasn't read", exception);
					throw;
				}
			}
		}
	}
}

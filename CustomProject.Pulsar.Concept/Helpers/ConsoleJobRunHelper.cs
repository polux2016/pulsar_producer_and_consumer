using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomProject.Pulsar.Concept.Helpers
{
	public static class ConsoleJobRunHelper
	{
		public static async Task RunConsumerAsync<TMessage>(
			IConsumerAdapter consumerAdapter,
			Action<TMessage> handler) where TMessage : ITopicMessage
		{
			var cts = GetCancellationTokenSource<TMessage>();

			await foreach (var messageDto in consumerAdapter.Messages<TMessage>(cts.Token))
			{
				Console.WriteLine("Message was consumed");
				try
				{
					handler(messageDto.Message);

					await consumerAdapter.AcknowledgeCumulative(messageDto.MessageId, cts.Token);
				}
				catch
				{
					throw;
				}
			}
		}

		public static async Task RunReaderAsync<TMessage>(
			IReaderAdapter readerAdapter,
			Action<TMessage> handler) where TMessage : ITopicMessage
		{
			var cts = GetCancellationTokenSource<TMessage>();

			await foreach (var message in readerAdapter.Messages<TMessage>(cts.Token))
			{
				Console.WriteLine("Message was read");
				handler(message);
			}
		}

		private static CancellationTokenSource GetCancellationTokenSource<TMessage>() where TMessage : ITopicMessage
		{
			var cts = new CancellationTokenSource();
			Console.CancelKeyPress += (s, e) =>
			{
				Console.WriteLine("Canceling...");
				cts.Cancel();
				e.Cancel = true;
			};
			return cts;
		}
	}
}

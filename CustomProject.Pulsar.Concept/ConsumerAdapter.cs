using CustomProject.Pulsar.Concept.Contracts;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CustomProject.Pulsar.Concept
{
	public class ConsumerAdapter<T> : IConsumerAdapter<T> where T : ITopicMessage
	{
		private readonly IConsumer<ReadOnlySequence<byte>> _nativeConsumer;

		internal ConsumerAdapter(IConsumer<ReadOnlySequence<byte>> nativeConsumer)
		{
			_nativeConsumer = nativeConsumer;
		}

		public ValueTask AcknowledgeCumulative(MessageIdProxy pulsarMessageId,
			CancellationToken cancellationToken = default)
		{
			return _nativeConsumer.AcknowledgeCumulative(pulsarMessageId.MessageId, cancellationToken);
		}

		public async IAsyncEnumerable<TopicMessageDto<T>> Messages(
			[EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			await foreach (var message in _nativeConsumer.Messages(cancellationToken))
			{
				var encodedMessage = Encoding.UTF8.GetString(message.Data.ToArray());

				yield return new TopicMessageDto<T>()
				{
					Message = JsonSerializer.Deserialize<T>(encodedMessage),
					PulsarMessageId = new MessageIdProxy { MessageId = message.MessageId }
				};
			}
		}
	}
}

using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using CustomProject.Pulsar.Concept.Contracts;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CustomProject.Pulsar.Contracts;
using DotPulsar;

namespace CustomProject.Pulsar.Concept
{
	public class ConsumerAdapter : IConsumerAdapter
	{
		private readonly IConsumer<ReadOnlySequence<byte>> _nativeConsumer;

		public ConsumerAdapter(IConsumer<ReadOnlySequence<byte>> nativeConsumer)
		{
			_nativeConsumer = nativeConsumer;
		}

		public ValueTask AcknowledgeCumulative(MessageId messageId,
			CancellationToken cancellationToken = default)
		{
			return _nativeConsumer.AcknowledgeCumulative(messageId, cancellationToken);
		}

		public async IAsyncEnumerable<TopicMessageDto<TMessage>> Messages<TMessage>(
			[EnumeratorCancellation] CancellationToken cancellationToken = default) 
			where TMessage: ITopicMessage
		{
			await foreach (var message in _nativeConsumer.Messages(cancellationToken))
			{
				var encodedMessage = Encoding.UTF8.GetString(message.Data.ToArray());

				yield return new TopicMessageDto<TMessage>() {
					Message = JsonSerializer.Deserialize<TMessage>(encodedMessage),
					MessageId = message.MessageId
				};
			}
		}
	}
}

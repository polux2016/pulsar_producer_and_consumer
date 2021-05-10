using CustomProject.Pulsar.Concept.Contracts;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace CustomProject.Pulsar.Concept
{
	public class ReaderAdapter<T> : IReaderAdapter<T> where T : ITopicMessage
	{
		private readonly IReader<ReadOnlySequence<byte>> _nativeReader;

		internal ReaderAdapter(IReader<ReadOnlySequence<byte>> nativeReader)
		{
			_nativeReader = nativeReader;
		}

		public async IAsyncEnumerable<TopicMessageDto<T>> Messages(
			[EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			await foreach (var message in _nativeReader.Messages(cancellationToken))
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

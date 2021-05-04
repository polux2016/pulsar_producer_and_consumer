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
	public class ReaderAdapter : IReaderAdapter
	{
		private readonly IReader<ReadOnlySequence<byte>> _nativeReader;

		public ReaderAdapter(IReader<ReadOnlySequence<byte>> nativeReader)
		{
			_nativeReader = nativeReader;
		}

		public async IAsyncEnumerable<TMessage> Messages<TMessage>(
			[EnumeratorCancellation] CancellationToken cancellationToken = default)
		{
			await foreach (var message in _nativeReader.Messages(cancellationToken))
			{
				var encodedMessage = Encoding.UTF8.GetString(message.Data.ToArray());

				var result = JsonSerializer.Deserialize<TMessage>(encodedMessage);

				yield return result;
			}
		}
	}
}

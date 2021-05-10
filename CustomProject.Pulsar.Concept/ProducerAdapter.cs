using CustomProject.Pulsar.Concept.Contracts;
using DotPulsar.Abstractions;
using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomProject.Pulsar.Concept
{
	public class ProducerAdapter<T> : IProducerAdapter<T> where T : ITopicMessage
	{
		private readonly IProducer<ReadOnlySequence<byte>> _nativeProducer;

		internal ProducerAdapter(IProducer<ReadOnlySequence<byte>> nativeProducer)
		{
			_nativeProducer = nativeProducer;
		}

		public async Task<MessageIdProxy> Send(T message)
		{
			var serializedMessage = JsonSerializer.Serialize(message);

			var dataBytes = Encoding.UTF8.GetBytes(serializedMessage);

			var data = new ReadOnlySequence<byte>(dataBytes);

			return new MessageIdProxy()
			{
				MessageId = await _nativeProducer.Send(data)
			};
		}
	}
}

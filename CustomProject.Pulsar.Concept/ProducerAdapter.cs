using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Contracts;
using DotPulsar.Abstractions;
using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DotPulsar;

namespace CustomProject.Pulsar.Concept
{
	public class ProducerAdapter<TMessage> : IProducerAdapter<TMessage> where TMessage : ITopicMessage
	{
		private readonly IProducer<ReadOnlySequence<byte>> _nativeProducer;

		public ProducerAdapter(IProducer<ReadOnlySequence<byte>> nativeProducer)
		{
			_nativeProducer = nativeProducer;
		}

		public async Task<MessageId> Send(TMessage message)
		{
			var serializedMessage = JsonSerializer.Serialize(message);

			var dataBytes = Encoding.UTF8.GetBytes(serializedMessage);

			var data = new ReadOnlySequence<byte>(dataBytes);

			return await _nativeProducer.Send(data);
		}
	}
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomProject.Pulsar.Contracts;
using DotPulsar;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IConsumerAdapter
	{
		IAsyncEnumerable<TopicMessageDto<TMessage>> Messages<TMessage>(CancellationToken cancellationToken = default)
			where TMessage : ITopicMessage;

		ValueTask AcknowledgeCumulative(MessageId messageId, CancellationToken cancellationToken = default);
	}
}

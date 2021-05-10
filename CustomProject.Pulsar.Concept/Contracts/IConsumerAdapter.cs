using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IConsumerAdapter<T> where T : ITopicMessage
	{
		IAsyncEnumerable<TopicMessageDto<T>> Messages(CancellationToken cancellationToken = default);

		ValueTask AcknowledgeCumulative(MessageIdProxy messageId, CancellationToken cancellationToken = default);
	}
}

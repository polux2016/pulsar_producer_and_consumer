using System.Collections.Generic;
using System.Threading;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IReaderAdapter<T> where T : ITopicMessage
	{
		IAsyncEnumerable<TopicMessageDto<T>> Messages(CancellationToken cancellationToken = default);
	}
}

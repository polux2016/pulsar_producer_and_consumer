using System.Collections.Generic;
using System.Threading;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IReaderAdapter
	{
		IAsyncEnumerable<TMessage> Messages<TMessage>(CancellationToken cancellationToken = default);
	}
}

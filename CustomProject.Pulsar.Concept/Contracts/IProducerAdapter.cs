using System.Threading.Tasks;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IProducerAdapter<in T> where T : ITopicMessage
	{
		Task<MessageIdProxy> Send(T message);
	}
}

using System.Threading.Tasks;
using CustomProject.Pulsar.Contracts;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IProducerAdapter<in TMessage> where TMessage : ITopicMessage
	{
		Task<DotPulsar.MessageId> Send(TMessage message);
	}
}

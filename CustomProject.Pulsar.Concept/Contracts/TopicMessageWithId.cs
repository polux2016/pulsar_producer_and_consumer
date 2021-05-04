using CustomProject.Pulsar.Contracts;
using DotPulsar;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public class TopicMessageDto<TMessage> where TMessage: ITopicMessage
	{
		public TMessage Message { get; set; }
		
		internal MessageId MessageId { get; set; }
	}
}

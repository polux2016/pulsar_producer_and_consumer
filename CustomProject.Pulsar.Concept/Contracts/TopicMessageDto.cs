namespace CustomProject.Pulsar.Concept.Contracts
{
	public class TopicMessageDto<T> where T : ITopicMessage
	{
		public T Message { get; init; }

		internal MessageIdProxy PulsarMessageId { get; init; }
	}
}

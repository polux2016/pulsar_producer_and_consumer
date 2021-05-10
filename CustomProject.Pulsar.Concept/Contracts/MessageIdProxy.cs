using DotPulsar;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public class  MessageIdProxy
	{
		internal MessageId MessageId { get; init; }

		public override string ToString()
		{
			return MessageId.ToString();
		}
	}
}

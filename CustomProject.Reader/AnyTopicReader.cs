using CustomProject.Pulsar.Concept;
using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Contracts.TopicMessages;
using System;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace CustomProject.Reader
{
	[HostedService]
	public class AnyTopicReader : PulsarReaderBackgroundService<AnyTopicMessage>
	{
		public AnyTopicReader(IPulsarClientFactory pulsarClientFactory) : base(pulsarClientFactory)
		{
		}

		protected override void Handler(AnyTopicMessage message)
		{
			Console.WriteLine($"[{nameof(AnyTopicMessage)}]. Message read.");
		}
	}
}

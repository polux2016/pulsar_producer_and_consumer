using CustomProject.Pulsar.Concept;
using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Contracts.TopicMessages;
using System;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace CustomProject.Consumer
{
	[HostedService]
	public class AnyTopicConsumer : PulsarConsumerBackgroundService<AnyTopicMessage>
	{
		protected override string GetSubscription()
		{
			return "Subscription";
		}

		public AnyTopicConsumer(IPulsarClientFactory pulsarClientFactory) : base(pulsarClientFactory)
		{
		}

		protected override void Handler(AnyTopicMessage message)
		{
			Console.WriteLine($"[{nameof(AnyTopicMessage)}]. Message handled.");
		}
	}
}

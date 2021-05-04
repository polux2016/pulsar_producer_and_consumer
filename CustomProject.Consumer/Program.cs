using System;
using System.Threading.Tasks;
using Autofac;
using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Concept.Helpers;
using CustomProject.Pulsar.Contracts.TopicMessages;

namespace CustomProject.Consumer
{
	internal static class Program
	{
		private static void Main()
		{
			MainAsync().GetAwaiter().GetResult();
		}

		private static async Task MainAsync()
		{
			var container = AgentAutofacConfig.Init();

			var configurationBuilder = container.Resolve<IConfigurationBuilder>();
			configurationBuilder.DefaultBuild();

			var pulsarClientProxy = container.Resolve<IPulsarClientProxy>();
			
			var consumer = pulsarClientProxy.NewDefaultConsumer<AnyTopicMessage>("Subscription");
			
			await ConsoleJobRunHelper.RunConsumerAsync<AnyTopicMessage>(consumer, Handler);

			Console.ReadKey();
		}

		private static void Handler(AnyTopicMessage message)
		{
			Console.WriteLine($"[{nameof(AnyTopicMessage)}]. Message received.");
		}
	}
}

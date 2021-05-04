using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Concept.Helpers;
using CustomProject.Pulsar.Contracts.TopicMessages;

namespace CustomProject.Producer
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

			var producer = pulsarClientProxy.NewDefaultProducer<AnyTopicMessage>();

			for(var numberOfMessages = 10; numberOfMessages > 0; numberOfMessages--)
			{
				var result = await producer.Send(new AnyTopicMessage());
				Console.WriteLine(result);

				Thread.Sleep(2000);
			}
		}
	}
}

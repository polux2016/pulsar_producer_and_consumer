using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Concept.Helpers;
using CustomProject.Pulsar.Contracts.TopicMessages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

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
			var serviceProvider = BuildServiceProvider();

			var pulsarClientFactory = serviceProvider.GetRequiredService<IPulsarClientFactory>();

			var producer = pulsarClientFactory.NewDefaultProducer<AnyTopicMessage>();

			for (var numberOfMessages = 10; numberOfMessages > 0; numberOfMessages--)
			{
				var messageIdProxy = await producer.Send(new AnyTopicMessage());

				Console.WriteLine(messageIdProxy);

				Thread.Sleep(2000);
			}
		}

		private static IServiceProvider BuildServiceProvider()
		{
			var host = ServiceRegistryHelper.ConfigureDefaultHostBuilder()
				.Build();
			return host.Services;
		}
	}
}

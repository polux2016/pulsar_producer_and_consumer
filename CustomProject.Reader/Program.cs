using CustomProject.Pulsar.Concept.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomProject.Reader
{
	internal static class Program
	{
		private static void Main()
		{
			MainAsync().GetAwaiter().GetResult();
		}

		private static async Task MainAsync()
		{
			var token = InitConsoleCancellationTokenSource();

			await ServiceRegistryHelper.ConfigureDefaultHostBuilder()
				.ConfigureServices(services =>
				{
					services.AddHostedService<AnyTopicReader>();
				})
				.RunConsoleAsync(token);
		}

		private static CancellationToken InitConsoleCancellationTokenSource()
		{
			var cts = new CancellationTokenSource();
			Console.CancelKeyPress += (_, e) =>
			{
				Console.WriteLine("Canceling...");
				cts.Cancel();
				e.Cancel = true;
			};
			return cts.Token;
		}
	}
}

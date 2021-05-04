using System;
using DotPulsar;
using DotPulsar.Abstractions;
using CustomProject.Pulsar.Concept.Contracts;

namespace CustomProject.Pulsar.Concept
{
	public class ConsoleConfigurationBuilder: IConfigurationBuilder
	{
		private readonly IPulsarSettings _pulsarSettings;

		private IPulsarClientBuilder _pulsarClientBuilder;
		
		public IPulsarClient PulsarNativeClient { get; private set; }

		public ConsoleConfigurationBuilder(IPulsarSettings pulsarSettings)
		{
			_pulsarSettings = pulsarSettings;
		}
		
		public IPulsarClientProxy DefaultBuild()
		{
			_pulsarClientBuilder ??= PulsarClient.Builder();

			PulsarNativeClient = _pulsarClientBuilder
				.ServiceUrl(GetServiceUri())
				.Build();
			
			return new PulsarClientProxy(this, _pulsarSettings);
		}

		private Uri GetServiceUri()
		{
			return new Uri($"pulsar://{_pulsarSettings.Domain}:{_pulsarSettings.Port}");
		}
	}
}

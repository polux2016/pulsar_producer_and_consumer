using System;
using DotPulsar;
using DotPulsar.Abstractions;
using CustomProject.Pulsar.Concept.Contracts;

namespace CustomProject.Pulsar.Concept
{
	public class PulsarConfigurationBuilder: IPulsarConfigurationBuilder
	{
		private readonly IPulsarSettings _pulsarSettings;
		
		private readonly Lazy<IPulsarClient> _pulsarNativeClientLazy;

		public IPulsarClient PulsarNativeClient => _pulsarNativeClientLazy.Value;

		public PulsarConfigurationBuilder(IPulsarSettings pulsarSettings)
		{
			_pulsarSettings = pulsarSettings;

			_pulsarNativeClientLazy = new Lazy<IPulsarClient>(BuildPulsarClientViaDefaultSettings);
		}
		
		private IPulsarClient BuildPulsarClientViaDefaultSettings()
		{
			return PulsarClient.Builder()
				.ServiceUrl(GetServiceUri())
				.Build();
		}

		private Uri GetServiceUri()
		{
			return new Uri($"pulsar://{_pulsarSettings.Domain}:{_pulsarSettings.Port}");
		}
	}
}

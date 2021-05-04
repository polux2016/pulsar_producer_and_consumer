using CustomProject.Pulsar.Concept.Contracts;
using CustomProject.Pulsar.Concept.Helpers;

namespace CustomProject.Pulsar.Concept
{
	public class PulsarSettings : BaseApplicationSettingsProvider, IPulsarSettings
	{
		public string Domain => GetFromDefaultSettings("Domain");

		public int Port => int.Parse(GetFromDefaultSettings("Port"));

		public bool IsPersistent => bool.Parse(GetFromDefaultSettings("IsPersistent"));

		public string Tenant => GetFromDefaultSettings("Tenant");

		public string Namespace => GetFromDefaultSettings("Namespace");
	}
}

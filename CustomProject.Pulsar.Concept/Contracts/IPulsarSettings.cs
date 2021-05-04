namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IPulsarSettings
	{
		bool IsPersistent { get; }

		string Tenant { get; }

		string Namespace { get; }

		string Domain { get; }

		int Port { get; }
	}
}

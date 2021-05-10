using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace CustomProject.Pulsar.Concept.Contracts
{
	public interface IPulsarSettings: ISingletonService
	{
		bool IsPersistent { get; }

		string Tenant { get; }

		string Namespace { get; }

		string Domain { get; }

		int Port { get; }
	}
}

using Autofac;
using CustomProject.Pulsar.Concept.Contracts;

namespace CustomProject.Pulsar.Concept
{
	public class IoCModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<PulsarClientProxy>().As<IPulsarClientProxy>();

			builder.RegisterType<ConsoleConfigurationBuilder>().As<IConfigurationBuilder>().SingleInstance();
			builder.RegisterType<PulsarSettings>().As<IPulsarSettings>().SingleInstance();
		}
	}
}

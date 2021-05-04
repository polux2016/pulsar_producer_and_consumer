using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace CustomProject.Pulsar.Concept.Helpers
{
	public static class AgentAutofacConfig
	{
		public static IContainer Init()
		{
			var builder = new ContainerBuilder();
			var callingAssembly = Assembly.GetCallingAssembly();
			var array = callingAssembly.GetReferencedAssemblies()
				.Where((Func<AssemblyName, bool>)(ass => ass.FullName.Contains("Pulsar")))
				.Select(Assembly.Load).ToArray();
			builder.RegisterAssemblyModules(array);
			builder.RegisterAssemblyModules(callingAssembly);
			return builder.Build();
		}
	}
}

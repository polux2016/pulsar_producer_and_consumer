using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CustomProject.Pulsar.Concept.Helpers
{
	public abstract class BaseApplicationSettingsProvider
	{
		private readonly Lazy<IConfigurationSection> _configurationSection;

		private const string AppSettingsJsonFileName = "appsettings.json";
		private const string AzureFunctionLocalSettingsJsonFileName = "local.settings.json";

		protected BaseApplicationSettingsProvider()
		{
			_configurationSection = new Lazy<IConfigurationSection>(InitConfigurationSection);
		}

		private IConfigurationSection InitConfigurationSection()
		{
			var configurationRoot = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName)
				.AddJsonFile(path: AzureFunctionLocalSettingsJsonFileName, optional: true)
				.AddJsonFile(path: AppSettingsJsonFileName, optional: true)
				.AddEnvironmentVariables()
				.Build();

			var settingsSectionName = this.GetType().Name;

			return configurationRoot.GetSection(settingsSectionName);
		}

		protected string GetFromDefaultSettings(string key)
		{
			return _configurationSection.Value[key];
		}
	}
}


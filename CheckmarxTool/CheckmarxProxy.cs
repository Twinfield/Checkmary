using System.Linq;
using CheckmarxTool.CxSDKWebService;
using CheckmarxTool.Models;
using ConfigurationSet = CheckmarxTool.Models.ConfigurationSet;
using Preset = CheckmarxTool.Models.Preset;

namespace CheckmarxTool
{
	class CheckmarxProxy
	{
		readonly ProxySettings settings;
		readonly CheckmarxClient client = new CheckmarxClient();
		string serviceUrl;
		string sessionId;

		public CheckmarxProxy(ProxySettings settings)
		{
			this.settings = settings;
		}

		public void Initialize()
		{
			serviceUrl = client.GetServiceUrl(settings.ResolverUrl);
			sessionId = client.Login(serviceUrl, settings.Username, settings.Password);
		}

		public ProjectSummary[] GetProjectSummaries()
		{
			return client.GetProjectsDisplayData(serviceUrl, sessionId)
				.ToProjectSummaries()
				.ToArray();
		}

		public ProjectSummary FindProjectByName(string name)
		{
			var projects = client.GetProjectsDisplayData(serviceUrl, sessionId);
			return projects
				.FirstOrDefault(i => i.ProjectName == name)
				.ToProjectSummary();
		}

		public ProjectConfiguration GetProjectConfigurationById(long projectId)
		{
			return client.GetProjectConfiguration(serviceUrl, sessionId, projectId);
		}

		public Preset[] GetPresets()
		{
			return client.GetPresets(serviceUrl, sessionId)
				.ToPresets()
				.ToArray();
		}
		public Preset FindPresetByName(string name)
		{
			return client.GetPresets(serviceUrl, sessionId)
				.FirstOrDefault(i => i.PresetName == name)
				.ToPreset();
		}

		public ConfigurationSet[] GetConfigurationSets()
		{
			return client.GetConfigurationSets(serviceUrl, sessionId)
				.ToConfigurationSets()
				.ToArray();
		}

		public ConfigurationSet FindConfigurationSetByName(string name)
		{
			return client.GetConfigurationSets(serviceUrl, sessionId)
				.FirstOrDefault(i => i.ConfigSetName == name)
				.ToConfigurationSet();
		}

		public Scan StartScan(ScanSettings scanSettings)
		{
			return client.Scan(serviceUrl, sessionId, scanSettings.ToCliScanArgs());
		}
	}
}
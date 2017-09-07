using System;
using CheckmarxTool.CxSDKWebService;
using CheckmarxTool.Models;
using ConfigurationSet = CheckmarxTool.CxSDKWebService.ConfigurationSet;
using Preset = CheckmarxTool.CxSDKWebService.Preset;

namespace CheckmarxTool
{
	class CheckmarxClient
	{
		const int LanguageId = 1033;

		readonly CxClientFactory clientFactory = new CxClientFactory();

		public string GetServiceUrl(string resolverUrl)
		{
			var client = clientFactory.CreateResolverClient(resolverUrl);
			var response = client.GetWebServiceUrl(CxWsResolver.CxClientType.SDK, 1);
			GuardResponse(response);
			return response.ServiceURL;
		}

		public string Login(string serviceUrl, string username, string password)
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var credentials = new Credentials { User = username, Pass = password };
			var response = client.Login(credentials, LanguageId);
			GuardResponse(response);
			return response.SessionId;
		}

		public ProjectDisplayData[] GetProjectsDisplayData(string serviceUrl, string sessionId)
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetProjectsDisplayData(sessionId);
			GuardResponse(response);
			return response.projectList;
		}

		public ProjectConfiguration GetProjectConfiguration(string serviceUrl, string sessionId, long projectId)
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetProjectConfiguration(sessionId, projectId);
			GuardResponse(response);
			return response.ProjectConfig;
		}

		public Preset[] GetPresets(string serviceUrl, string sessionId)
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetPresetList(sessionId);
			GuardResponse(response);
			return response.PresetList;
		}

		public ConfigurationSet[] GetConfigurationSets(string serviceUrl, string sessionId)
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetConfigurationSetList(sessionId);
			GuardResponse(response);
			return response.ConfigSetList;
		}

		public Scan Scan(string serviceUrl, string sessionId, CliScanArgs scanArgs)
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.Scan(sessionId, scanArgs);
			GuardResponse(response);
			return new Scan
			{
				RunId = response.RunId,
				ProjectId = response.ProjectID
			};
		}

		static void GuardResponse(CxWsResolver.CxWSBasicRepsonse response)
		{
			if (!response.IsSuccesfull)
				throw new Exception(response.ErrorMessage);
		}

		static void GuardResponse(CxSDKWebService.CxWSBasicRepsonse response)
		{
			if (!response.IsSuccesfull)
				throw new Exception(response.ErrorMessage);
		}
	}
}
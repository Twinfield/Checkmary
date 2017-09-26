using System;
using Checkmary.CxSDKWebService;

namespace Checkmary.Checkmarx
{
	class CheckmarxSoapClient
	{
		const int LanguageId = 1033;

		readonly ProxySettings settings;
		readonly CxClientFactory clientFactory = new CxClientFactory();

		string serviceUrl;
		string sessionId;

		public CheckmarxSoapClient(ProxySettings settings)
		{
			this.settings = settings;
		}

		public void Login()
		{
			ResolveServiceUrl();

			var client = clientFactory.CreateServiceClient(serviceUrl);
			var credentials = new Credentials { User = settings.Username, Pass = settings.Password };
			var response = client.Login(credentials, LanguageId);
			GuardResponseSuccessfull(response);
			sessionId = response.SessionId;
		}

		void ResolveServiceUrl()
		{
			var client = clientFactory.CreateResolverClient(settings.SoapResolverUrl);
			var rresponse = client.GetWebServiceUrl(CxWsResolver.CxClientType.SDK, 1);
			GuardResponseSuccessfull(rresponse);
			serviceUrl = rresponse.ServiceURL;
		}

		public ProjectDisplayData[] GetProjectsDisplayData()
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetProjectsDisplayData(sessionId);
			GuardResponseSuccessfull(response);
			return response.projectList;
		}

		public ProjectConfiguration GetProjectConfiguration(long projectId)
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetProjectConfiguration(sessionId, projectId);
			GuardResponseSuccessfull(response);
			return response.ProjectConfig;
		}

		public Preset[] GetPresets()
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetPresetList(sessionId);
			GuardResponseSuccessfull(response);
			return response.PresetList;
		}

		public ConfigurationSet[] GetConfigurationSets()
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetConfigurationSetList(sessionId);
			GuardResponseSuccessfull(response);
			return response.ConfigSetList;
		}

		public Group[] GetAssociatedGroups()
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.GetAssociatedGroupsList(sessionId);
			GuardResponseSuccessfull(response);
			return response.GroupList;
		}

		public Scan Scan(CliScanArgs scanArgs)
		{
			var client = clientFactory.CreateServiceClient(serviceUrl);
			var response = client.Scan(sessionId, scanArgs);
			GuardResponseSuccessfull(response);
			return new Scan
			{
				RunId = response.RunId,
				ProjectId = response.ProjectID
			};
		}

		static void GuardResponseSuccessfull(CxWsResolver.CxWSBasicRepsonse response)
		{
			if (!response.IsSuccesfull)
				throw new Exception(response.ErrorMessage);
		}

		static void GuardResponseSuccessfull(CxSDKWebService.CxWSBasicRepsonse response)
		{
			if (!response.IsSuccesfull)
				throw new Exception(response.ErrorMessage);
		}
	}
}
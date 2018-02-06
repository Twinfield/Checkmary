using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;

namespace Checkmary.Checkmarx
{
	class CheckmarxRestClient
	{
		const string TokenKey = "CXCSRFToken";

		readonly ProxySettings settings;
		readonly RestClient restClient;

		string tokenCookie;

		public CheckmarxRestClient(ProxySettings settings)
		{
			this.settings = settings;
			restClient = CreateRestClient(settings.RestApiUrl);
		}

		static RestClient CreateRestClient(string url)
		{
			return new RestClient(url)
			{
				CookieContainer = new CookieContainer()
			};
		}

		public void Login()
		{
			var request = new RestRequest("/auth/login", Method.POST);
			request.AddParameter("userName", settings.Username);
			request.AddParameter("password", settings.Password);

			var response = restClient.Execute(request);
			GuardResponseOk(response);

			var cookies = restClient.CookieContainer.GetCookies(new Uri(settings.RestApiUrl));
			tokenCookie = cookies[TokenKey]?.Value;
		}

		public List<SastScanRequestDTO> GetScanRequests()
		{
			var request = new RestRequest("/sast/scanRequests", Method.GET);
			request.AddHeader(TokenKey, tokenCookie);

			var response = restClient.Execute<List<SastScanRequestDTO>>(request);
			GuardResponseOk(response);

			return response.Data;
		}

		public OsaScanResponse Scan(OsaScanRequestDto scanRequest)
		{
			var request = new RestRequest($"/projects/{scanRequest.ProjectId}/scans", Method.POST)
			{
				RequestFormat = DataFormat.Json
			};
			request.AddHeader(TokenKey, tokenCookie);
			request.AddHeader("Content-Type", "multipart/form-data");
			request.AddFile("OSAZippedSourceCode", scanRequest.ZippedSource, scanRequest.ProjectName + ".zip", "application/x-zip-compressed");
			request.AddParameter("origin", scanRequest.Origin);

			var response = restClient.Execute<OsaScanResponse>(request);
			GuardResponseAccepted(response);

			return response.Data;
		}

		public void DownloadOsaScanReport(DownloadOsaScanReportDto reportDto)
		{
			var request = new RestRequest("/osa/reports", Method.GET);
			request.AddHeader(TokenKey, tokenCookie);
			request.AddParameter("scanId", reportDto.ScanId);
			request.AddParameter("reportFormat", reportDto.ReportFormat);

			var response = restClient.Execute(request);
			GuardResponseOk(response);
			File.WriteAllBytes($"{ConfigurationManager.AppSettings["ReportsPath"]}\\{reportDto.ProjectName}_{reportDto.ScanId}.{reportDto.ReportFormat}", response.RawBytes);
		}

		static void GuardResponseOk(IRestResponse response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
				throw new Exception($"Invalid response status {response.StatusCode}.");
		}

		static void GuardResponseAccepted(IRestResponse response)
		{
			if (response.StatusCode != HttpStatusCode.Accepted)
				throw new Exception($"Invalid response status {response.StatusCode}.");
		}
	}
}
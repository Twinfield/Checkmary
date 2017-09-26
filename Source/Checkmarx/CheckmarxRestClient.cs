using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

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

			var response = restClient.Execute<List< SastScanRequestDTO>>(request);
			GuardResponseOk(response);

			return response.Data;
		}

		static void GuardResponseOk(IRestResponse response)
		{
			if (response.StatusCode != HttpStatusCode.OK)
				throw new Exception($"Invalid response status {response.StatusCode}.");
		}
	}
}
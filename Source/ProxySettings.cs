namespace Checkmary
{
	class ProxySettings
	{
		public string Url { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string SoapResolverUrl => $"{Url}/Cxwebinterface/CxWsResolver.asmx";
		public string RestApiUrl => $"{Url}/cxrestapi";
	}
}
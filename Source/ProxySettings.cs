namespace CheckmarxTool
{
	class ProxySettings
	{
		public string Url { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		public string ResolverUrl => $"{Url}/CxWsResolver.asmx";
	}
}
using CheckmarxTool.CxSDKWebService;
using CheckmarxTool.CxWsResolver;

namespace CheckmarxTool
{
	class CxClientFactory
	{
		public CxWSResolverSoapClient CreateResolverClient(string resolverUrl)
		{
			return new CxWSResolverSoapClient("CxWSResolverSoap", resolverUrl);
		}

		public CxSDKWebServiceSoapClient CreateServiceClient(string serviceUrl)
		{
			return new CxSDKWebServiceSoapClient("CxSDKWebServiceSoap", serviceUrl);
		}
	}
}
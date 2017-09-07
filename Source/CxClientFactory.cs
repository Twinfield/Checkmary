using Checkmary.CxSDKWebService;
using Checkmary.CxWsResolver;

namespace Checkmary
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
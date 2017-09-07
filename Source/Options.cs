using System.Text;
using CommandLine;

namespace Checkmary
{
	class Options
	{
		[Option('u', "username", Required = true, HelpText = "Checkmarx username")]
		public string Username { get; set; }
		[Option('p', "password", Required = true, HelpText = "Checkmarx password")]
		public string Password { get; set; }
		[Option('a', "apiurl", Required = true, HelpText = "Checkmarx API URL. E.g. 'https://<your-server>/Cxwebinterface/'.")]
		public string CheckmarxApiUrl { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			var usage = new StringBuilder();
			usage.AppendLine("Checkmary");
			usage.AppendLine("Command line tool to start Checkmarx scans.");
			return usage.ToString();
		}
	}
}
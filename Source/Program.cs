using System;

namespace Checkmary
{
	class Program
	{
		static void Main(string[] args)
		{
			CommandLine.Parser.Default.ParseArguments(args, new Options(), OnVerbCommand);
		}

		static void OnVerbCommand(string verb, object verbOptions)
		{
			switch (verbOptions)
			{
				case StartScanOptions startScanOptions:
					OnStartScanCommand(startScanOptions);
					break;
				default:
					OnUnknownCommand(verb);
					break;
			}
		}

		static void OnStartScanCommand(StartScanOptions options)
		{
			var proxy = Proxy(options);
			var scanRequest = ScanRequest(options);
			new Scanner(proxy).Scan(scanRequest);
		}

		static ScanRequest ScanRequest(StartScanOptions options)
		{
			var scanRequest = new ScanRequest
			{
				ProjectName = options.ProjectName,
				ProjectPath = options.ProjectPath,
				Preset = options.Preset,
				ConfigurationSet = options.ConfigurationSet,
				SourceCodePath = options.SourceCodePath,
				DryRun = options.DryRun
			};
			return scanRequest;
		}

		static void OnUnknownCommand(string verb)
		{
			Console.WriteLine($"Unknow command '{verb}'.");
		}

		static CheckmarxProxy Proxy(CommonSubOptions options)
		{
			return new CheckmarxProxy(new ProxySettings
			{
				Url = options.CheckmarxApiUrl,
				Username = options.Username,
				Password = options.Password
			});
		}
	}
}

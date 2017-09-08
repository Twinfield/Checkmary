using System;
using System.Text.RegularExpressions;
using Checkmary.Models;

namespace Checkmary
{
	class Program
	{
		static readonly Options Options = new Options();

		static void Main(string[] args)
		{
			if (!CommandLine.Parser.Default.ParseArguments(args, Options))
				return;

			var scanRequest = new ScanRequest
			{
				ProjectName = Options.ProjectName,
				ProjectPath =Options.ProjectPath,
				Preset = Options.Preset,
				ConfigurationSet = Options.ConfigurationSet,
				SourceCodePath = Options.SourceCodePath
			};

			Scan(scanRequest);
			Console.WriteLine("Finished");
		}

		static void Scan(ScanRequest scanRequest)
		{
			var proxy = CreateCheckmarxProxy();
			proxy.Initialize();

			var scanSettings = new ScanSettings();
			scanSettings.ProjectName = scanRequest.ProjectName;
			scanSettings.ProjectPath = scanRequest.ProjectPath;

			Console.WriteLine($"Resolve project {scanRequest.ProjectName}");
			var project = proxy.FindProjectByName(scanRequest.ProjectName);
			scanSettings.ProjectId = project.Id;
			Console.WriteLine($"Project last scanned on {project.LastScanDate}");

			Console.WriteLine($"Resolve preset {scanRequest.Preset}");
			var preset = proxy.FindPresetByName(scanRequest.Preset);
			scanSettings.PresetId = preset.Id;

			Console.WriteLine($"Resolve configuration set {scanRequest.ConfigurationSet}");
			var configurationSet = proxy.FindConfigurationSetByName(scanRequest.ConfigurationSet);
			scanSettings.ConfigurationSetId = configurationSet.Id;

			Console.WriteLine("Collect source code");
			var excludeFileFilter = new Regex(@"[/\\](\.git|\.vs|\.nuget|output|packages|.*\.msi)([/\\]|^)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			scanSettings.ZipFileName = $"{scanRequest.ProjectName}.zip";
			scanSettings.ZipFileContents = ZipHelper.ZipDirectoryToByteArray(scanRequest.SourceCodePath, f => !excludeFileFilter.IsMatch(f));

			Console.WriteLine("Start scan");
			if (Options.DryRun)
			{
				Console.WriteLine("Not starting scan, because dry run is enabled.");
			}
			else
			{
				var scan = proxy.StartScan(scanSettings);
				Console.WriteLine($"Scan of project with ID {scan.ProjectId} started with run ID {scan.RunId}.");
			}
		}

		static CheckmarxProxy CreateCheckmarxProxy()
		{
			return new CheckmarxProxy(new ProxySettings
			{
				Url = Options.CheckmarxApiUrl,
				Username = Options.Username,
				Password = Options.Password
			});
		}
	}
}

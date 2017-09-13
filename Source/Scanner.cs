using System;
using System.Text.RegularExpressions;
using Checkmary.Models;

namespace Checkmary
{
	class Scanner
	{
		readonly CheckmarxProxy proxy;

		public Scanner(CheckmarxProxy proxy)
		{
			this.proxy = proxy;
		}

		public void Scan(ScanRequest request)
		{
			proxy.Initialize();

			var scanSettings = new ScanSettings();
			scanSettings.ProjectName = request.ProjectName;
			scanSettings.ProjectPath = request.ProjectPath;

			var project = proxy.FindProjectByName(request.ProjectName);
			scanSettings.ProjectId = project.Id;
			Console.WriteLine($"Project {request.ProjectName} last scanned on {project.LastScanDate}");

			if (DateTime.Now - project.LastScanDate < TimeSpan.FromDays(request.DaysSinceLastScan))
			{
				Console.WriteLine($"The last scan was less than {request.DaysSinceLastScan} days ago. No new scan will be started.");
				return;
			}

			var preset = proxy.FindPresetByName(request.Preset);
			scanSettings.PresetId = preset.Id;

			var configurationSet = proxy.FindConfigurationSetByName(request.ConfigurationSet);
			scanSettings.ConfigurationSetId = configurationSet.Id;

			Console.WriteLine("Collecting source code...");
			var excludeFileFilter = new Regex(@"[/\\](\.git|\.vs|\.nuget|output|packages|.*\.msi)([/\\]|^)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			scanSettings.ZipFileName = $"{request.ProjectName}.zip";
			scanSettings.ZipFileContents = ZipHelper.ZipDirectoryToByteArray(request.SourceCodePath, f => !excludeFileFilter.IsMatch(f));

			Console.WriteLine("Starting scan...");
			if (request.DryRun)
			{
				Console.WriteLine("Not starting scan, because dry run is enabled.");
			}
			else
			{
				var scan = proxy.StartScan(scanSettings);
				Console.WriteLine($"Scan of project with ID {scan.ProjectId} started with run ID {scan.RunId}.");
			}
		}

	}
}
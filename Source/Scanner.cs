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

			var project = ResolveProject(request, scanSettings);

			Console.WriteLine($"Project {request.ProjectName} last scanned on {project.LastScanDate}.");
			if (DateTime.Now - project.LastScanDate < TimeSpan.FromDays(request.DaysSinceLastScan))
			{
				Console.WriteLine($"The last scan was less than {request.DaysSinceLastScan} days ago. No new scan will be started.");
				return;
			}

			ResolvePreset(request, scanSettings);
			ResolveConfigurationSet(request, scanSettings);
			CollectSourceCode(request, scanSettings);
			StartScan(request, scanSettings);
		}

		ProjectSummary ResolveProject(ScanRequest request, ScanSettings scanSettings)
		{
			var project = proxy.FindProjectByName(request.ProjectName);
			scanSettings.ProjectId = project.Id;
			scanSettings.ProjectName = request.ProjectName;
			scanSettings.TeamName = request.TeamName;
			return project;
		}

		void ResolveConfigurationSet(ScanRequest request, ScanSettings scanSettings)
		{
			var configurationSet = proxy.FindConfigurationSetByName(request.ConfigurationSet);
			scanSettings.ConfigurationSetId = configurationSet.Id;
		}

		void ResolvePreset(ScanRequest request, ScanSettings scanSettings)
		{
			var preset = proxy.FindPresetByName(request.Preset);
			scanSettings.PresetId = preset.Id;
		}

		static void CollectSourceCode(ScanRequest request, ScanSettings scanSettings)
		{
			Console.WriteLine("Collecting source code...");

			var excludeFileFilter = new Regex(@"[/\\](\.git|\.vs|\.nuget|output|packages|.*\.msi)([/\\]|^)",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);

			scanSettings.ZipFileName = $"{request.ProjectName}.zip";
			scanSettings.ZipFileContents = ZipHelper.ZipDirectoryToByteArray(request.SourceCodePath, f => !excludeFileFilter.IsMatch(f));
		}

		void StartScan(ScanRequest request, ScanSettings scanSettings)
		{
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
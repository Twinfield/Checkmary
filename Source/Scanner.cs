using Checkmary.Models;
using Checkmary.Persistence;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Checkmary
{
	class Scanner
	{
		readonly CheckmarxProxy proxy;

		public Scanner(CheckmarxProxy proxy)
		{
			this.proxy = proxy;
		}

		public void Scan(SastScanRequest request)
		{
			proxy.Initialize();

			var scanSettings = new SastScanSettings();

			var project = ResolveProject(request, scanSettings);
			if (project == null)
			{
				Console.WriteLine($"Project with name {request.ProjectName} does not exist on Checkmarx.");
				return;
			}

			Console.WriteLine($"Project {request.ProjectName} last scanned on {project.LastScanDate}.");
			if (DateTime.Now - project.LastScanDate < TimeSpan.FromDays(request.DaysSinceLastScan))
			{
				Console.WriteLine($"The last scan was less than {request.DaysSinceLastScan} days ago. No new scan will be started.");
				return;
			}

			ResolvePreset(request, scanSettings);
			ResolveConfigurationSet(request, scanSettings);
			CollectSourceCode(request, scanSettings);
			StartSastScan(request, scanSettings);
		}

		public void Scan(OsaScanRequest request)
		{
			proxy.Initialize();

			var scanSettings = new OsaScanSettings();

			var project = ResolveProject(request, scanSettings);
			if (project == null)
			{
				Console.WriteLine($"Project with name {request.ProjectName} does not exist on Checkmarx.");
				return;
			}

			CollectSourceCode(request, scanSettings);
			StartOsaScan(request, scanSettings);
		}

		ProjectSummary ResolveProject(ScanRequest request, ScanSettings scanSettings)
		{
			var project = proxy.FindProjectByName(request.ProjectName);
			if (project == null)
				return null;

			scanSettings.ProjectId = project.Id;
			scanSettings.ProjectName = request.ProjectName;
			scanSettings.TeamName = request.TeamName;
			return project;
		}

		void ResolveConfigurationSet(SastScanRequest request, SastScanSettings scanSettings)
		{
			var configurationSet = proxy.FindConfigurationSetByName(request.ConfigurationSet);
			scanSettings.ConfigurationSetId = configurationSet.Id;
		}

		void ResolvePreset(SastScanRequest request, SastScanSettings scanSettings)
		{
			var preset = proxy.FindPresetByName(request.Preset);
			scanSettings.PresetId = preset.Id;
		}

		static void CollectSourceCode(ScanRequest request, ScanSettings scanSettings)
		{
			Console.WriteLine("Collecting source code...");

			var excludeFileFilter = new Regex(@"[/\\](\.git|\.vs|\.nuget|build|packages|.*\.msi|.*\.exe)([/\\]|$)",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);

			scanSettings.ZipFileName = $"{request.ProjectName}.zip";
			scanSettings.ZipFileContents = ZipHelper.ZipDirectoryToByteArray(request.SourceCodePath, f => !excludeFileFilter.IsMatch(f));
		}

		void StartSastScan(SastScanRequest request, SastScanSettings scanSettings)
		{
			Console.WriteLine("Starting scan...");
			if (request.DryRun)
			{
				Console.WriteLine("Not starting scan, because dry run is enabled.");
			}
			else
			{
				var scan = proxy.StartSastScan(scanSettings);
				Console.WriteLine(
					$"Scan of project with ID {scan.ProjectId} and Name {scanSettings.ProjectName} started with run ID {scan.RunId}.");
			}
		}

		void StartOsaScan(OsaScanRequest request, OsaScanSettings scanSettings)
		{
			Console.WriteLine("Starting scan...");
			if (request.DryRun)
			{
				Console.WriteLine("Not starting scan, because dry run is enabled.");
			}
			else
			{
				var scanResponse = proxy.StartOsaScan(scanSettings);
				new ScanIdStore(request.ScanIdsFilePath).Save(scanSettings.ProjectName, scanResponse.ScanId);
				Console.WriteLine(
					$"Scan of project with ID {scanSettings.ProjectId} and Name {scanSettings.ProjectName} started with run ID {scanResponse.ScanId}.");
			}
		}
	}
}
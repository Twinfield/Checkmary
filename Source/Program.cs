using Checkmary.Checkmarx;
using Checkmary.Models;
using Checkmary.Persistence;
using System;
using System.Diagnostics;
using System.Linq;

namespace Checkmary
{
	class Program
	{
		static readonly Options Options = new Options();

		static int Main(string[] args)
		{
			ConfigureExceptionHandler();

			if (!CommandLine.Parser.Default.ParseArgumentsStrict(args, Options, OnVerbCommand))
				return (int)ExitCode.InvalidArgument;

			return (int)ExitCode.Success;
		}

		static void ConfigureExceptionHandler()
		{
			if (!Debugger.IsAttached)
				AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionEventHandler;
		}

		static void OnVerbCommand(string verb, object verbOptions)
		{
			switch (verbOptions)
			{
				case StartSastScanOptions startSastScanOptions:
					OnStartSastScanCommand(startSastScanOptions);
					break;
				case StartOsaScanOptions startOsaScanOptions:
					OnStartOsaScanCommand(startOsaScanOptions);
					break;
				case GetProjectsOptions getProjectOptions:
					OnGetProjects(getProjectOptions);
					break;
				case GetPresetsOptions getPresetsOptions:
					OnGetPresets(getPresetsOptions);
					break;
				case GetConfigurationSetsOptions getConfigurationSetsOptions:
					OnGetConfigurationSets(getConfigurationSetsOptions);
					break;
				case DownloadReportsOptions downloadReportsOptions:
					OnDownloadReports(downloadReportsOptions);
					break;
			}
		}

		static void OnStartSastScanCommand(StartSastScanOptions options)
		{
			new Scanner(Proxy(options)).Scan(new SastScanRequest
			{
				ProjectName = options.Project,
				TeamName = options.Team,
				Preset = options.Preset,
				ConfigurationSet = options.ConfigurationSet,
				SourceCodePath = options.SourceCodePath,
				DaysSinceLastScan = options.DaysSinceLastScan,
				DryRun = options.DryRun
			});
		}

		static void OnStartOsaScanCommand(StartOsaScanOptions options)
		{
			new Scanner(Proxy(options)).Scan(new OsaScanRequest
			{
				ProjectName = options.Project,
				TeamName = options.Team,
				SourceCodePath = options.SourceCodePath,
				ScanIdsFilePath = options.ScanIdsFilePath,
				DryRun = options.DryRun
			});
		}

		static void OnGetProjects(GetProjectsOptions options)
		{
			var proxy = Proxy(options);
			proxy.Initialize();
			var projects = proxy.GetProjectSummaries();
			Console.WriteLine($"Found {projects.Length} projects");
			foreach (var project in projects.OrderBy(p => p.Name))
				Console.WriteLine($"{project.TeamName}, {project.Name}, last scanned on {project.LastScanDate}");
		}

		static void OnGetPresets(GetPresetsOptions options)
		{
			var proxy = Proxy(options);
			proxy.Initialize();
			var presets = proxy.GetPresets();
			Console.WriteLine($"Found {presets.Length} presets");
			foreach (var preset in presets.OrderBy(p => p.Name))
				Console.WriteLine(preset.Name);
		}

		static void OnGetConfigurationSets(GetConfigurationSetsOptions options)
		{
			var proxy = Proxy(options);
			proxy.Initialize();
			var configurationSets = proxy.GetConfigurationSets();
			Console.WriteLine($"Found {configurationSets.Length} configuration sets");
			foreach (var set in configurationSets.OrderBy(s => s.Name))
				Console.WriteLine(set.Name);
		}

		static void OnDownloadReports(DownloadReportsOptions options)
		{
			var proxy = Proxy(options);
			proxy.Initialize();
			var projectScanDetails = new ScanIdStore(options.ScanIdsFilePath).GetScanIds();
			foreach (var scanDetails in projectScanDetails)
			{
				var reportDto = new DownloadOsaScanReportDto
				{
					ScanId = scanDetails.ScanId,
					ReportFormat = options.ReportFormat,
					ProjectName = scanDetails.ProjectName,
					ReportsFolderPath = options.ReportsFolderPath
				};

				Console.WriteLine($"Downloading Osa scan report for project {reportDto.ProjectName}...");
				proxy.DownloadOsaScanReport(reportDto);
			}

			Console.WriteLine($"Downloading Osa scan reports finished.");
		}

		static CheckmarxProxy Proxy(CommonOptions options)
		{
			return new CheckmarxProxy(new ProxySettings
			{
				Url = options.CheckmarxApiUrl,
				Username = options.Username,
				Password = options.Password
			});
		}

		static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
		{
			var exception = (Exception)e.ExceptionObject;
			Console.Error.WriteLine(exception.Message);
			Console.Error.WriteLine(exception.StackTrace);
			Environment.Exit((int)ExitCode.Error);
		}
	}

	enum ExitCode
	{
		Success = 0,
		Error = -1,
		InvalidArgument = -2
	}
}

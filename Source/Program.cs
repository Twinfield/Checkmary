using System;
using System.Diagnostics;
using System.Linq;
using Checkmary.Models;

namespace Checkmary
{
	class Program
	{
		static readonly Options Options = new Options();

		static int Main(string[] args)
		{
			ConfigureExceptionHandler();

			if (!args.Any())
			{
				Console.WriteLine(Options.GetUsage());
				return (int)ExitCode.Success;
			}

			if (!CommandLine.Parser.Default.ParseArgumentsStrict(args, Options, OnVerbCommand, OnFail))
			{
				return (int)ExitCode.InvalidArgument;
			}

			return (int)ExitCode.Success;
		}

		static void OnFail()
		{
			Console.WriteLine(Options.GetUsage());
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
				case StartScanOptions startScanOptions:
					OnStartScanCommand(startScanOptions);
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
				case GetQueueOptions getQueueOptions:
					OnGetQueue(getQueueOptions);
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
			return new ScanRequest
			{
				ProjectName = options.ProjectName,
				ProjectPath = options.ProjectPath,
				Preset = options.Preset,
				ConfigurationSet = options.ConfigurationSet,
				SourceCodePath = options.SourceCodePath,
				DaysSinceLastScan = options.DaysSinceLastScan,
				DryRun = options.DryRun
			};
		}

		static void OnGetProjects(GetProjectsOptions options)
		{
			var proxy = Proxy(options);
			proxy.Initialize();
			var projects = proxy.GetProjectSummaries();
			Console.WriteLine($"Found {projects.Length} projects");
			foreach (var project in projects.OrderBy(p => p.Name))
				Console.WriteLine($"{project.Name}, last scanned on {project.LastScanDate}");
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

		static void OnGetQueue(GetQueueOptions options)
		{
			var proxy = Proxy(options);
			proxy.Initialize();
			var queuedScanRequests = proxy.GetQueuedScans();
			Console.WriteLine("Get queued scans...");

			Console.WriteLine($"Found {queuedScanRequests.Length} queued request.");
			foreach (var queuedRequest in queuedScanRequests)
				Console.WriteLine($"{queuedRequest.Id}");
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
			Console.WriteLine(exception.Message);
			Console.WriteLine(exception.StackTrace);
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

﻿using System;
using System.Linq;
using Checkmary.Models;

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

		static void OnUnknownCommand(string verb)
		{
			Console.WriteLine($"Unknow command '{verb}'.");
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
	}
}

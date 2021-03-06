using System;
using System.Collections.Generic;
using System.Linq;
using Checkmary.Checkmarx;
using Checkmary.CxSDKWebService;

namespace Checkmary.Models
{
	static class CxToModelMappingExtensions
	{
		public static ProjectSummary ToProjectSummary(this ProjectDisplayData project)
		{
			return project == null
				? null
				: new ProjectSummary
				{
					Id = project.projectID,
					Name = project.ProjectName,
					TeamName = project.Group,
					LastScanDate = project.LastScanDate.ToDateTime()
				};
		}

		public static IEnumerable<ProjectSummary> ToProjectSummaries(this ProjectDisplayData[] projects)
		{
			return projects == null
				? new ProjectSummary[0]
				: projects
					.Select(i => i.ToProjectSummary());
		}

		public static Preset ToPreset(this CxSDKWebService.Preset preset)
		{
			return preset == null
				? null
				: new Preset
				{
					Id = preset.ID,
					Name = preset.PresetName
				};
		}

		public static IEnumerable<Preset> ToPresets(this CxSDKWebService.Preset[] presets)
		{
			return presets == null
				? new Preset[0]
				: presets
					.Select(i => i.ToPreset());
		}

		public static DateTime ToDateTime(this CxDateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
				dateTime.Hour, dateTime.Minute, dateTime.Second,
				DateTimeKind.Utc);
		}

		public static ConfigurationSet ToConfigurationSet(this CxSDKWebService.ConfigurationSet configurationSet)
		{
			return configurationSet == null
				? null
				: new ConfigurationSet
				{
					Id = configurationSet.ID,
					Name = configurationSet.ConfigSetName
				};
		}

		public static IEnumerable<ConfigurationSet> ToConfigurationSets(
			this CxSDKWebService.ConfigurationSet[] configurationSets)
		{
			return configurationSets == null
				? new ConfigurationSet[0]
				: configurationSets.Select(i => i.ToConfigurationSet());
		}

		public static QueuedScanRequest ToQueuedScanRequest(this SastScanRequestDTO request)
		{
			return new QueuedScanRequest
			{
				Id = request.id,
				RunId = request.runId,
				Stage = request.stage
			};
		}
	}
}
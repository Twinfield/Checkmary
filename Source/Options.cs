using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace Checkmary
{
	class Options
	{
		[ParserState]
		public IParserState LastParserState { get; set; }

		[VerbOption("startscan", HelpText = "Collects source code and starts a Checkmarx scan.")]
		public StartScanOptions StartScan { get; set; }

		[VerbOption("getprojects", HelpText = "Gets all projects.")]
		public GetProjectsOptions GetProjects { get; set; }

		[VerbOption("getpresets", HelpText = "Gets all presets.")]
		public GetPresetsOptions GetPresets { get; set; }

		[VerbOption("getconfigsets", HelpText = "Gets all configuration sets.")]
		public GetConfigurationSetsOptions GetCongConfigurationSets { get; set; }

		[VerbOption("getqueue", HelpText = "Gets all queued scans.")]
		public GetQueueOptions GetQueue { get; set; }

		[HelpVerbOption]
		public string GetUsage(string verb)
		{
			var help = HelpText.AutoBuild(this, verb);
			AddParseErrors(help);
			return help;
		}

		void AddParseErrors(HelpText help)
		{
			if (!(LastParserState?.Errors.Any() ?? false)) return;

			var errors = help.RenderParsingErrorsText(this, 2);

			if (!string.IsNullOrEmpty(errors))
			{
				help.AddPreOptionsLine("ERROR(S):");
				help.AddPreOptionsLine(errors);
			}
		}
	}

	class CommonOptions
	{
		[Option("username", Required = true, HelpText = "Checkmarx username.")]
		public string Username { get; set; }

		[Option("password", Required = true, HelpText = "Checkmarx password.")]
		public string Password { get; set; }

		[Option("apiurl", Required = true, HelpText = "Checkmarx API URL. E.g. 'https://<your-server>'.")]
		public string CheckmarxApiUrl { get; set; }
	}

	class StartScanOptions : CommonOptions
	{
		[Option("team", Required = true)]
		public string Team { get; set; }

		[Option("project", Required = true)]
		public string Project { get; set; }

		[Option("preset", DefaultValue = "All")]
		public string Preset { get; set; }

		[Option("configset", DefaultValue = "Default all languages")]
		public string ConfigurationSet { get; set; }

		[Option("sourcecodepath", Required = true)]
		public string SourceCodePath { get; set; }

		[Option("dayssincelastscan", DefaultValue = 7, HelpText = "If the last scan was less than the specifield number of day ago, no scan will be started.")]
		public int DaysSinceLastScan { get; set; }

		[Option("dryrun", DefaultValue = false, HelpText = "If set to true, no actual scan will be started.")]
		public bool DryRun { get; set; }
	}

	class GetProjectsOptions : CommonOptions
	{ }

	class GetPresetsOptions : CommonOptions
	{ }

	class GetConfigurationSetsOptions : CommonOptions
	{ }

	class GetQueueOptions : CommonOptions
	{ }
}
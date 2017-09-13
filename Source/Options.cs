using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace Checkmary
{
	class Options
	{
		[ParserState]
		public IParserState LastParserState { get; set; }

		[VerbOption("StartScan", HelpText = "Collects source code and starts a Checkmarx scan.")]
		public StartScanOptions StartScanVerb { get; set; }

		[VerbOption("GetProjects", HelpText = "")]
		public GetProjectsOptions GetProjectsVerb { get; set; }

		[VerbOption("GetPresets", HelpText = "")]
		public GetPresetsOptions GetPresetsVerb { get; set; }

		[VerbOption("GetConfigSets", HelpText = "")]
		public GetConfigurationSetsOptions GetCongConfigurationSetsVerb { get; set; }

		[VerbOption("GetQueue", HelpText = "")]
		public GetQueueOptions GetQueueVerb { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			var help = HelpTextWithHeading();
			AddParseErrors(help);
			help.AddOptions(this);
			return help;
		}

		static HelpText HelpTextWithHeading()
		{
			var help = new HelpText
			{
				Heading = new HeadingInfo("Checkmary, a command line tool to work with Checkmarx scans"),
				AdditionalNewLineAfterOption = true,
				AddDashesToOption = true
			};
			return help;
		}

		void AddParseErrors(HelpText help)
		{
			if (LastParserState?.Errors.Any() ?? false)
			{
				var errors = help.RenderParsingErrorsText(this, 2);

				if (!string.IsNullOrEmpty(errors))
				{
					help.AddPreOptionsLine("ERROR(S):");
					help.AddPreOptionsLine(errors);
				}
			}
		}
	}

	class CommonOptions
	{
		[Option("Username", Required = true, HelpText = "Checkmarx username.")]
		public string Username { get; set; }

		[Option("Password", Required = true, HelpText = "Checkmarx password.")]
		public string Password { get; set; }

		[Option("ApiUrl", Required = true, HelpText = "Checkmarx API URL. E.g. 'https://<your-server>'.")]
		public string CheckmarxApiUrl { get; set; }
	}

	class StartScanOptions : CommonOptions
	{
		[Option(Required = true)]
		public string ProjectPath { get; set; }

		[Option(Required = true)]
		public string ProjectName { get; set; }

		[Option(DefaultValue = "All")]
		public string Preset { get; set; }

		[Option(DefaultValue = "Default all languages")]
		public string ConfigurationSet { get; set; }

		[Option(Required = true)]
		public string SourceCodePath { get; set; }

		[Option(DefaultValue = 7, HelpText = "If the last scan was less than the specifield number of day ago, no scan will be started.")]
		public int DaysSinceLastScan { get; set; }

		[Option(DefaultValue = false, HelpText = "If set to true, no actual scan will be started.")]
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
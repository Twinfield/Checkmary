using System;
using System.Linq;
using CommandLine;
using CommandLine.Text;

namespace Checkmary
{
	class Options
	{
		[Option("Username", Required = true, HelpText = "Checkmarx username.")]
		public string Username { get; set; }

		[Option("Password", Required = true, HelpText = "Checkmarx password.")]
		public string Password { get; set; }

		[Option("ApiUrl", Required = true, HelpText = "Checkmarx API URL. E.g. 'https://<your-server>/Cxwebinterface/'.")]
		public string CheckmarxApiUrl { get; set; }

		[Option("ProjectPath", Required = true)]
		public string ProjectPath { get; set; }

		[Option("ProjectName", Required = true)]
		public string ProjectName { get; set; }

		[Option("Preset", DefaultValue = "All")]
		public string Preset { get; set; }

		[Option("ConfigSet", DefaultValue = "Default all languages")]
		public string ConfigurationSet { get; set; }

		[Option("SourceCodePath", Required = true)]
		public string SourceCodePath { get; set; }

		[Option("DryRun", DefaultValue = false, HelpText = "If set to true, no actual scan will be started.")]
		public bool DryRun { get; set; }

		[ParserState]
		public IParserState LastParserState { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			var help = new HelpText
			{
				Heading = new HeadingInfo("Checkmary, a command line tool to work with Checkmarx scans"),
				AdditionalNewLineAfterOption = true,
				AddDashesToOption = true
			};

			if (LastParserState?.Errors.Any() ?? false)
			{
				var errors = help.RenderParsingErrorsText(this, 2);

				if (!string.IsNullOrEmpty(errors))
				{
					help.AddPreOptionsLine("");
					help.AddPreOptionsLine("ERROR(S):");
					help.AddPreOptionsLine(errors);
				}
			}

			help.AddOptions(this);
			return help;
		}
	}
}
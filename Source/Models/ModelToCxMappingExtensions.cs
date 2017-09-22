using Checkmary.CxSDKWebService;

namespace Checkmary.Models
{
	static class ModelToCxMappingExtensions
	{
		public static CliScanArgs ToCliScanArgs(this ScanSettings scanSettings)
		{
			return new CliScanArgs
			{
				PrjSettings = new ProjectSettings
				{
					projectID = scanSettings.ProjectId,
					ProjectName = $@"{scanSettings.TeamName}\{scanSettings.ProjectName}",
					PresetID = scanSettings.PresetId,
					ScanConfigurationID = scanSettings.ConfigurationSetId
				},
				SrcCodeSettings = new SourceCodeSettings
				{
					SourceOrigin = SourceLocationType.Local,
					PackagedCode = new LocalCodeContainer
					{
						FileName = scanSettings.ZipFileName,
						ZippedFile = scanSettings.ZipFileContents
					}
				},
				IsPrivateScan = false,
				IsIncremental = false
			};
		}
	}
}
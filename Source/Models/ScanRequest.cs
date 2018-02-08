namespace Checkmary.Models
{
	class ScanRequest
	{
		public string ProjectName { get; set; }
		public string TeamName { get; set; }
		public string SourceCodePath { get; set; }
		public bool DryRun { get; set; }
	}

	class SastScanRequest : ScanRequest
	{
		public string Preset { get; set; }
		public string ConfigurationSet { get; set; }
		public int DaysSinceLastScan { get; set; }
	}

	class OsaScanRequest : ScanRequest
	{
		public string ScanIdsFilePath { get; set; }
	 }
}
namespace Checkmary.Models
{
	class ScanSettings
	{
		public long ProjectId { get; set; }
		public string ProjectName { get; set; }
		public string TeamName { get; set; }
		public string ZipFileName { get; set; }
		public byte[] ZipFileContents { get; set; }
	}

	class SastScanSettings : ScanSettings
	{
		public long PresetId { get; set; }
		public long ConfigurationSetId { get; set; }
	 }

	class OsaScanSettings : ScanSettings
	{ }
}
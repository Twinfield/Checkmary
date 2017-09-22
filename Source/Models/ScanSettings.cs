namespace Checkmary.Models
{
	class ScanSettings
	{
		public long ProjectId { get; set; }
		public string TeamName { get; set; }
		public string ProjectName { get; set; }
		public long PresetId { get; set; }
		public long ConfigurationSetId { get; set; }
		public string ZipFileName { get; set; }
		public byte[] ZipFileContents { get; set; }
	}
}
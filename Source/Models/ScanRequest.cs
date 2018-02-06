namespace Checkmary.Models
{
	class ScanRequest
	{
		public string ProjectName { get; set; }
		public string TeamName { get; set; }
		public string Preset { get; set; }
		public string ConfigurationSet { get; set; }
		public string SourceCodePath { get; set; }
		public int DaysSinceLastScan { get; set; }
		public bool DryRun { get; set; }
	    public SourceType SourceType { get; set; }
    }

    enum SourceType
    {
        SAST = 1,
        OSA = 2
    }
}
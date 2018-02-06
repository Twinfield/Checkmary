namespace Checkmary.Checkmarx
{
    public class DownloadOsaScanReportDto
    {
        public string ScanId { get; set; }
        public string ReportFormat { get; set; }
        public string ProjectName { get; set; }
    }
}
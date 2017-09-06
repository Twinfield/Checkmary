using System;

namespace CheckmarxTool.Models
{
	class ProjectSummary
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public DateTime LastScanDate { get; set; }
	}

	class ConfigurationSet
	{
		public long Id { get; set; }
		public string Name { get; set; }
	}
}
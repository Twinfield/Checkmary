using System;

namespace Checkmary.Models
{
	class ProjectSummary
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string TeamName { get; set; }
		public DateTime LastScanDate { get; set; }
	}
}
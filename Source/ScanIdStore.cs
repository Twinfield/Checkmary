using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Checkmary
{
	public static class ScanIdStore
	{
		public static void WriteToFile(string filePath, string projectName, string scanId)
		{
			File.AppendAllLines(filePath, new[] { $"{projectName}\t{scanId}" });
		}

		public static List<ProjectScanDetails> ParseScanIds(string filePath)
		{
			var scanIds = File.ReadAllLines(filePath);

			return scanIds.Select(scanId => scanId.Split('\t'))
				.Select(scanDetails => new ProjectScanDetails
				{
					ScanId = scanDetails[1],
					ProjectName = scanDetails[0]
				})
				.ToList();
		}
	}

	public class ProjectScanDetails
	{
		public string ProjectName { get; set; }
		public string ScanId { get; set; }
	}
}
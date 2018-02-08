using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Checkmary.Persistence
{
	public class ScanIdStore
	{
		private readonly string _filePath;

		public ScanIdStore(string filePath)
		{
			_filePath = filePath;
		}

		public void Save(string projectName, string scanId)
		{
			File.AppendAllLines(_filePath, new[] { $"{projectName}\t{scanId}" });
		}

		public List<ProjectScanDetails> GetScanIds()
		{
			var scanIds = File.ReadAllLines(_filePath);

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
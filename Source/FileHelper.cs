using System.IO;

namespace Checkmary
{
	public static class FileHelper
	{
		public static void WriteToFile(string filePath, string projectName, string scanId)
		{
			File.AppendAllLines(filePath, new[] { $"{projectName}\t{scanId}" });
		}

		public static string[] ReadFile(string filePath)
		{
			return File.ReadAllLines(filePath);
		}
	}
}

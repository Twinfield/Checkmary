using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace CheckmarxTool
{
	public static class ZipHelper
	{
		public static byte[] ZipDirectoryToByteArray(string sourcePath, Func<string, bool> filter)
		{
			var sourcePathLength = GetSourcePathLength(sourcePath);

			using (var zipFileStream = new MemoryStream())
			{
				using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
				{
					foreach (var fileToAdd in FindFiles(sourcePath, filter))
						archive.CreateEntryFromFile(fileToAdd, fileToAdd.Substring(sourcePathLength));
				}
				return zipFileStream.ToArray();
			}
		}

		static IEnumerable<string> FindFiles(string path, Func<string, bool> filter)
		{
			return Directory
				.EnumerateFiles(path, "*", SearchOption.AllDirectories)
				.Where(filter);
		}

		static int GetSourcePathLength(string sourceFolder)
		{
			return sourceFolder.Length + (PathEndsWithDirectorySeparator(sourceFolder) ? 1 : 0);
		}

		static bool PathEndsWithDirectorySeparator(string sourceFolder)
		{
			var lastChar = sourceFolder[sourceFolder.Length - 1];
			return lastChar != Path.DirectorySeparatorChar
					 && lastChar != Path.AltDirectorySeparatorChar;
		}
	}
}
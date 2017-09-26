using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Checkmary
{
	public static class ZipHelper
	{
		public static byte[] ZipDirectoryToByteArray(string path, Func<string, bool> filter)
		{
			return WithinDirectory(path, () =>
			{
				using (var zipFileStream = new MemoryStream())
				{
					ZipCurrentDirectoryToStream(filter, zipFileStream);

					return zipFileStream.ToArray();
				}
			});
		}

		static void ZipCurrentDirectoryToStream(Func<string, bool> filter, Stream zipFileStream)
		{
			using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
			{
				foreach (var file in FindFilesInCurrentDirectory(filter))
					archive.CreateEntryFromFile(file, StripCurrentDirectory(file));
			}
		}

		static byte[] WithinDirectory(string path, Func<byte[]> func)
		{
			var oldDirectory = Directory.GetCurrentDirectory();

			try
			{
				Directory.SetCurrentDirectory(path);

				return func();
			}
			finally
			{
				Directory.SetCurrentDirectory(oldDirectory);
			}
		}

		static string StripCurrentDirectory(string path)
		{
			// Strip leading .\ from relative path
			return path.Substring(2);
		}

		static IEnumerable<string> FindFilesInCurrentDirectory(Func<string, bool> filter)
		{
			return Directory
				.EnumerateFiles(".", "*", SearchOption.AllDirectories)
				.Where(filter);
		}
	}
}
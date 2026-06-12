using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TERM_FLIX.Services
{
    internal class FolderRepository
    {
        private readonly string[] _blacklist =
        {
            "menu", "ost", "ncop", "nced", "extrák", "extras",
            "sample", "extra", "font", "00. extrák", "00. extras",
            "00. extra", "00. menu", "00 ost", "dvd", 
        };

        public IEnumerable<string> Search(string rootPath)
        {
            var mkvFiles = new List<string>();
            Crawl(rootPath, mkvFiles);
            return mkvFiles;
        }

        private void Crawl(string currentPath, List<string> mkvFiles)
        {
            // Fast path to bypass OS system folders and the Recycle Bin before checking files
            string currentPathUpper = currentPath.ToUpperInvariant();
            if (currentPathUpper.Contains("$RECYCLE.BIN") ||
                currentPathUpper.Contains("SYSTEM VOLUME INFORMATION"))
            {
                return;
            }

            // 1. Process and filter all .mkv files within the current directory
            try
            {
                var mkvs = Directory.GetFiles(currentPath, "*.mkv");

                var filteredMkvs = mkvs.Where(filePath =>
                {
                    string fileName = Path.GetFileName(filePath).ToLowerInvariant();

                    var directoryPath = Path.GetDirectoryName(filePath);
                    string currentFolderName = !string.IsNullOrEmpty(directoryPath)
                        ? Path.GetFileName(directoryPath).ToLowerInvariant()
                        : string.Empty;

                    // Exclude files matching blacklist keywords in either filename or direct parent folder
                    return !_blacklist.Any(forbidden =>
                        fileName.Contains(forbidden) || currentFolderName.Contains(forbidden)
                    );
                });

                mkvFiles.AddRange(filteredMkvs);
            }
            catch (UnauthorizedAccessException)
            {
                // Access denied to files in this directory, skip silently
            }

            // 2. Recursively crawl through subdirectories
            try
            {
                var subFolders = Directory.GetDirectories(currentPath);

                foreach (var subDir in subFolders)
                {
                    try
                    {
                        Crawl(subDir, mkvFiles);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Skip locked or protected subfolders without breaking the main loop
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Access denied to the subdirectory list itself, abort this branch safely
            }
        }
    }
}
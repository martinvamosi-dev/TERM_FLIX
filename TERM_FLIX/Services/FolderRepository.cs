using System.IO;

namespace TERM_FLIX.Services
{
    internal class FolderRepository
    {
        private readonly string[] _blacklist = { "menu", "ost", "ncop", "nced", "extrák", "extras", "sample", "extra", "font", "00. extrák", "00. extras", "00. extra", "00. menu", "00 ost" };

        public IEnumerable<string> Search(string roothPath)
        {
            List<string> mkvFiles = new List<string>();
            Crawl(roothPath, mkvFiles);
            return mkvFiles;
        }

        private void Crawl(string currentPath, List<string> mkvFiles)
        {
            try
            {
                var mkvs = Directory.GetFiles(currentPath,"*.mkv");
                mkvFiles.AddRange(mkvs.Where(d => !_blacklist.Any(f => d.ToLower().Contains(f))));
            }
            catch (UnauthorizedAccessException)
            { }
            try
            {
                var subFolders = Directory.GetDirectories(currentPath);

                foreach (var subDir in subFolders)
                {
                    Crawl(subDir, mkvFiles);
                }
            }
            catch (UnauthorizedAccessException)
            { }
        }
    }
}

using System.Text.RegularExpressions;

namespace TERM_FLIX.Services.Parsers
{
    public class MediaTitleCleaner
    {
        public string Clean(string uncleanTitle)
        {
            var firstPattern = @"\[.*?\]|\(.*?\)";
            var secondPattern = @"^\d+[\s\.\-]*";
            var cleanTitle = Regex.Replace(uncleanTitle, firstPattern, "");
            cleanTitle = Regex.Replace(cleanTitle, secondPattern, "");
            cleanTitle = cleanTitle.Replace(".", " "); cleanTitle = cleanTitle.Replace(',', ' ');
            cleanTitle = cleanTitle.Trim();

            return cleanTitle;
        }
    }
}

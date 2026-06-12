using TERM_FLIX.Models;
using System.Text.RegularExpressions;
using TERM_FLIX.Models.Enums;
using System.IO;

namespace TERM_FLIX.Services.Parsers
{
    /// <summary>
    /// Parses multi-media folder structures to extract media metadata such as title, season, and episode.
    /// </summary>
    internal class MultiMediaParser : ICanParse
    {
        public bool TryParse(string filePath, out ExtractedMediaInfo extractedMediaInfo)
        {
            var cleanTitle = string.Empty;
            var seasonNumber = 1;
            var episodeNumber = 1;
            var collectionTitle = string.Empty;

            // Initialize title cleaner and extract directory information
            MediaTitleCleaner mediaTitleCleaner = new MediaTitleCleaner();
            var rawFolderPath = Path.GetDirectoryName(filePath);
            var rawFolderName = Path.GetFileName(rawFolderPath);

            // Define regex patterns for metadata extraction
            var seasonPattern = @"[Ss](\d+)|Season\s*(\d+)";
            var onlySeasonFolderPattern = @"^(?:\d+\.\s*)?(?:[Ss](\d+)|Season\s*(\d+))(?:\s*[:\-–—]\s*.*|\s+.*)?$";
            var specialFolderPattern = @"\b(OVA|Specials|Special|SP|Movies|Movie|OAD|Extras|Extra|Spec|OV|ONA)\b";

            Match onlySeasonMatch = Regex.Match(rawFolderName, onlySeasonFolderPattern);

            // STEP 1: Determine Title and evaluate special directory flags
            if (Regex.IsMatch(rawFolderName, specialFolderPattern, RegexOptions.IgnoreCase))
            {
                // Assign to Season 0 for special content (OVAs, Specials, Movies)
                seasonNumber = 0;

                // Traverse up one level to capture the parent series title
                var newFolderPath = Path.GetDirectoryName(rawFolderPath);
                var newFileName = Path.GetFileName(newFolderPath);
                cleanTitle = mediaTitleCleaner.Clean(newFileName);
            }
            else if (onlySeasonMatch.Success)
            {
                // If current folder is strictly a season identifier, traverse up to get the series title
                var newFolderPath = Path.GetDirectoryName(rawFolderPath);
                var newFileName = Path.GetFileName(newFolderPath);
                cleanTitle = mediaTitleCleaner.Clean(newFileName);
            }
            else
            {
                // Current folder represents the standalone series title
                cleanTitle = mediaTitleCleaner.Clean(rawFolderName);
            }

            // STEP 2: Extract Episode Number from filename via prioritized pipeline
            var fileName = Path.GetFileName(filePath);

            var episodePattern = @"(?:-\s*|[Ee][Pp]?|\[)(\d+)";
            var episodePatternV2 = @"\s+(\d{1,3})(?:\s+|\.mkv)";
            var episodeFallbackPattern = @"\s+(\d{2,3})(?:\s+|\.mkv)";

            Match episodeFinderMatch = Regex.Match(fileName, episodePattern);

            if (episodeFinderMatch.Success)
            {
                episodeNumber = int.Parse(episodeFinderMatch.Groups[1].Value);
            }
            else
            {
                Match episodeV2FinderMatch = Regex.Match(fileName, episodePatternV2);
                if (episodeV2FinderMatch.Success)
                {
                    episodeNumber = int.Parse(episodeV2FinderMatch.Groups[1].Value);
                }
                else
                {
                    Match episodeFallbackFinderMatch = Regex.Match(fileName, episodeFallbackPattern);
                    if (episodeFallbackFinderMatch.Success)
                    {
                        episodeNumber = int.Parse(episodeFallbackFinderMatch.Groups[1].Value);
                    }
                    else
                    {
                        // Abort parsing if no valid episode number is found
                        extractedMediaInfo = null;
                        return false;
                    }
                }
            }

            // STEP 3: Refine Season Number from folder context (Skip if explicitly marked as Special/OVA)
            if (seasonNumber != 0)
            {
                var romanNumberPath = @"\b(I|II|III|IV|V|VI|VII|VIII)\b";
                Match seasonFinderMatch = Regex.Match(rawFolderName, seasonPattern);

                if (seasonFinderMatch.Success)
                {
                    seasonNumber = int.Parse(seasonFinderMatch.Groups[1].Success ? seasonFinderMatch.Groups[1].Value : seasonFinderMatch.Groups[2].Value);
                }
                else
                {
                    Match romanNumberFinderMatch = Regex.Match(rawFolderName, romanNumberPath);
                    if (romanNumberFinderMatch.Success)
                    {
                        var seasonStr = romanNumberFinderMatch.Groups[1].Value;

                        // Map Roman numerals to standard numerical identifiers
                        seasonNumber = seasonStr.ToUpper() switch
                        {
                            "I" => 1,
                            "II" => 2,
                            "III" => 3,
                            "IV" => 4,
                            "V" => 5,
                            "VI" => 6,
                            "VII" => 7,
                            "VIII" => 8,
                            _ => int.TryParse(seasonStr, out int parsed) ? parsed : 1
                        };
                    }
                }
            }

            //STEP 4: Finding collection infos
            var collectionPath = Path.GetDirectoryName(rawFolderPath);
            if (collectionPath != null)
            {
                var collectionDirectorys = Directory.GetDirectories(collectionPath);
                foreach (var collectionDirectory in collectionDirectorys)
                {
                    var collectionFileName = Path.GetFileName(collectionDirectory);

                    Match collectionMatch = Regex.Match(collectionFileName, onlySeasonFolderPattern);
                    if (collectionMatch.Success)
                    {
                        collectionTitle = mediaTitleCleaner.Clean(Path.GetFileName(collectionPath));
                        break;
                    }
                }
            }

            // STEP 5: Instantiate and populate metadata payload
            extractedMediaInfo = new ExtractedMediaInfo(
                title: cleanTitle,
                location: filePath,
                mediaType: MediaType.Uncategorized,
                extractedMediaType: ExtractedMediaType.MultipleMediaItem,
                seasonNumber: seasonNumber,
                episodeNumber: episodeNumber,
                yearOfMaking: string.Empty,
                collectionTitle: collectionTitle
            );

            return true;
        }
    }
}
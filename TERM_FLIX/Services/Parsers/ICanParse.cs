using TERM_FLIX.Models;

namespace TERM_FLIX.Services.Parsers
{
    internal interface ICanParse
    {
        bool TryParse(string filePath, out ExtractedMediaInfo extractedMediaInfo);
    }
}

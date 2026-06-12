using Spectre.Console;
using TERM_FLIX.Services.Parsers;
using TERM_FLIX.Models;
using TERM_FLIX.Models.Enums;

namespace TERM_FLIX.Services
{
    internal class MasterScanner
    {
        public void ExecuteScan(string roothPath, StatusContext ctx)
        {
            FolderRepository folderRepository = new FolderRepository();
            var folderParser = new MultiMediaParser();
            List<ICanParse> parsers = new List<ICanParse>() { folderParser};

            var directories = folderRepository.Search(roothPath);

            foreach (var directory in directories)
            {
                bool isParsed = false;

                foreach (var parser in parsers)
                {
                    ExtractedMediaInfo info;
                    if (parser.TryParse(directory, out info))
                    {
                        isParsed = true;

                        var titleCleaner = new MediaTitleCleaner();
                        string cleanTitle = titleCleaner.Clean(info.Title);

                        // SAFE ESCAPING FOR EVERYTHING FROM THE FILE SYSTEM
                        string safeTitle = Markup.Escape(cleanTitle);
                        string safeLocation = Markup.Escape(info.Location); 
                        string safeCollection = !string.IsNullOrEmpty(info.CollectionTitle)
                            ? Markup.Escape(titleCleaner.Clean(info.CollectionTitle))
                            : "None";

                        string logLine = $"SUCCESS | Title: {cleanTitle} | Collection: {safeCollection} | Season: {info.SeasonNumber} | Episode: {info.EpisodeNumber} | Location: {info.Location}{Environment.NewLine}";
                        File.AppendAllText("scan_history_log.txt", logLine);
                        // Clean visualization pipeline
                        AnsiConsole.MarkupLine($"[GREEN]SUCCESS[/] [grey]Title:[/] [white]{safeTitle}[/]");
                        AnsiConsole.MarkupLine($"[white]Location:[/] [yellow]{safeLocation}[/]"); 
                        AnsiConsole.MarkupLine($"[red]Collection:[/] [cyan]{safeCollection}[/]");
                        AnsiConsole.MarkupLine($"[grey]Details:[/] [cyan]Season {info.SeasonNumber}[/], [purple]Episode {info.EpisodeNumber}[/]");
                        AnsiConsole.MarkupLine("[grey]----------------------------------------[/]");
                        break;
                    }
                }

                if (!isParsed)
                {
                    var fileName = Path.GetFileName(directory);
                    // START
                    string UIsafeDirectoryPath = Markup.Escape(directory);
                    string UIsafefileName = Markup.Escape(fileName);
                    AnsiConsole.MarkupLine($"[grey][[[yellow]FOLDER[/]]][/] [red]CANT PROCESS[/]Directory: [white]{UIsafefileName}[/]");
                    //Thread.Sleep(200);
                    // END
                }
            }
        }
    }
}

using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace TERM_FLIX.Services
{
    internal class MasterScanner
    {
        public void ExecuteScan(string roothPath, StatusContext ctx)
        {
            FolderRepository folderRepository = new FolderRepository();
            var directories = folderRepository.Search(roothPath);
            foreach (var directory in directories)
            {
                // START
                // This is for the ui.
                string UIsafeDirectoryPath = Markup.Escape(directory);
                AnsiConsole.MarkupLine($"[grey][[[green]FOLDER[/]]][/] Processing directory: [white]{UIsafeDirectoryPath}[/]");
                //Thread.Sleep(200);
                // END
            }
        }
    }
}

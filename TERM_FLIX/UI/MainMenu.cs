using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace TERM_FLIX.UI
{
    public class MainMenu
    {
        public void Show()
        {
            bool running = true;

            while (running)
            {
                AnsiConsole.Clear();

                AnsiConsole.Write(new FigletText("TERM_FLIX").Color(Color.Red));
                AnsiConsole.MarkupLine("[grey]v0.0.1 - Open Source TUI[/]\n");

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[bold white]What to do next?![/]")
                    .AddChoices(new[] { "Series", "Movies", "Animes","Dashboard & Tools", "Exit" })
                    );

                if (choice == "Animes")
                {
                    var anime = new AnimeBrowser();
                    anime.Show();
                }
                if (choice == "Dashboard & Tools")
                {
                    var dashTools = new Dashboard___Tools();
                    dashTools.show();
                }
                if (choice == "Exit")
                {
                    running = false;
                }
            }
        }
    }
}

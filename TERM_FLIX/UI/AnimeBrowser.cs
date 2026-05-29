using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace TERM_FLIX.UI
{
    public class AnimeBrowser
    {
        public void Show()
        {
            bool browsing = true;

            while (browsing)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[bold green]=== ANIME BROWSER ===[/]");

                var animeChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Chose an anime:")
                    .AddChoices(new[] { "One piece", "Hunter x Hunter", "Naruto", "Return" })
                    );

                if (animeChoice != "Return")
                {
                    AnsiConsole.Write(new Panel("Fake description until we create a proper one with backend...").BorderColor(Color.Green));
                    Console.ReadKey();
                }
                else
                {
                    browsing = false;
                }
            }
        }
    }
}

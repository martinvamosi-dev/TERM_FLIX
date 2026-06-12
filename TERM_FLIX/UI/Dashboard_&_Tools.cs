using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace TERM_FLIX.UI
{
    internal class Dashboard___Tools
    {
        public void show()
        {
            bool inDashboard = true;
            while (inDashboard)
            {
                AnsiConsole.Clear();

                AnsiConsole.Write(new Rule("[bold yellow]=== DASHBOARD & TOOLS===[/]").Centered());
                AnsiConsole.WriteLine();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[grey]Select an administrative acton:[/]")
                    .AddChoices(new[] { "Search & Scan for new Content", "Return to Main Menu" })
                    );

                switch (choice)
                {
                    case "Search & Scan for new Content":
                        AnsiConsole.Clear();
                        AnsiConsole.Write(new Rule("[bold yellow]=== MEDIA SCANNER ===[/]").Centered());
                        AnsiConsole.WriteLine();

                        // 1. Bekérjük az elérést a usertől
                        string path = AnsiConsole.Ask<string>("[white]Give a location:[/]");
                        AnsiConsole.WriteLine();

                        // 2. Példányosítjuk a scannert
                        var scanner = new Services.MasterScanner();

                        // 3. Elindítjuk a pörgést, és átadjuk neki a munkát
                        AnsiConsole.Status()
                            .Spinner(Spinner.Known.Dots)
                            .SpinnerStyle(Style.Parse("yellow"))
                            .Start("Scanner inicialization...", ctx =>
                            {
                                // Átadjuk a path-t és a Spectre saját 'ctx' változóját
                                scanner.ExecuteScan(path, ctx);
                            });

                        // 4. Miután végzett, megáll a spinner, és várunk egy gombnyomásra
                        AnsiConsole.WriteLine();
                        AnsiConsole.MarkupLine("[bold green]✔ Scanning finished succesfully![/]");
                        AnsiConsole.MarkupLine("[grey]Press a key on your keyboard to return...[/]");
                        Console.ReadKey();
                        break;

                    case "Return to Main Menu":
                        inDashboard = false;
                        break;
                }
            }
        }
    }
}

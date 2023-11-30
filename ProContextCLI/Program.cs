using ConsoleTables;
using ProContext.Services;

namespace ProContextCLI;

class Program
{
    static async Task Main()
    {
        var cbrScraper = new CbrScraper(new HttpClientHandler());
        var statService = new CurrencyStatService(cbrScraper);
        var stats = await statService.GetStats();

        Console.WriteLine("Currency rates for the past 90 days");

        var table = new ConsoleTable("Currency", "Min rate", "Min rate date", "Max rate", "Max rate date", "Average rate");
        foreach (var s in stats)
        {
            table.AddRow(s.Code, s.MinValue, s.MinDate.ToString("dd.MM.yyyy"), s.MaxValue, s.MaxDate.ToString("dd.MM.yyyy"), s.AvgValue);
        }
        
        table.Write();
        Console.WriteLine();
    }
}


using ProContext.Dtos;
using ProContext.Entities;

namespace ProContext.Services;

public class CurrencyStatService
{
	private readonly ICbrCurrencyProvider provider;

	public CurrencyStatService(ICbrCurrencyProvider provider)
	{
		this.provider = provider;
	}

	public async Task<List<CurrencyStat>> GetStats()
	{
		var date = DateTime.Now;
		var stats = new Dictionary<string, CurrencyStat>();

		for (int i = 0; i < 90; i++)
		{
			var cbrRates = await TryGetRatesByDate(date);

			foreach (var rate in cbrRates)
			{
				stats.TryGetValue(rate.CharCode, out var stat);
				stat ??= new CurrencyStat(rate.CharCode);

				stat.UpdateFromCbr(rate, date);
				stats[stat.Code] = stat;
			}

			date = date.AddDays(-1);
		}

		return stats.Values.ToList();
	}

	private async Task<List<CbrCurrency>> TryGetRatesByDate(DateTime date)
	{
		try
		{
            return await provider.GetRatesByDate(date);
        }
		catch (HttpRequestException ex)
		{
			Console.WriteLine(ex);
			return new List<CbrCurrency>();
		}
	}
}

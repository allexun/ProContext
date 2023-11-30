using ProContext.Dtos;

namespace ProContext.Services;

public interface ICbrCurrencyProvider
{
    Task<List<CbrCurrency>> GetRatesByDate(DateTime date);
}


using ProContext.Dtos;
using System.Text;
using System.Xml.Serialization;

namespace ProContext.Services;

public class CbrScraper : IDisposable, ICbrCurrencyProvider
{
    private readonly HttpClient client;

	public CbrScraper(HttpMessageHandler handler)
	{
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        client = new HttpClient(handler);
    }

    public async Task<List<CbrCurrency>> GetRatesByDate(DateTime date)
    {
        var xml = await GetRatesXml(date);

        var serializer = new XmlSerializer(typeof(CbrResponse));

        using var reader = new StringReader(xml);
        var response = (CbrResponse)serializer.Deserialize(reader)!;

        return response.Currencies;
    }

    private async Task<string> GetRatesXml(DateTime date)
    {
        var dateStr = date.ToString("dd/MM/yyyy");
        var url = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={dateStr}";

        return await client.GetStringAsync(url);
    }

    public void Dispose()
    {
        client.Dispose();
    }
}


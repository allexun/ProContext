using Moq;
using Moq.Protected;
using System.Net;
using ProContext.Services;

namespace ProContextTests;

public class CbrScraperTests
{
	private CbrScraper scraper;

    private const string API_TEST_CONTENT = """
        <ValCurs Date="21.11.2020" name="Foreign Currency Market">
            <Valute ID="R01010">
                <NumCode>036</NumCode>
                <CharCode>AUD</CharCode>
                <Nominal>1</Nominal>
                <Name>Australian Dollar</Name>
                <Value>55,4127</Value>
                <VunitRate>55,4127</VunitRate>
            </Valute>
        </ValCurs>
        """;

    [SetUp]
	public void SetUp()
	{
        var messageHandler = new Mock<HttpMessageHandler>();
        messageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                                          ItExpr.IsAny<HttpRequestMessage>(),
                                          ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(API_TEST_CONTENT)
            });

        scraper = new CbrScraper(messageHandler.Object);
	}

    [TearDown]
    public void TearDown()
    {
        scraper.Dispose();
    }

	[Test]
	public async Task TestGetRates()
	{
        var rates = await scraper.GetRatesByDate(DateTime.Now);

        Assert.That(rates, Has.Count.EqualTo(1));
	}
}


using ProContext.Dtos;
using ProContext.Entities;

namespace ProContextTests;

public class CurrencyStatTests
{
    [Test]
    public void TestUpdateFromCbr()
    {
        var cbr = new CbrCurrency
        {
            CharCode = "EUR",
            Value = "50,50",
        };

        var stat = new CurrencyStat(cbr.CharCode);
        stat.UpdateFromCbr(cbr, DateTime.Now);

        Assert.That(stat.MinValue, Is.EqualTo(50.50));
        Assert.That(stat.MaxValue, Is.EqualTo(50.50));
        Assert.That(stat.AvgValue, Is.EqualTo(50.50));
    }
}


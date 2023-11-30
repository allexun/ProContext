using System.Globalization;
using ProContext.Dtos;

namespace ProContext.Entities;

public class CurrencyStat
{
    public readonly string Code;
    public float MinValue;
    public DateTime MinDate;
    public float MaxValue;
    public DateTime MaxDate;
    public float AvgValue;
    public float Sum;
    public int Count;

    public CurrencyStat(string code)
    {
        Code = code;
    }

    public void UpdateFromCbr(CbrCurrency cbr, DateTime date)
    {
        float.TryParse(cbr.Value, NumberStyles.Any, CultureInfo.GetCultureInfo("ru"), out var value);

        if (MinValue == 0 || MinValue > value)
        {
            MinValue = value;
            MinDate = date;
        }
        if (MaxValue == 0 || MaxValue < value)
        {
            MaxValue = value;
            MaxDate = date;
        }
        Sum += value;
        Count++;
        AvgValue = Sum / Count;
    }
}


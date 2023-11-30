using System.Globalization;
using System.Xml.Serialization;

namespace ProContext.Dtos;

[Serializable]
public class CbrCurrency
{
    [XmlAttribute("ID")]
    public string ID { get; set; }

    [XmlElement("NumCode")]
    public string NumCode { get; set; }

    [XmlElement("CharCode")]
    public string CharCode { get; set; }

    [XmlElement("Nominal")]
    public int Nominal { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlElement("Value")]
    public string Value { get; set; }

    [XmlElement("VunitRate")]
    public string VunitRate { get; set; }
}

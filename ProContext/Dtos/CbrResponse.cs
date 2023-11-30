using System.Xml.Serialization;

namespace ProContext.Dtos;

[Serializable]
[XmlRoot("ValCurs")]
public class CbrResponse
{
    [XmlAttribute("Date")]
    public string Date { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlElement("Valute")]
    public List<CbrCurrency> Currencies { get; set; }
}


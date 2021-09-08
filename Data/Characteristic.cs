using System.Xml.Serialization;

namespace OnePageRulesCards.Data
{
  [XmlType("characteristic")]
  public class Characteristic
  {
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlText]
    public string Value { get; set; }
  }
}

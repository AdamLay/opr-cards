using System.Xml.Serialization;

namespace OnePageRulesCards.Data
{
  [XmlType("rule")]
  public class Rule
  {
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }
  }
}

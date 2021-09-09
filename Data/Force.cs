using System.Xml.Serialization;

namespace OnePageRulesCards.Data
{
  [XmlType("force")]
  public class Force
  {
    [XmlAttribute("catalogueName")]
    public string Name { get; set; }

    [XmlArray("selections")]
    public Selection[] Selections { get; set; }
  }
}

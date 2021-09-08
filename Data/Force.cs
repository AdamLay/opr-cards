using System.Xml.Serialization;

namespace OnePageRulesCards.Data
{
  [XmlType("force")]
  public class Force
  {
    [XmlArray("selections")]
    public Selection[] Selections { get; set; }
  }
}

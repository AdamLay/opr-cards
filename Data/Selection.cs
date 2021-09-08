using System.Xml.Serialization;

namespace OnePageRulesCards.Data
{
  [XmlType("selection")]
  public class Selection
  {
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlArray("rules")]
    public Rule[] Rules { get; set; }

    [XmlArray("profiles")]
    public Profile[] Profiles { get; set; }

    [XmlArray("selections")]
    public Selection[] Selections { get; set; }
  }
}

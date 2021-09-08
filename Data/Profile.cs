using System.Xml.Serialization;

namespace OnePageRulesCards.Data
{
  [XmlType("profile")]
  public class Profile
  {
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("typeName")]
    public string TypeName { get; set; }

    [XmlArray("characteristics")]
    public Characteristic[] Characteristics { get; set; }
  }
}

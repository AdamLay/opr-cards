using System.Xml.Serialization;

namespace OnePageRulesCards.Data
{
  [XmlRoot("roster", Namespace = @"http://www.battlescribe.net/schema/rosterSchema")]
  public class Roster
  {
    [XmlArray("forces")]
    public Force[] Forces { get; set; }
  }
}

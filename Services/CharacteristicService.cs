namespace OnePageRulesCards.Services
{
  public class CharacteristicService
  {
    public string MapName(string name)
    {
      return name.ToLower() switch
      {
        "range" => "RNG",
        "attacks" => "ATK",
        "special rules" => "Rules",
        "psychic spell" => "Effect",
        "melee" => "-",
        _ => name
      };
    }
  }
}

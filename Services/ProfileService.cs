namespace OnePageRulesCards.Services
{
  public class ProfileService
  {
    public string MapName(string name)
    {
      return name.ToLower() switch
      {
        "ranged weapon" => "Ranged",
        "psychic spell" => "Psychic",
        _ => name
      };
    }
  }
}

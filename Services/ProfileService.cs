using System.Linq;
using System.Text.RegularExpressions;
using OnePageRulesCards.Data;

namespace OnePageRulesCards.Services
{
  public class ProfileService
  {
    public int? GetToughness(Profile profile)
    {
      string? specialRules = profile
        .Characteristics
        ?.FirstOrDefault(c => c.Name.ToLower() == "special rules")
        ?.Value;

      // No special rules means no Tough(X)
      if (specialRules == null)
        return null;

      var toughPattern = new Regex(@"Tough\((\d+)\)", RegexOptions.IgnoreCase);

      // Does not have the Tough(X) rule
      if (!toughPattern.IsMatch(specialRules))
        return null;

      return int.Parse(toughPattern.Match(specialRules).Groups[1].Value);
    }

    public int? GetPsychic(Profile profile)
    {
      string? specialRules = profile
        .Characteristics
        ?.FirstOrDefault(c => c.Name.ToLower() == "special rules")
        ?.Value;

      // No special rules means no Tough(X)
      if (specialRules == null)
        return null;

      var psychicPattern = new Regex(@"Psychic\((\d+)\)", RegexOptions.IgnoreCase);

      // Does not have the Tough(X) rule
      if (!psychicPattern.IsMatch(specialRules))
        return null;

      return int.Parse(psychicPattern.Match(specialRules).Groups[1].Value);
    }

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

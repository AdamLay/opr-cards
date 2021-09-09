using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OnePageRulesCards.Data;

namespace OnePageRulesCards.Services
{
  public class ProfileService
  {
    public IEnumerable<Profile> GetProfiles(Selection selection, string type = null)
    {
      var profiles = new List<Profile>();

      if (selection.Profiles?.Any(p => type == null || p.TypeName == type) == true)
        profiles.AddRange(selection.Profiles);

      if (selection.Selections?.Any() == true)
        profiles.AddRange(selection.Selections.SelectMany(s => GetProfiles(s, type)));

      return profiles;
    }

    public string GetSpecialRules(Profile profile)
    {
      return profile
        ?.Characteristics
        ?.FirstOrDefault(c => c.Name.ToLower() == "special rules")
        ?.Value;
    }

    public int? GetSpecialRuleValue(Profile profile, string ruleName)
    {
      string? specialRules = GetSpecialRules(profile);

      // No special rules means no Tough(X)
      if (specialRules == null)
        return null;

      var pattern = new Regex(ruleName + @"\((\d+)\)", RegexOptions.IgnoreCase);

      // Does not have the Tough(X) rule
      if (!pattern.IsMatch(specialRules))
        return null;

      return int.Parse(pattern.Match(specialRules).Groups[1].Value);
    }

    public int? GetToughness(Profile profile)
    {
      string? specialRules = GetSpecialRules(profile);

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

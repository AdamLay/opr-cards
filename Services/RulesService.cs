using System.Collections.Generic;
using System.Text.RegularExpressions;
using OnePageRulesCards.Data;
using OnePageRulesCards.Extensions;

namespace OnePageRulesCards.Services
{
  public class RulesService
  {
    public IEnumerable<Rule> FindRulesForUnit(Selection selection)
    {
      IEnumerable<Rule> FindFirstRules(IEnumerable<Selection> selections)
      {
        if (selections == null)
          return null;

        foreach (Selection selection in selections)
        {
          if (selection?.Rules != null)
            return selection.Rules;

          return FindFirstRules(selection.Selections);
        }

        return null;
      }

      if (selection?.Rules != null)
        return selection.Rules;

      return FindFirstRules(selection.Selections) ?? new List<Rule>();
    }

    public string ParseRule(string rule)
    {
      rule = Regex.Replace(rule, @"^\w+ with this special rule ", string.Empty);
      int firstFullstop = rule.IndexOf('.');
      return (firstFullstop >= 0 ? rule.Substring(0, firstFullstop+1) : rule).FirstCharToUpper();
    }

    public string ReplaceNameFromSpecialRules(string ruleName, string specialRules)
    {
      bool isParameterised = ruleName.IndexOf("(X)") > -1;
      if (!isParameterised)
        return ruleName;

      var pattern = new Regex(ruleName.Replace("(X)", @"\((\d+)\)"));

      Match match = pattern.Match(specialRules);

      return match?.Success == true
        ? match.Groups[0].Value
        : ruleName;
    }
  }
}
